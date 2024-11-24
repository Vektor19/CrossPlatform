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
    public class RefCallStatusCodesController : ControllerBase
    {
        private readonly CallCentersDbContext _context;
        // GET: api/refCallStatusCodes
        public RefCallStatusCodesController(CallCentersDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefCallStatusCode>>> Get()
        {
            var refCallStatusCodes = await _context.RefCallStatusCodes.ToListAsync();
            if (refCallStatusCodes == null)
            {
                return NotFound();
            }
            return Ok(refCallStatusCodes);
        }

        // GET api/refCallStatusCodes/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<RefCallStatusCode>> Get(string id)
        {
            var refCallStatusCode = await _context.RefCallStatusCodes.FindAsync(id);
            if (refCallStatusCode == null)
            {
                return NotFound();
            }
            return Ok(refCallStatusCode);
        }

        // POST api/refCallStatusCodes
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RefCallStatusCodeModel refCallStatusCode)
        {
            var refCallStatusCodeToCreate = new RefCallStatusCode
            {
                CallStatusCode = Guid.NewGuid().ToString(),
                CallStatusDescription = refCallStatusCode.CallStatusDescription,
                CallStatusComments = refCallStatusCode.CallStatusComments
            };
            await _context.RefCallStatusCodes.AddAsync(refCallStatusCodeToCreate);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/refCallStatusCodes/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] RefCallStatusCodeModel refCallStatusCode)
        {
            var refCallStatusCodeInDb = await _context.RefCallStatusCodes.FindAsync(id);
            if (refCallStatusCodeInDb == null)
            {
                return NotFound();
            }
            refCallStatusCodeInDb.CallStatusDescription = refCallStatusCode.CallStatusDescription;
            refCallStatusCodeInDb.CallStatusComments = refCallStatusCode.CallStatusComments;
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        // DELETE api/refCallStatusCodes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var refCallStatusCode = await _context.RefCallStatusCodes.FindAsync(id);
            if (refCallStatusCode == null)
            {
                return NotFound();
            }
            _context.RefCallStatusCodes.Remove(refCallStatusCode);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
