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
using Microsoft.Extensions.Options;
using InventoryManagement.Common.Utils;
using InventoryDBManagement.Configuration.Options;
using InventoryManagement.Common.Models.DTO;
using InventoryManagement.Common.Models.In;
using InventoryManagement.Common.Models.Out;

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
        public async Task<ActionResult<IEnumerable<ProductOut>>> GetProducts()
        {
            var productDtos = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking().
                ToListAsync();
            List<ProductOut> products = new List<ProductOut>();
            foreach (var product in productDtos)
            {
                var productOut = new ProductOut(product);
                products.Add(productOut);
            }
            return products;
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOut>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking().
                FirstOrDefaultAsync(p => p.ProductID == id);


            if (product == null)
            {
                return NotFound();
            }

            return new ProductOut(product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO productDto)
        {
            if (id != productDto.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(productDto).State = EntityState.Modified;

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
        public async Task<ActionResult<ProductOut>> PostProduct(ProductIn productIn)
        {
            try
            {
                ProductDTO productDTO = new ProductDTO(productIn);
                _context.Products.Add(productDTO);
                await _context.SaveChangesAsync();

                productDTO = CreatedAtAction("GetProduct", new { id = productDTO.ProductID }, productDTO).Value as ProductDTO;

                var relativeDestPath = Path.Combine(_sharedMediaOptions.Products, productDTO.ProductID.ToString(), productIn.ImageName);

                //Temporary TODO: Need to pass byte array received from user
                byte[] image = System.IO.File.ReadAllBytes("D:\\Test.jpg");

                bool success = _mediaSaver.SaveImage(image, relativeDestPath);
                if (success)
                {
                    productDTO.ImagePath = relativeDestPath;
                    await PutProduct(productDTO.ProductID, productDTO);
                }
                ProductOut productOut = new ProductOut(productDTO);
                return productOut;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        // DELETE: api/Product/5
        [HttpDelete("{ id}")]
        public async Task<ActionResult<ProductDTO>> DeleteProduct(int id)
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
        public async Task<List<ProductOut>> GetSelectedProducts(List<string> productIds)
        {
            List<ProductOut> prodList = new List<ProductOut>();
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
