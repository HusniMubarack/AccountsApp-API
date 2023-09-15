using System;
using System.ComponentModel.DataAnnotations;

namespace AccountsAPI.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public string UserName { get; set; }
        public string TransactionType { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalBalance { get; set; }
    }

    public class TransactionInputModel
    {
        public string UserName { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}

