using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryDBManagement.Configuration.Options;
using InventoryDBManagement.DAL;
using InventoryDBManagement.Handlers;
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
    public class VendorHttpController : ControllerBase
    {
        private VendorHandler m_Handler;
        private readonly IHostingEnvironment m_HostingEnvironment;
        private readonly InventoryDBContext _context;

        public VendorHttpController(IHostingEnvironment hostingEnvironment, InventoryDBContext context)
        {
            m_Handler = new VendorHandler(context, this);
            m_HostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: /Vendors
        [HttpGet("/Vendors")]
        public async Task<ActionResult<IEnumerable<VendorOut>>> GetVendors()
        {
            return await m_Handler.GetVendors();
        }

        // GET: /Vendor/5
        [HttpGet("/Vendor/{id}")]
        public async Task<ActionResult<VendorOut>> GetVendor(int id)
        {
            return await m_Handler.GetVendor(id);
        }

        // PUT: /Vendor/5
        [HttpPut("/Vendor/{id}")]
        public async Task<IActionResult> PutVendor(int id, VendorDTO VendorDTO)
        {
            return await m_Handler.UpdateVendor(id, VendorDTO);
        }

        // POST: /Vendor
        [HttpPost("/Vendor")]
        public async Task<ActionResult<VendorOut>> PostVendor([FromForm]VendorIn vendorIn)
        {
            try
            {
                if (vendorIn.ID == 0)
                    return await m_Handler.AddNewVendor(vendorIn);

                return await m_Handler.UpdateVendor(vendorIn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        [HttpGet("/Vendor/name={name}")]
        public async Task<ActionResult<IEnumerable<VendorOut>>> SearchVendors(string name)
        {
            return await m_Handler.SearchVendorByName(name);
        }

        // DELETE: /Vendor/5
        [HttpDelete("/Vendor/{id}")]
        public async Task<ActionResult<VendorDTO>> DeleteVendor(int id)
        {
            return await m_Handler.DeleteVendor(id);
        }
    }
}
