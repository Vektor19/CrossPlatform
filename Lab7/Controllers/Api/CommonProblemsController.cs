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
    public class CommonProblemsController : ControllerBase
    {
        private readonly CallCentersDbContext _context;
        // GET: api/commonProblems
        public CommonProblemsController(CallCentersDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommonProblem>>> Get()
        {
            var commonProblems = await _context.CommonProblems.ToListAsync();
            if (commonProblems == null)
            {
                return NotFound();
            }
            return Ok(commonProblems);
        }

        // GET api/commonProblems/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CommonProblem>> Get(string id)
        {
            var commonProblem = await _context.CommonProblems.FindAsync(id);
            if (commonProblem == null)
            {
                return NotFound();
            }
            return Ok(commonProblem);
        }

        // POST api/commonProblems
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CommonProblemModel commonProblem)
        {
            var commonProblemToCreate = new CommonProblem
            {
                ProblemId = Guid.NewGuid().ToString(),
                ProblemDescription = commonProblem.ProblemDescription,
                OtherProblemDetails = commonProblem.OtherProblemDetails
            };
            await _context.CommonProblems.AddAsync(commonProblemToCreate);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/commonProblems/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] CommonProblemModel commonProblem)
        {
            var commonProblemInDb = await _context.CommonProblems.FindAsync(id);
            if (commonProblemInDb == null)
            {
                return NotFound();
            }
            commonProblemInDb.ProblemDescription = commonProblem.ProblemDescription;
            commonProblemInDb.OtherProblemDetails = commonProblem.OtherProblemDetails;
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        // DELETE api/commonProblems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var commonProblem = await _context.CommonProblems.FindAsync(id);
            if (commonProblem == null)
            {
                return NotFound();
            }
            _context.CommonProblems.Remove(commonProblem);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
