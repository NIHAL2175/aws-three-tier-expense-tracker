using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expense_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Services
{
    public class TransactionTotals
    {
        public int TotalIncome { get; set; }
        public int TotalExpense { get; set; }
        public int Balance => TotalIncome - TotalExpense;
    }

    public class TransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactionsForUserAsync(string userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.Category)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task<List<TransactionDto>> GetTransactionDtosForUserAsync(string userId)
        {
            var transactions = await GetTransactionsForUserAsync(userId);

            return transactions.Select(t => new TransactionDto
            {
                CategoryName = t.Category?.Title ?? string.Empty,
                Amount = t.Amount,
                Date = t.Date
            })
            .ToList();
        }

        public TransactionTotals CalculateTotals(IEnumerable<Transaction> transactions)
        {
            var totalIncome = transactions
                .Where(t => t.Category?.Type?.ToLower() == "income")
                .Sum(t => t.Amount);

            var totalExpense = transactions
                .Where(t => t.Category?.Type?.ToLower() == "expense")
                .Sum(t => t.Amount);

            return new TransactionTotals
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense
            };
        }
    }
}
