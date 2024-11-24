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
            var callCenterToCreate = new CallCenter
            {
                CallCenterId = Guid.NewGuid().ToString(),
                CallCenterAddress = callCenter.CallCenterAddress,
                CallCenterOtherDetails = callCenter.CallCenterOtherDetails
            };
            await _context.CallCenters.AddAsync(callCenterToCreate);
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
            var callCenterInDb = await _context.CallCenters.FindAsync(id);
            if (callCenterInDb == null)
            {
                return NotFound();
            }
            callCenterInDb.CallCenterAddress = callCenter.CallCenterAddress;
            callCenterInDb.CallCenterOtherDetails = callCenter.CallCenterOtherDetails;

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
    }
}
