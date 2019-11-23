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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InventoryDBContext _context;
        SharedMediaConfigOptions _sharedMediaOptions;

        public ProductsController(IOptions<SharedMediaConfigOptions> sharedMediaOptions, InventoryDBContext context)
        {
            _context = context;
            _sharedMediaOptions = sharedMediaOptions.Value;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            product.ImagePath = Path.Combine(Environment.CurrentDirectory, _sharedMediaOptions.Image, product.ImagePath);
            return product;
        }

        // PUT: api/Products/5
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

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                var imageDirPath = Path.Combine(Environment.CurrentDirectory, @"Images\");
                if (!Directory.Exists(imageDirPath))
                {
                    Directory.CreateDirectory(imageDirPath);
                }
                if (System.IO.File.Exists(product.ImagePath))
                {
                    //New path images folder
                    var imagePath = Path.Combine(imageDirPath, Path.GetFileName(product.ImagePath));
                    if (!System.IO.File.Exists(imagePath))
                        System.IO.File.Copy(product.ImagePath, imagePath);

                    product.ImagePath = Path.GetFileName(product.ImagePath);

                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
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

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }


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

    }
}
