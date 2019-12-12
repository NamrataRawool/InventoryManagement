//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using InventoryDBManagement.DAL;
//using InventoryManagement.Models;

//namespace InventoryDBManagement.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class TaxController : ControllerBase
//    {
//        private readonly InventoryDBContext _context;

//        public TaxController(InventoryDBContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Taxes
//        [HttpGet("/Taxes")]
//        public async Task<ActionResult<IEnumerable<Tax>>> GetTaxes()
//        {
//            return await _context.Taxes.ToListAsync();
//        }

//        // GET: api/Tax/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Tax>> GetTax(int id)
//        {
//            var tax = await _context.Taxes.FindAsync(id);

//            if (tax == null)
//            {
//                return NotFound();
//            }

//            return tax;
//        }

//        // PUT: api/Tax/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutTax(int id, Tax tax)
//        {
//            if (id != tax.TaxID)
//            {
//                return BadRequest();
//            }

//            _context.Entry(tax).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!TaxExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Tax
//        [HttpPost]
//        public async Task<ActionResult<Tax>> PostTax(Tax tax)
//        {
//            _context.Taxes.Add(tax);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetTax", new { id = tax.TaxID }, tax);
//        }

//        // DELETE: api/Tax/5
//        [HttpDelete("{id}")]
//        public async Task<ActionResult<Tax>> DeleteTax(int id)
//        {
//            var tax = await _context.Taxes.FindAsync(id);
//            if (tax == null)
//            {
//                return NotFound();
//            }

//            _context.Taxes.Remove(tax);
//            await _context.SaveChangesAsync();

//            return tax;
//        }

//        private bool TaxExists(int id)
//        {
//            return _context.Taxes.Any(e => e.TaxID == id);
//        }
//    }
//}
