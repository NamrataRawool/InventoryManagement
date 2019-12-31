using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryDBManagement.Configuration.Options;
using InventoryDBManagement.DAL;
using InventoryDBManagement.Models.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InventoryDBManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly InventoryDBContext _context;
        private readonly SharedMediaConfigOptions _sharedMediaOptions;

        public PurchaseController(IHostingEnvironment hostingEnvironment, InventoryDBContext context, IOptions<SharedMediaConfigOptions> sharedMediaOptions)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _sharedMediaOptions = sharedMediaOptions.Value;
        }

        // GET: /Purchases
        [HttpGet("/Purchases")]
        public async Task<ActionResult<IEnumerable<PurchaseOut>>> GetPurchases()
        {
            var PurchaseDTOs = await _context.Purchases
                                           .AsNoTracking()
                                           .ToListAsync();

            List<PurchaseOut> Purchases = new List<PurchaseOut>();
            foreach (var Purchase in PurchaseDTOs)
                Purchases.Add(new PurchaseOut(_context, Purchase));

            return Purchases;
        }

        // GET: /Purchase/5
        [HttpGet("/Purchase/{id}")]
        public async Task<ActionResult<PurchaseOut>> GetPurchase(int id)
        {
            var Purchase = await _context.Purchases
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(p => p.ID == id);


            if (Purchase == null)
            {
                return NotFound();
            }

            return new PurchaseOut(_context, Purchase);
        }

        // PUT: /Purchase/5
        [HttpPut("/Purchase/{id}")]
        public async Task<IActionResult> PutPurchase(int id, PurchaseDTO PurchaseDTO)
        {
            if (id != PurchaseDTO.ID)
            {
                return BadRequest();
            }

            _context.Entry(PurchaseDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: /Purchase
        [HttpPost("/Purchase")]
        public async Task<ActionResult<PurchaseOut>> PostPurchase([FromForm]PurchaseIn PurchaseIn)
        {
            try
            {
                PurchaseDTO PurchaseDTO = new PurchaseDTO(PurchaseIn);
                _context.Purchases.Add(PurchaseDTO);
                await _context.SaveChangesAsync();

                PurchaseDTO = CreatedAtAction("GetProduct", new { id = PurchaseDTO.ID }, PurchaseDTO).Value as PurchaseDTO;

                await PutPurchase(PurchaseDTO.ID, PurchaseDTO);

                return new PurchaseOut(_context, PurchaseDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        // DELETE: /Purchase/5
        [HttpDelete("/Purchase/{id}")]
        public async Task<ActionResult<PurchaseDTO>> DeletePurchase(int id)
        {
            var Purchase = await _context.Purchases.FindAsync(id);
            if (Purchase == null)
            {
                return NotFound();
            }

            _context.Purchases.Remove(Purchase);
            await _context.SaveChangesAsync();

            return Purchase;
        }

        #region Public Methods
        public async Task<List<PurchaseOut>> GetSelectedPurchases(List<string> PurchaseIds)
        {
            List<PurchaseOut> prodList = new List<PurchaseOut>();
            foreach (var productId in PurchaseIds)
            {
                var product = await GetPurchase(Convert.ToInt32(productId.Trim()));
                if (product == null)
                    continue;
                prodList.Add(product.Value);
            }
            return prodList;
        }
        #endregion
        #region Private Methods
        private bool PurchaseExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
        #endregion
    }
}
