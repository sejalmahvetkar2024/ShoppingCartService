using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletService.Models;
using WalletService.Interface;
using WalletService.Repository;

namespace WalletService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWallet _IWallet;
        private readonly IStatement _IStatement;

        public WalletController(IWallet wallet, IStatement statement)
        {
            _IWallet = wallet;
            _IStatement = statement;
        }

        // 1️⃣ Get all payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wallet>>> GetPayments()
        {
            var payments = await _IWallet.GetPaymentsAsync();
            return Ok(payments);
        }

        // 2️⃣ Get payment status by Order ID
        [HttpGet("status/{orderId}")]
        public async Task<ActionResult<Wallet>> GetPaymentStatus(long orderId)
        {
            var payment = await _IWallet.GetPaymentByOrderIdAsync(orderId);
            if (payment == null)
            {
                return NotFound("Payment not found for this order.");
            }
            return Ok(payment);
        }

        // 3️⃣ Process a new payment & record a statement
        [HttpPost("process")]
        public async Task<ActionResult> ProcessPayment(Wallet wallet, string transactionType, string transactionRemark)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _IWallet.AddPaymentAsync(wallet);

                    // ✅ Add transaction statement
                    var state = new Statement
                    {
                        WalletId = wallet.WalletId,
                        OrderId = wallet.OrderId, // Assuming Order ID represents the user
                        Amount = wallet.Balance,
                        TransactionType = transactionType,
                        TransactionRemark = transactionRemark
                    };



                    await _IStatement.AddStatementAsync(state);

                    return CreatedAtAction(nameof(GetPaymentStatus), new { orderId = wallet.OrderId }, wallet);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        // 4️⃣ Update payment status
        [HttpPut("update-status/{orderId}")]
        public async Task<IActionResult> UpdatePaymentStatus(long orderId, decimal balance, string transactionType, string transactionRemark)
        {
            //await _IWallet.UpdateBalanceAsync(orderId,balance);

            //// ✅ Also update statement
            //var payment = await _IWallet.GetPaymentByOrderIdAsync(orderId);
            //if (payment != null)
            //{
            //    var statement = new Statement
            //    {
            //        OrderId = payment.OrderId, // Assuming Order ID represents the user
            //        WalletId = payment.WalletId,
            //        Amount = payment.Balance ,
            //        TransactionType = transactionType,
            //        TransactionRemark = transactionRemark
            //    };
            //    await _IStatement.AddStatementAsync(statement);
            //}

            //return NoContent();

            bool updateSuccess = await _IWallet.UpdateBalanceAsync(orderId, balance);

            if (!updateSuccess)
            {
                return NotFound(new { Message = "Wallet not found for this order." });
            }

            // ✅ Then, add a new transaction statement
            var payment = await _IWallet.GetPaymentByOrderIdAsync(orderId);
            if (payment != null)
            {
                var statement = new Statement
                {
                    OrderId = payment.OrderId,
                    WalletId = payment.WalletId,
                    Amount = balance,  // ✅ Use the updated balance
                    TransactionType = transactionType,
                    TransactionRemark = transactionRemark
                };

                await _IStatement.AddStatementAsync(statement);
            }

            return Ok(new { Message = "Wallet updated successfully." }); // ✅ Return success message
        }


        // 5️⃣ Get user statements
        [HttpGet("statements/{userId}")]
        public async Task<ActionResult<IEnumerable<Statement>>> GetUserStatements(long userId)
        {
            var statements = await _IStatement.GetStatementsByUserIdAsync(userId);
            return Ok(statements);
        }
    }
}