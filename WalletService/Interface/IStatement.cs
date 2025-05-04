using WalletService.Models;

namespace WalletService.Interface
{
    public interface IStatement
    {
        Task<IEnumerable<Statement>> GetStatementsByUserIdAsync(long userId);
        Task AddStatementAsync(Statement statement);
    }
}