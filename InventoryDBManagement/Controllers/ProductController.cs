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

namespace InventoryDBManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly InventoryDBContext _context;
        SharedMediaConfigOptions _sharedMediaOptions;

        public ProductController(IOptions<SharedMediaConfigOptions> sharedMediaOptions, InventoryDBContext context)
        {
            _context = context;
            _sharedMediaOptions = sharedMediaOptions.Value;
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
            product.ImagePath = Path.Combine(Environment.CurrentDirectory, _sharedMediaOptions.Image, product.ImagePath);
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
                product.ImagePath = SaveImage(product.ProductID, product.ImagePath);

                await PutProduct(product.ProductID, product);

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

        private string SaveImage(int productId, string imagePath)
        {
            var relativePath = String.Format("{0}{1}", @"shared\media\images\products\", productId);
            var imageDirPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", relativePath);

            if (!Directory.Exists(imageDirPath))
            {
                Directory.CreateDirectory(imageDirPath);
            }
            if (System.IO.File.Exists(imagePath))
            {
                //New path images folder
                var destPath = Path.Combine(imageDirPath, Path.GetFileName(imagePath));
                if (!System.IO.File.Exists(destPath))
                    System.IO.File.Copy(imagePath, destPath);

                var storePath = String.Format("{0}\\{1}", relativePath, Path.GetFileName(imagePath));
                return storePath;
            }
            return null;
        }
        #endregion
    }
}
