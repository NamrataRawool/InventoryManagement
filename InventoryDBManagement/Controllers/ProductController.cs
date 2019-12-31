﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryDBManagement.DAL;
using InventoryManagement.Models;
using System.IO;
using Microsoft.Extensions.Options;
using InventoryDBManagement.Configuration.Options;
using InventoryManagement.Models.DTO;
using InventoryManagement.Models.In;
using InventoryManagement.Models.Out;
using Microsoft.AspNetCore.Hosting;

namespace InventoryDBManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly InventoryDBContext _context;
        private readonly SharedMediaConfigOptions _sharedMediaOptions;

        public ProductController(IHostingEnvironment hostingEnvironment, InventoryDBContext context, IOptions<SharedMediaConfigOptions> sharedMediaOptions)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _sharedMediaOptions = sharedMediaOptions.Value;
        }

        // GET: /Products
        [HttpGet("/Products")]
        public async Task<ActionResult<IEnumerable<ProductOut>>> GetProducts()
        {
            var productDtos = await _context.Products
                .AsNoTracking().
                ToListAsync();

            List<ProductOut> products = new List<ProductOut>();
            foreach (var product in productDtos)
                products.Add(new ProductOut(_context, product));

            return products;
        }

        // GET: /Product/5
        [HttpGet("/Product/{id}")]
        public async Task<ActionResult<ProductOut>> GetProduct(int id)
        {
            var product = await _context.Products
                .AsNoTracking().
                FirstOrDefaultAsync(p => p.ID == id);

            if (product == null)
                return NotFound();

            return new ProductOut(_context, product);
        }

        // PUT: /Product/5
        [HttpPut("/Product/{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO productDto)
        {
            if (id != productDto.ID)
                return BadRequest();

            _context.Entry(productDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        /* return the path to save in the DB */
        private string UpdateImage(ProductDTO productDTO, ProductIn productIN)
        {
            string pathToSave = String.Empty;
            if (productIN.Images != null)
            {
                var image = productIN.Images[0];
                if (image.Length > 0)
                {
                    string FolderPath = Path.Combine(_hostingEnvironment.WebRootPath, _sharedMediaOptions.Products, productDTO.ID.ToString());
                    if (!Directory.Exists(FolderPath))
                        Directory.CreateDirectory(FolderPath);

                    // copy the image
                    var finalPath = Path.Combine(FolderPath, image.FileName);
                    using(var fs = new FileStream(finalPath, FileMode.Create))
                        image.CopyTo(fs);

                    var relativeDestPath = Path.Combine(_sharedMediaOptions.Products, productDTO.ID.ToString(), image.FileName);
                    pathToSave += relativeDestPath + ",";
                }

                // remove last ','
                if (pathToSave != null && pathToSave.Length > 0)
                    pathToSave = pathToSave.Substring(0, pathToSave.Length - 1);

                productDTO.ImagePath = pathToSave;
            }
            return pathToSave;
        }

        private async Task<ActionResult<ProductOut>> AddNewProduct(ProductIn productIN)
        {
            try
            {
                ProductDTO productDTO = new ProductDTO(productIN);
                _context.Products.Add(productDTO);
                await _context.SaveChangesAsync();

                productDTO = CreatedAtAction("GetProduct", new { id = productDTO.ID }, productDTO).Value as ProductDTO;

                // Save image with IformFile
                UpdateImage(productDTO, productIN);

                await PutProduct(productDTO.ID, productDTO);

                return new ProductOut(_context, productDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        private async Task<ActionResult<ProductOut>> UpdateProduct(ProductIn productIN)
        {
            try
            {
                //var productDTO = await _context.Products
                //            .AsNoTracking()
                //            .FirstOrDefaultAsync(p => p.ID == productIN.ID);

                var productDTO = new ProductDTO(productIN);
                UpdateImage(productDTO, productIN);

                await PutProduct(productDTO.ID, productDTO);

                return new ProductOut(_context, productDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        // POST: /Product
        [HttpPost("/Product")]
        public async Task<ActionResult<ProductOut>> PostProduct([FromForm]ProductIn productIN)
        {
            try
            {
                if (productIN.ID == 0)
                    return await AddNewProduct(productIN);

                return await UpdateProduct(productIN);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductOut>>> SearchProducts(string name)
        {
            if (String.IsNullOrEmpty(name))
                return BadRequest();

            var productDtos = await _context.Products
            .AsNoTracking()
            .Where(p => p.Name.Contains(name))
            .ToListAsync();

            List<ProductOut> products = new List<ProductOut>();
            foreach (var product in productDtos)
                products.Add(new ProductOut(_context, product));

            return products;
        }

        // DELETE: /Product/5
        [HttpDelete("/Product/{id}")]
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
            return _context.Products.Any(e => e.ID == id);
        }
        #endregion
    }
}
