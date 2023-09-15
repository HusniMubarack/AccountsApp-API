using System;
namespace AccountsAPI.Models
{
    public class TransactionRequestModel
    {
        public string Name { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }

    public enum TransactionType
    {
        Credit,
        Debit
    }
}

