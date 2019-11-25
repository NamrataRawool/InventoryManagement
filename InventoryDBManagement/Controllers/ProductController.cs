using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryDBManagement.DAL;
using InventoryManagement.Common.Models;
using System.IO;
using InventoryManagement.Common.Configuration.Options;
using Microsoft.Extensions.Options;
using InventoryManagement.Common.Utils;

namespace InventoryDBManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly InventoryDBContext _context;
        private readonly IMediaSaver _mediaSaver;
        private readonly SharedMediaConfigOptions _sharedMediaOptions;

        public ProductController(InventoryDBContext context, IOptions<SharedMediaConfigOptions> sharedMediaOptions, IMediaSaver mediaSaver)
        {
            _context = context;
            _sharedMediaOptions = sharedMediaOptions.Value;
            _mediaSaver = mediaSaver;
        }

        // GET: api/Products
        [HttpGet("/Products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ThenInclude(c => c.Tax)
                .AsNoTracking().
                ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .ThenInclude(c => c.Tax)
                .AsNoTracking().
                FirstOrDefaultAsync(p => p.ProductID == id);


            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                product = CreatedAtAction("GetProduct", new { id = product.ProductID }, product).Value as Product;

                var imageName = Path.GetFileName(product.ImagePath);
                var relativeDestPath = Path.Combine( _sharedMediaOptions.Products, product.ProductID.ToString(), imageName);
                
                //Temporary 
                byte[] image = System.IO.File.ReadAllBytes(product.ImagePath);

                bool success = _mediaSaver.SaveImage(image, relativeDestPath);
                if (success)
                {
                    product.ImagePath = relativeDestPath;
                    await PutProduct(product.ProductID, product);
                }
                return product;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        // DELETE: api/Product/5
        [HttpDelete("{ id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }



        #region Public Methods
        public async Task<List<Product>> GetSelectedProducts(List<string> productIds)
        {
            List<Product> prodList = new List<Product>();
            foreach (var productId in productIds)
            {
                var product = await GetProduct(Convert.ToInt32(productId.Trim()));
                if (product == null)
                    continue;
                prodList.Add(product.Value);
            }
            return prodList;
        }
        #endregion
        #region Private Methods
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
        #endregion
    }
}
