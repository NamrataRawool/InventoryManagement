using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryDBManagement.DAL;
using InventoryManagement.Common.Models.DTO;
using InventoryManagement.Common.Models.In;
using InventoryManagement.Common.Models.Out;

namespace InventoryDBManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly InventoryDBContext _context;

        public CustomerController(InventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet("/Customers")]
        public async Task<ActionResult<IEnumerable<CustomerOut>>> GetCustomers()
        {
            var customersDto = await _context.Customers
               .AsNoTracking().
               ToListAsync();

            List<CustomerOut> customers = new List<CustomerOut>();
            foreach (var customer in customersDto)
            {
                var customerOut = new CustomerOut(customer);
                customers.Add(customerOut);
            }
            return customers;
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerOut>> GetCustomer(int id)
        {
            var customer = await _context.Customers
                        .AsNoTracking()
                        .FirstAsync(c => c.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return new CustomerOut(customer);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerDTO customerDTO)
        {
            if (id != customerDTO.ID)
            {
                return BadRequest();
            }

            _context.Entry(customerDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<CustomerOut>> PostCustomer(CustomerIn customerIn)
        {
            var customerDto = new CustomerDTO(customerIn);
            _context.Customers.Add(customerDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customerDto.ID }, customerDto);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.ID == id);
        }
    }
}
