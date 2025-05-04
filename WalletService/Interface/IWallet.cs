
using WalletService.Models;

namespace WalletService.Interface
{
    public interface IWallet
    {
        Task<IEnumerable<Wallet>> GetPaymentsAsync();
        Task<Wallet?> GetPaymentByOrderIdAsync(long orderId);
        Task AddPaymentAsync(Wallet wallet);
        Task<bool> UpdateBalanceAsync(long orderId, decimal balance);
    }
}
