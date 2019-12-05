using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryDBManagement.DAL;
using InventoryManagement.Common.Models;
using InventoryManagement.Common.Models.Out;
using InventoryManagement.Common.Models.In;
using InventoryManagement.Common.Models.DTO;

namespace InventoryDBManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly InventoryDBContext _context;

        public CategoryController(InventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet("/Categories")]
        public async Task<ActionResult<IEnumerable<CategoryOut>>> GetCategories()
        {
            var categoriesDto = await _context.Categories
                .AsNoTracking()
                .ToListAsync();
            List<CategoryOut> categories = new List<CategoryOut>();
            foreach (var category in categoriesDto)
            {
                var categoryOut = new CategoryOut(category);
                categories.Add(categoryOut);
            }
            return categories;
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryOut>> GetCategory(int id)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryID == id);

            if (category == null)
            {
                return NotFound();
            }

            return new CategoryOut(category);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryID)
            {
                return BadRequest();
            }

            _context.Entry(categoryDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryOut>> PostCategory(CategoryIn categoryIn)
        {
            try
            {
                CategoryDTO categoryDTO = new CategoryDTO(categoryIn);
                _context.Categories.Add(categoryDTO);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCategory", new { id = categoryDTO.CategoryID }, categoryDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }
    }
}
