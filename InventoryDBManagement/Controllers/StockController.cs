using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryDBManagement.DAL;
using InventoryManagement.Models.DTO;
using InventoryManagement.Models.Out;
using InventoryManagement.Models.In;

namespace InventoryDBManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly InventoryDBContext _context;

        public StockController(InventoryDBContext context)
        {
            _context = context;
        }

        // GET: /Stock
        [HttpGet("/Stocks")]
        public async Task<ActionResult<IEnumerable<StockOut>>> GetStocks()
        {
            await _context.Stocks.ToListAsync();
            var stocksDto = await _context.Stocks
                .Include(s => s.Product)
               .AsNoTracking().
               ToListAsync();

            List<StockOut> stocks = new List<StockOut>();
            foreach (var stock in stocksDto)
            {
                var stockOut = new StockOut(stock);
                stocks.Add(stockOut);
            }
            return stocks;
        }

        // GET: /Stock/5
        [HttpGet("/Stock/{id}")]
        public async Task<ActionResult<StockOut>> GetStock(int id)
        {
            var stockDTO = await _context.Stocks
                .Include(s => s.Product)
                .AsNoTracking()
                .FirstAsync(s => s.ID == id);

            if (stockDTO == null)
            {
                return NotFound();
            }

            return new StockOut(stockDTO);
        }

        // PUT: /Stock/5
        [HttpPut("/Stock/{id}")]
        public async Task<IActionResult> PutStock(int id, StockDTO stockDTO)
        {
            if (id != stockDTO.ID)
            {
                return BadRequest();
            }

            _context.Entry(stockDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
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

        // POST: /Stock
        [HttpPost("/Stock")]
        public async Task<ActionResult<StockOut>> PostStock(StockIn stockIn)
        {
            var stockDto = new StockDTO(stockIn);
            _context.Stocks.Add(stockDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStock", new { id = stockDto.ID }, stockDto);
        }


        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.ID == id);
        }
    }
}
