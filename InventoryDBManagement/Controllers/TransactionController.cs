using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryDBManagement.DAL;
using InventoryManagement.Common.Models;
using InventoryManagement.Common.Models.DTO;
using InventoryManagement.Common.Models.Out;
using InventoryManagement.Common.Models.In;

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
        public async Task<ActionResult<List<TransactionOut>>> GetTransactions()
        {
            var transactions = await _context.Transactions.ToListAsync();
            if (transactions == null)
                return NotFound();

            List<TransactionOut> transactionList = new List<TransactionOut>();

            foreach (var transaction in transactions)
            {
                var trans = await GetTransaction(transaction.TransactionID);
                transactionList.Add(trans.Value);
            }
            return transactionList;
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionOut>> GetTransaction(int id)
        {
            try
            {
                var transactionDto = _context.Transactions
                .Include(t => t.Customer)
                .AsNoTracking()
                .FirstOrDefault(t => t.TransactionID == id);

                if (transactionDto == null)
                {
                    return NotFound();
                }

                string[] productIDs = transactionDto.ProductIDs.Split(',');
                string[] productQuantity = transactionDto.ProductQuantity.Split(',');
                List<ProductOut> prodList = new List<ProductOut>();
                int i = 0;
                foreach (var productId in productIDs)
                {
                    var product = await _productsController.GetProduct(Convert.ToInt32(productId.Trim()));
                    product.Value.Quantity = Convert.ToInt32(productQuantity[i]);
                    if (product == null)
                        continue;
                    prodList.Add(product.Value);
                    i++;
                }
                var transactionOut = new TransactionOut(transactionDto);
                transactionOut.ProductDetails = prodList;

                return transactionOut;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // PUT: api/Transaction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, TransactionDTO transactionDto)
        {
            if (id != transactionDto.TransactionID)
            {
                return BadRequest();
            }

            _context.Entry(transactionDto).State = EntityState.Modified;

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
        public async Task<ActionResult<TransactionOut>> PostTransaction(TransactionIn transactionIn)
        {
            try
            {
                TransactionDTO transactionDTO = new TransactionDTO(transactionIn);
                _context.Transactions.Add(transactionDTO);
                await _context.SaveChangesAsync();

                //TODO: Customer amount update

                return await GetTransaction(transactionDTO.TransactionID);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionID == id);
        }
    }
}
