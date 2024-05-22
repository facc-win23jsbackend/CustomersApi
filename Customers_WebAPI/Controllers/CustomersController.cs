using Customers_WebAPI.Context;
using Customers_WebAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customers_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomersController(CustomerContext context)
        {
            _context = context;
        }


        
        [HttpPost] // CREATE - Skapar en Customer
        public async Task<ActionResult<CustomerEntity>> CreateCustomer([FromBody] CustomerEntity customer)
        {
            // Kontrollerar om e-postadressen redan finns i databasen
            if (_context.Customers.Any(x => x.Email == customer.Email))
            {
                return BadRequest("This Email address is already taken, please use another email. ");
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }



        [HttpGet] // READ - Hämtar alla Customers till en lista
        public async Task<ActionResult<IEnumerable<CustomerEntity>>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }



        [HttpGet("{id}")] // READ - Hämtar en Customer med ett Id
        public async Task<ActionResult<CustomerEntity>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }



        [HttpPut("{id}")] // UPDATE - Uppdatera en Customer
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerEntity updatedCustomer)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
            {
                return NotFound($"No customer with id: {id}, was found.");
            }

            // Uppdaterar kundinformation
            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            customer.PhoneNumber = updatedCustomer.PhoneNumber;
            customer.Biography = updatedCustomer.Biography;
            customer.ProfileImage = updatedCustomer.ProfileImage;
            customer.StreetName = updatedCustomer.StreetName;
            customer.StreetName_2 = updatedCustomer.StreetName_2;
            customer.PostalCode = updatedCustomer.PostalCode;
            customer.City = updatedCustomer.City;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Customers.Any(x => x.Id == id))
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



        [HttpDelete("{id}")] // DELETE - Raderar en Customer med Email
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
