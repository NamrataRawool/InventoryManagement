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
    public class VendorController : ControllerBase
    {

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly InventoryDBContext _context;
        private readonly SharedMediaConfigOptions _sharedMediaOptions;

        public VendorController(IHostingEnvironment hostingEnvironment, InventoryDBContext context, IOptions<SharedMediaConfigOptions> sharedMediaOptions)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _sharedMediaOptions = sharedMediaOptions.Value;
        }

        // GET: /Vendors
        [HttpGet("/Vendors")]
        public async Task<ActionResult<IEnumerable<VendorOut>>> GetVendors()
        {
            var VendorDTOs = await _context.Vendors
                                           .AsNoTracking()
                                           .ToListAsync();

            List<VendorOut> vendors = new List<VendorOut>();
            foreach (var vendor in VendorDTOs)
                vendors.Add(new VendorOut(_context, vendor));

            return vendors;
        }

        // GET: /Vendor/5
        [HttpGet("/Vendor/{id}")]
        public async Task<ActionResult<VendorOut>> GetVendor(int id)
        {
            var vendor = await _context.Vendors
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(p => p.ID == id);


            if (vendor == null)
            {
                return NotFound();
            }

            return new VendorOut(_context, vendor);
        }

        // PUT: /Vendor/5
        [HttpPut("/Vendor/{id}")]
        public async Task<IActionResult> PutVendor(int id, VendorDTO VendorDTO)
        {
            if (id != VendorDTO.ID)
            {
                return BadRequest();
            }

            _context.Entry(VendorDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // POST: /Vendor
        [HttpPost("/Vendor")]
        public async Task<ActionResult<VendorOut>> PostVendor([FromForm]VendorIn vendorIn)
        {
            try
            {
                VendorDTO vendorDTO = new VendorDTO(vendorIn);
                _context.Vendors.Add(vendorDTO);
                await _context.SaveChangesAsync();

                vendorDTO = CreatedAtAction("GetProduct", new { id = vendorDTO.ID }, vendorDTO).Value as VendorDTO;

                await PutVendor(vendorDTO.ID, vendorDTO);

                return new VendorOut(_context, vendorDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendorOut>>> SearchVendors(string name)
        {
            if (String.IsNullOrEmpty(name))
                return BadRequest();

            var VendorDTOs = await _context.Vendors
            .AsNoTracking()
            .Where(p => p.CompanyName.Contains(name))
            .ToListAsync();

            List<VendorOut> products = new List<VendorOut>();
            foreach (var vendor in VendorDTOs)
                products.Add(new VendorOut(_context, vendor));

            return products;
        }

        // DELETE: /Vendor/5
        [HttpDelete("/Vendor/{id}")]
        public async Task<ActionResult<VendorDTO>> DeleteVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return vendor;
        }

        #region Public Methods
        public async Task<List<VendorOut>> GetSelectedVendors(List<string> vendorIds)
        {
            List<VendorOut> prodList = new List<VendorOut>();
            foreach (var productId in vendorIds)
            {
                var product = await GetVendor(Convert.ToInt32(productId.Trim()));
                if (product == null)
                    continue;
                prodList.Add(product.Value);
            }
            return prodList;
        }
        #endregion
        #region Private Methods
        private bool VendorExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
        #endregion

    }
}
