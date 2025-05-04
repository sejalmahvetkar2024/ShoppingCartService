using static WalletService.Repository.StatementRepository;
using System;
using WalletService.Interface;
using WalletService.Models;
using Microsoft.EntityFrameworkCore;

namespace WalletService.Repository
{
    public class StatementRepository : IStatement
    {
        private readonly ShoppingCartContext _context;

        public StatementRepository(ShoppingCartContext context)
        {
            _context = context;
        }

        // 1️⃣ Get all statements for a user
        public async Task<IEnumerable<Statement>> GetStatementsByUserIdAsync(long orderId)
        {
            return await _context.Statements
                .Where(s => s.OrderId == orderId)
                .ToListAsync();
        }

        // 2️⃣ Add a new statement
        public async Task AddStatementAsync(Statement statement)
        {
            await _context.Statements.AddAsync(statement);
            await _context.SaveChangesAsync();
        }
    }
}