using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Common.Models;
using InventoryManagement.UI.DAL;
using System.Data.SqlClient;

namespace InventoryManagement.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly InventoryDBContext _context;
        private readonly ProductsController _productsController;
        public TransactionsController(InventoryDBContext context, ProductsController productsController)
        {
            _context = context;
            _productsController = productsController;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        // GET: api/Transactions/5
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
                List<Product> prodList = new List<Product>();
                foreach (var productId in productIDs)
                {
                    var product = await _productsController.GetProduct(Convert.ToInt32(productId));
                    prodList.Add(product.Value);
                }
                transaction.Products = prodList;
                return transaction;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // PUT: api/Transactions/5
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

        // POST: api/Transactions
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

        // DELETE: api/Transactions/5
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
