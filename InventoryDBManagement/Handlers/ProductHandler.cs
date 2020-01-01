using InventoryDBManagement.Broadcaster;
using InventoryDBManagement.Configuration.Options;
using InventoryDBManagement.Controllers;
using InventoryDBManagement.DAL;
using InventoryDBManagement.Events;
using InventoryDBManagement.Events.Product;
using InventoryManagement.Models.DTO;
using InventoryManagement.Models.In;
using InventoryManagement.Models.Out;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.Handlers
{
    public class ProductHandler : IHandler<ProductHttpController>
    {

        public ProductHandler(InventoryDBContext Context, ProductHttpController HttpController)
            : base(Context, HttpController)
        {
        }

        public async Task<ActionResult<IEnumerable<ProductOut>>> GetProducts()
        {
            var productDtos = await m_Context.Products
                                .AsNoTracking()
                                .ToListAsync();

            List<ProductOut> products = new List<ProductOut>();
            foreach (var product in productDtos)
                products.Add(new ProductOut(m_Context, product));

            return products;
        }

        public async Task<ActionResult<ProductOut>> GetProduct(int ID)
        {
            var product = await m_Context.Products
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.ID == ID);

            if (product == null)
                return m_HttpController.NotFound();

            return new ProductOut(m_Context, product);
        }

        public async Task<ActionResult<ProductOut>> AddNewProduct(ProductIn productIN)
        {
            try
            {
                ProductDTO productDTO = new ProductDTO(productIN);
                m_Context.Products.Add(productDTO);
                await m_Context.SaveChangesAsync();

                productDTO = m_HttpController.CreatedAtAction("GetProduct", new { id = productDTO.ID }, productDTO).Value as ProductDTO;

                // Save image with IformFile
                UpdateImage(productDTO, productIN);

                await m_HttpController.PutProduct(productDTO.ID, productDTO);

                // Fire New product added event
                Event_NewProductAdded e = new Event_NewProductAdded(productDTO.ID);
                EventBroadcaster.Get().BroadcastEvent(e);

                return new ProductOut(m_Context, productDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        public async Task<ActionResult<ProductOut>> UpdateProduct(ProductIn productIN)
        {
            try
            {
                var productDTO = m_Context.GetProduct(productIN.ID);
                productDTO.CopyFrom(productIN);

                UpdateImage(productDTO, productIN);

                //await m_HttpController.PutProduct(productDTO.ID, productDTO);
                await UpdateProduct(productDTO.ID, productDTO);

                return new ProductOut(m_Context, productDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDto)
        {
            if (id != productDto.ID)
                return m_HttpController.BadRequest();

            m_Context.Entry(productDto).State = EntityState.Modified;

            try
            {
                await m_Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                    return m_HttpController.NotFound();
                else
                    throw;
            }

            return m_HttpController.NoContent();
        }

        public async Task<ActionResult<ProductDTO>> DeleteProduct(int ID)
        {
            var product = await m_Context.Products.FindAsync(ID);
            if (product == null)
            {
                return m_HttpController.NotFound();
            }

            m_Context.Products.Remove(product);
            await m_Context.SaveChangesAsync();

            return product;
        }

        public async Task<ActionResult<IEnumerable<ProductOut>>> GetProductByName(string Name)
        {
            if (String.IsNullOrEmpty(Name))
                return m_HttpController.BadRequest();

            var productDtos = await m_Context.Products
            .AsNoTracking()
            .Where(p => p.Name.Contains(Name))
            .ToListAsync();

            List<ProductOut> products = new List<ProductOut>();
            foreach (var product in productDtos)
                products.Add(new ProductOut(m_Context, product));

            return products;
        }

        /* return the path to save in the DB */
        private string UpdateImage(ProductDTO productDTO, ProductIn productIN)
        {
            string pathToSave = string.Empty;

            if (productIN.Images == null || productIN.Images.Count <= 0)
            {
                pathToSave = m_Context.GetProduct(productDTO.ID).ImagePath;
                return pathToSave;
            }

            var image = productIN.Images[0];
            if (image.Length > 0)
            {
                string FolderPath = Path.Combine(m_HttpController.GetHostingEnvironment().WebRootPath, SharedMediaConfigOptions.Products, productDTO.ID.ToString());
                if (!Directory.Exists(FolderPath))
                    Directory.CreateDirectory(FolderPath);

                // copy the image
                var finalPath = Path.Combine(FolderPath, image.FileName);
                using (var fs = new FileStream(finalPath, FileMode.Create))
                    image.CopyTo(fs);

                var relativeDestPath = Path.Combine(SharedMediaConfigOptions.Products, productDTO.ID.ToString(), image.FileName);
                pathToSave += relativeDestPath + ",";
            }

            // remove last ','
            if (pathToSave != null && pathToSave.Length > 0)
                pathToSave = pathToSave.Substring(0, pathToSave.Length - 1);

            productDTO.ImagePath = pathToSave;

            return pathToSave;
        }

        private bool ProductExists(int id)
        {
            return m_Context.Products.Any(e => e.ID == id);
        }

        public override void OnEvent(IEvent e)
        {
        }

        public override void RegisterEvents()
        {
        }
    }
}
