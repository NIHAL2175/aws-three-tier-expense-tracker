using System;

namespace Expense_Tracker.Models
{
    public class TransactionDto
    {
        public string? CategoryName { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
