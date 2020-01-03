using InventoryDBManagement.Controllers;
using InventoryDBManagement.DAL;
using InventoryDBManagement.Events;
using InventoryDBManagement.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.Handlers
{
    public class VendorHandler : IHandler<VendorHttpController>
    {
        public VendorHandler(InventoryDBContext Context, VendorHttpController HttpController) : base(Context, HttpController)
        {
        }

        public async Task<ActionResult<IEnumerable<VendorOut>>> GetVendors()
        {
            var VendorDTOs = await m_Context.Vendors
                                         .AsNoTracking()
                                         .ToListAsync();

            List<VendorOut> vendors = new List<VendorOut>();
            foreach (var vendor in VendorDTOs)
                vendors.Add(new VendorOut(m_Context, vendor));

            return vendors;
        }

        public async Task<ActionResult<VendorOut>> GetVendor(int id)
        {
            var vendor = await m_Context.Vendors
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(p => p.ID == id);

            if (vendor == null)
            {
                return m_HttpController.NotFound();
            }

            return new VendorOut(m_Context, vendor);
        }

        public async Task<IActionResult> UpdateVendor(int id, VendorDTO VendorDTO)
        {
            if (id != VendorDTO.ID)
            {
                return m_HttpController.BadRequest();
            }

            m_Context.Entry(VendorDTO).State = EntityState.Modified;

            try
            {
                await m_Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
                    return m_HttpController.NotFound();
                else
                    throw;
            }
            return m_HttpController.NoContent();
        }

        public async Task<ActionResult<VendorOut>> UpdateVendor(VendorIn vendorIn)
        {
            try
            {
                var vendorDTO = m_Context.GetVendor(vendorIn.ID);
                vendorDTO.CopyFrom(vendorIn);

                await UpdateVendor(vendorDTO.ID, vendorDTO);

                return new VendorOut(m_Context, vendorDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<ActionResult<IEnumerable<VendorOut>>> SearchVendorByName(string name)
        {
            if (String.IsNullOrEmpty(name))
                return m_HttpController.BadRequest();

            var VendorDTOs = await m_Context.Vendors
            .AsNoTracking()
            .Where(p => p.CompanyName.Contains(name))
            .ToListAsync();

            List<VendorOut> products = new List<VendorOut>();
            foreach (var vendor in VendorDTOs)
                products.Add(new VendorOut(m_Context, vendor));

            return products;
        }

        public async Task<ActionResult<VendorOut>> AddNewVendor([FromForm]VendorIn vendorIn)
        {
            try
            {
                VendorDTO vendorDTO = new VendorDTO(vendorIn);
                m_Context.Vendors.Add(vendorDTO);
                await m_Context.SaveChangesAsync();

                vendorDTO = m_HttpController.CreatedAtAction("GetProduct", new { id = vendorDTO.ID }, vendorDTO).Value as VendorDTO;

                await m_HttpController.PutVendor(vendorDTO.ID, vendorDTO);

                return new VendorOut(m_Context, vendorDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }
        public async Task<ActionResult<VendorDTO>> DeleteVendor(int id)
        {
            var vendor = await m_Context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return m_HttpController.NotFound();
            }

            m_Context.Vendors.Remove(vendor);
            await m_Context.SaveChangesAsync();

            return vendor;
        }
        private bool VendorExists(int id)
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
