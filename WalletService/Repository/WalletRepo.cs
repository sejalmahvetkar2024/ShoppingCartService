using static WalletService.Repository.WalletRepository;
using System;
using WalletService.Interface;
using WalletService.Models;
using Microsoft.EntityFrameworkCore;

namespace WalletService.Repository
{
    public class WalletRepository : IWallet
    {

        private readonly ShoppingCartContext _context;

        public WalletRepository(ShoppingCartContext context)
        {
            _context = context;
        }

        // 1️⃣ Get all payments
        public async Task<IEnumerable<Wallet>> GetPaymentsAsync()
        {
            return await _context.Wallets.ToListAsync();
        }

        // 2️⃣ Get a payment by Order ID
        public async Task<Wallet?> GetPaymentByOrderIdAsync(long orderId)
        {
            return await _context.Wallets.FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        // 3️⃣ Add a new payment
        public async Task AddPaymentAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        // 4️⃣ Update payment status
        public async Task<bool> UpdateBalanceAsync(long orderId, decimal balance)
        {
            var payment = await _context.Wallets.FirstOrDefaultAsync(p => p.OrderId == orderId);
            if (payment == null)
            {
                return false;
            }
            payment.Balance = balance;
            _context.Wallets.Update(payment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}