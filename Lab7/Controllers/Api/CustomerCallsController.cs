using Lab7.Database;
using Lab7.Database.Models;
using Lab7.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab7.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCallsController : ControllerBase
    {
        private readonly CallCentersDbContext _context;
        // GET: api/customerCalls
        public CustomerCallsController(CallCentersDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerCall>>> Get()
        {
            var customerCalls = await _context.CustomerCalls.ToListAsync();
            if (customerCalls == null)
            {
                return NotFound();
            }
            return Ok(customerCalls);
        }

        // GET api/customerCalls/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CustomerCall>> Get(string id)
        {
            var customerCall = await _context.CustomerCalls.FindAsync(id);
            if (customerCall == null)
            {
                return NotFound();
            }
            return Ok(customerCall);
        }

        // POST api/customerCalls
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CustomerCallModel customerCall)
        {
            var customerCallToCreate = new CustomerCall
            {
                CallId= Guid.NewGuid().ToString(),
                StaffId = customerCall.StaffId,
                CallDateTime = customerCall.CallDateTime,
                CallDescription = customerCall.CallDescription,
                CallOtherDetails = customerCall.CallOtherDetails,
                CallOutcomeCode = customerCall.CallOutcomeCode,
                CallStatusCode = customerCall.CallStatusCode,
                CustomerId = customerCall.CustomerId,
                RecommendedSolutionId = customerCall.RecommendedSolutionId,
                TailoredSolutionDescription = customerCall.TailoredSolutionDescription
            };
            await _context.CustomerCalls.AddAsync(customerCallToCreate);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/customerCalls/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] CustomerCallModel customerCall)
        {
            var customerCallInDb = await _context.CustomerCalls.FindAsync(id);
            if (customerCallInDb == null)
            {
                return NotFound();
            }
            customerCallInDb.StaffId = customerCall.StaffId;
            customerCallInDb.CallDateTime = customerCall.CallDateTime;
            customerCallInDb.CallDescription = customerCall.CallDescription;
            customerCallInDb.CallOtherDetails = customerCall.CallOtherDetails;
            customerCallInDb.CallOutcomeCode = customerCall.CallOutcomeCode;
            customerCallInDb.CallStatusCode = customerCall.CallStatusCode;
            customerCallInDb.CustomerId = customerCall.CustomerId;
            customerCallInDb.RecommendedSolutionId = customerCall.RecommendedSolutionId;
            customerCallInDb.TailoredSolutionDescription = customerCall.TailoredSolutionDescription;

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        // DELETE api/customerCalls/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var customerCall = await _context.CustomerCalls.FindAsync(id);
            if (customerCall == null)
            {
                return NotFound();
            }
            _context.CustomerCalls.Remove(customerCall);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
