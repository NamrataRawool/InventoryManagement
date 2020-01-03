using System;
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
using InventoryDBManagement.Handlers;

namespace InventoryDBManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductHttpController : ControllerBase
    {

        private ProductHandler m_Handler;

        private readonly IHostingEnvironment m_HostingEnvironment;
        private readonly InventoryDBContext _context;

        public ProductHttpController(IHostingEnvironment hostingEnvironment, InventoryDBContext context)
        {
            m_Handler = new ProductHandler(context, this);

            m_HostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: /Products
        [HttpGet("/Products")]
        public async Task<ActionResult<IEnumerable<ProductOut>>> GetProducts()
        {
            return await m_Handler.GetProducts();
        }

        // GET: /Product/5
        [HttpGet("/Product/{id}")]
        public async Task<ActionResult<ProductOut>> GetProduct(int id)
        {
            return await m_Handler.GetProduct(id);
        }

        // PUT: /Product/5
        [HttpPut("/Product/{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO productDTO)
        {
            return await m_Handler.UpdateProduct(id, productDTO);
        }

        // POST: /Product
        [HttpPost("/Product")]
        public async Task<ActionResult<ProductOut>> PostProduct([FromForm]ProductIn productIN)
        {
            try
            {
                if (productIN.ID == 0)
                    return await m_Handler.AddNewProduct(productIN);

                return await m_Handler.UpdateProduct(productIN);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        [HttpGet("/Product/Name={name}")]
        public async Task<ActionResult<IEnumerable<ProductOut>>> SearchProducts(string name)
        {
            return await m_Handler.GetProductByName(name);
        }

        // DELETE: /Product/5
        [HttpDelete("/Product/{id}")]
        public async Task<ActionResult<ProductDTO>> DeleteProduct(int ID)
        {
            return await m_Handler.DeleteProduct(ID);
        }

        public IHostingEnvironment GetHostingEnvironment()
        {
            return m_HostingEnvironment;
        }

    }
}
