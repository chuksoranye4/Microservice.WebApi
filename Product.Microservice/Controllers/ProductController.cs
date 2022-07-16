using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Microservice.Data;
using System.Threading.Tasks;

namespace Product.Microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        public ProductController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Entities.Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesToDB();
            return Ok(product.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var prod = await _context.Products.ToListAsync();
            if (prod is null) return NotFound();
            return Ok(prod);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (prod is null) return NotFound();
            return Ok(prod);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (prod is null) return NotFound();
            _context.Products.Remove(prod);
            await _context.SaveChangesToDB();
            return Ok(prod.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Entities.Product product)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (prod is null) return NotFound();
            prod.Name = product.Name;
            prod.Rate = product.Rate;
            await _context.SaveChangesToDB();
            return Ok(prod.Id);
        }

    }
}
