using Lab7.Database;
using Lab7.Database.Models;
using Lab7.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text.Json;

namespace Lab7.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallCentersController : ControllerBase
    {
        private readonly CallCentersDbContext _context;
        // GET: api/callcenters
        public CallCentersController(CallCentersDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CallCenter>>> Get()
        {
            var callcenters = await _context.CallCenters.ToListAsync();
            if (callcenters == null)
            {
                return NotFound();
            }
            return Ok(callcenters);
        }

        // GET api/callcenters/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CallCenter>> Get(string id)
        {
            var callCenter = await _context.CallCenters.FindAsync(id);
            if (callCenter == null)
            {
                return NotFound();
            }
            return Ok(callCenter);
        }

        // POST api/customers
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CallCenterModel callCenter)
        {
            var contractToCreate = new CallCenter
            {
                ContractId = Guid.NewGuid().ToString(),
                CustomerId = callCenter.CustomerId,
                ContractStartDate = callCenter.ContractStartDate,
                ContractEndDate = callCenter.ContractEndDate,
                OtherDetails = callCenter.OtherDetails
            };
            await _context.CallCenters.AddAsync(contractToCreate);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] CallCenterModel callCenter)
        {
            var contractInDb = await _context.CallCenters.FindAsync(id);
            if (contractInDb == null)
            {
                return NotFound();
            }
            contractInDb.CustomerId = callCenter.CustomerId;
            contractInDb.ContractStartDate = callCenter.ContractStartDate;
            contractInDb.ContractEndDate = callCenter.ContractEndDate;
            contractInDb.OtherDetails = callCenter.OtherDetails;

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        // DELETE api/callcenters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var callCenter = await _context.CallCenters.FindAsync(id);
            if (callCenter == null)
            {
                return NotFound();
            }
            _context.CallCenters.Remove(callCenter);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<IEnumerable<object>>> SearchContracts([FromQuery] SearchContractsRequest request)
        {
            var query = _context.CallCenters
             .Include(c => c.Customer)
             .AsQueryable();
            
            if (request.DateTime.HasValue)
            {
                query = query.Where(c => c.ContractStartDate.Date <= request.DateTime.Value.Date
                                       && c.ContractEndDate.Date >= request.DateTime.Value.Date);
            }

            if (!string.IsNullOrEmpty(request.CustomerIds))
            {
                var ids = request.CustomerIds.Split(",");
                query = query.Where(c => ids.Contains(c.CustomerId));
            }

            if (!string.IsNullOrEmpty(request.OtherDetails))
            {
                query = query.Where(c => c.OtherDetails.StartsWith(request.OtherDetails));
            }
            var results = await query
                .Select(c => new
                {
                    c.ContractId,
                    c.ContractStartDate,
                    c.ContractEndDate,
                    c.OtherDetails,
                    c.Customer,
                    c.CustomerId,
                })
                .ToListAsync();

            if (!results.Any())
            {
                return NotFound("No callcenters found with the specified criteria.");
            }

            return Ok(results);
        }
    }
}
