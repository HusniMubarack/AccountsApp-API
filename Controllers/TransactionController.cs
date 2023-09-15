// Controllers/TransactionsController.cs
using System.Collections.Generic;
using System.Linq;
using AccountsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public TransactionsController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> GetTransactions()
        {
            return _dbContext.Transactions.ToList();
        }


        [HttpPost]
        public IActionResult AddTransaction(TransactionInputModel input)
        {
            var lastTransaction = _dbContext.Transactions
                .Where(t => t.UserName == input.UserName)
                .OrderByDescending(t => t.TransactionID)
                .FirstOrDefault();

            decimal totalBalance = lastTransaction?.TotalBalance ?? 0;

            var newTransaction = new Transaction();
            if (input.TransactionType == "Credit")
            {
                totalBalance += input.Amount;
                newTransaction = new Transaction
                {
                    UserName = input.UserName,
                    TransactionType = input.TransactionType,
                    CreditAmount = input.Amount,
                    DebitAmount = 0,
                    Date = input.Date,
                    TotalBalance = totalBalance
                };
            }
            else if (input.TransactionType == "Debit")
            {
                totalBalance -= input.Amount;
                newTransaction = new Transaction
                {
                    UserName = input.UserName,
                    TransactionType = input.TransactionType,
                    CreditAmount = 0,
                    DebitAmount = input.Amount,
                    Date = input.Date,
                    TotalBalance = totalBalance
                };
            }
            else
            {
                return BadRequest("Invalid transaction type.");
            }

            _dbContext.Transactions.Add(newTransaction);
            _dbContext.SaveChanges();

            return Ok("Transaction added successfully.");
        }

        [HttpDelete("{userName}")]
        public IActionResult DeleteLastTransaction(string userName)
        {
            var lastTransaction = _dbContext.Transactions
                .Where(t => t.UserName == userName)
                .OrderByDescending(t => t.Date)
                .FirstOrDefault();

            if (lastTransaction == null)
            {
                return NotFound($"No transactions found for user {userName}.");
            }

            _dbContext.Transactions.Remove(lastTransaction);
            _dbContext.SaveChanges();

            return Ok($"Last transaction for user {userName} deleted.");
        }

        [HttpGet("{userName}")]
        public IActionResult GetTransactionsForUser(string userName)
        {
            var transactions = _dbContext.Transactions
                .Where(t => t.UserName == userName)
                .OrderByDescending(t => t.Date)
                .Take(10)
                .ToList();

            if (transactions.Count == 0)
            {
                return NotFound($"No transactions found for user {userName}.");
            }

            return Ok(transactions);
        }

        [HttpGet("usernames")]
        public IActionResult GetUniqueUsernames()
        {
            var uniqueUsernames = _dbContext.Transactions
                .Select(t => t.UserName)
                .Distinct()
                .ToList();

            return Ok(uniqueUsernames);
        }   

    }
}
