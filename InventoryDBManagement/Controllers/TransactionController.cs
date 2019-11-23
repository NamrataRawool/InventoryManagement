using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryDBManagement.DAL;
using InventoryManagement.Common.Models;

namespace InventoryDBManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly InventoryDBContext _context;
        private readonly ProductController _productsController;
        public TransactionController(InventoryDBContext context, ProductController productsController)
        {
            _context = context;
            _productsController = productsController;
        }

        // GET: api/Transactions
        [HttpGet("/Transactions")]
        public async Task<ActionResult<List<Transaction>>> GetTransactions()
        {
            var transactions = await _context.Transactions.ToListAsync();
            if (transactions == null)
                return NotFound();
            List<Transaction> transactionList = new List<Transaction>();

            foreach (var transaction in transactions)
            {
                var trans = await GetTransaction(transaction.TransactionID);
                transactionList.Add(trans.Value);
            }
            return transactionList;
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            try
            {
                var transaction = _context.Transactions
                .AsNoTracking()
                .FirstOrDefault(t => t.TransactionID == id);

                if (transaction == null)
                {
                    return NotFound();
                }
                string[] productIDs = transaction.ProductIDs.Split(',');
                transaction.ProductDetails = await _productsController.GetSelectedProducts(productIDs.ToList());
                return transaction;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // PUT: api/Transaction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.TransactionID)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transaction
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            try
            {
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return await GetTransaction(transaction.TransactionID);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        // DELETE: api/Transaction/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transaction>> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionID == id);
        }
    }
}
