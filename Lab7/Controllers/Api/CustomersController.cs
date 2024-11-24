using Lab7.Database;
using Lab7.Database.Models;
using Lab7.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace Lab7.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CallCentersDbContext _context;
        // GET: api/customers
        public CustomersController(CallCentersDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }

        // GET api/customers/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            var staff = await _context.Customers.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        // POST api/customers
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CustomerModel customer)
        {
            var customerToCreate = new Customer
            {
                CustomerId = Guid.NewGuid().ToString(),
                CustomerAddressLine1 = customer.CustomerAddressLine1,
                CustomerAddressLine2 = customer.CustomerAddressLine2,
                CustomerAddressLine3 = customer.CustomerAddressLine3,
                TownCity = customer.TownCity,
                State = customer.State,
                EmailAddress = customer.EmailAddress,
                PhoneNumber = customer.PhoneNumber,
                CustomerOtherDetails = customer.CustomerOtherDetails
            };
            await _context.Customers.AddAsync(customerToCreate);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] CustomerModel customer)
        {
            var customerInDb = await _context.Customers.FindAsync(id);
            if (customerInDb == null)
            {
                return NotFound();
            }
            customerInDb.CustomerAddressLine1 = customer.CustomerAddressLine1;
            customerInDb.CustomerAddressLine2 = customer.CustomerAddressLine2;
            customerInDb.CustomerAddressLine3 = customer.CustomerAddressLine3;
            customerInDb.TownCity = customer.TownCity;
            customerInDb.State = customer.State;
            customerInDb.EmailAddress = customer.EmailAddress;
            customerInDb.PhoneNumber = customer.PhoneNumber;
            customerInDb.CustomerOtherDetails = customer.CustomerOtherDetails;
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
