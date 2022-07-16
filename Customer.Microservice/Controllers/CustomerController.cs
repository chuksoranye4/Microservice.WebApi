using Customer.Microservice.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Customer.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IApplicationDbContext _context;

        public CustomerController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Entities.Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesToDB();
            return Ok(customer.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customer = await _context.Customers.ToListAsync();
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cus = await _context.Customers.FirstOrDefaultAsync(p => p.Id == id);
            if (cus == null) return NotFound();
            return Ok(cus);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cus = await _context.Customers.FirstOrDefaultAsync(p => p.Id == id);
            if (cus == null) return NotFound();
            _context.Customers.Remove(cus);
            await _context.SaveChangesToDB();
            return Ok(cus.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Entities.Customer customerData)
        {
            var cus = await _context.Customers.FirstOrDefaultAsync(p => p.Id == id);
            if (cus == null) return NotFound();
            cus.City = customerData.City;
            cus.Contact = customerData.Contact;
            cus.Email = customerData.Email;
            await _context.SaveChangesToDB();
            return Ok(cus.Id);
        }
    }
}
