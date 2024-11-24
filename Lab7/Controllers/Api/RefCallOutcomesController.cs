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
    public class RefCallOutcomesController : ControllerBase
    {
        private readonly CallCentersDbContext _context;
        // GET: api/refCallOutcomes
        public RefCallOutcomesController(CallCentersDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefCallOutcome>>> Get()
        {
            var refCallOutcomes = await _context.RefCallOutcomes.ToListAsync();
            if (refCallOutcomes == null)
            {
                return NotFound();
            }
            return Ok(refCallOutcomes);
        }

        // GET api/refCallOutcomes/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<RefCallOutcome>> Get(string id)
        {
            var refCallOutcome = await _context.RefCallOutcomes.FindAsync(id);
            if (refCallOutcome == null)
            {
                return NotFound();
            }
            return Ok(refCallOutcome);
        }

        // POST api/refCallOutcomes
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RefCallOutcomeModel refCallOutcome)
        {
            var refCallOutcomeToCreate = new RefCallOutcome
            {
                CallOutcomeCode = Guid.NewGuid().ToString(),
                CallOutcomeDescription = refCallOutcome.CallOutcomeDescription,
                OtherCallOutcomeDetails = refCallOutcome.OtherCallOutcomeDetails
            };
            await _context.RefCallOutcomes.AddAsync(refCallOutcomeToCreate);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/refCallOutcomes/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] RefCallOutcomeModel refCallOutcome)
        {
            var refCallOutcomeInDb = await _context.RefCallOutcomes.FindAsync(id);
            if (refCallOutcomeInDb == null)
            {
                return NotFound();
            }
            refCallOutcomeInDb.CallOutcomeDescription = refCallOutcome.CallOutcomeDescription;
            refCallOutcomeInDb.OtherCallOutcomeDetails = refCallOutcome.OtherCallOutcomeDetails;
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        // DELETE api/refCallOutcomes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var refCallOutcome = await _context.RefCallOutcomes.FindAsync(id);
            if (refCallOutcome == null)
            {
                return NotFound();
            }
            _context.RefCallOutcomes.Remove(refCallOutcome);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
