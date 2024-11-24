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
    public class CommonSolutionsController : ControllerBase
    {
        private readonly CallCentersDbContext _context;
        // GET: api/commonSolutions
        public CommonSolutionsController(CallCentersDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommonSolution>>> Get()
        {
            var commonSolutions = await _context.CommonSolutions.ToListAsync();
            if (commonSolutions == null)
            {
                return NotFound();
            }
            return Ok(commonSolutions);
        }

        // GET api/commonSolutions/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CommonSolution>> Get(string id)
        {
            var commonSolution = await _context.CommonSolutions.FindAsync(id);
            if (commonSolution == null)
            {
                return NotFound();
            }
            return Ok(commonSolution);
        }

        // POST api/commonSolutions
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CommonSolutionModel commonSolution)
        {
            var commonSolutionToCreate = new CommonSolution
            {
                SolutionId = Guid.NewGuid().ToString(),
                SolutionDescription = commonSolution.SolutionDescription,
                OtherSolutionDetails = commonSolution.OtherSolutionDetails
            };
            await _context.CommonSolutions.AddAsync(commonSolutionToCreate);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/commonSolutions/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] CommonSolutionModel commonSolution)
        {
            var commonSolutionInDb = await _context.CommonSolutions.FindAsync(id);
            if (commonSolutionInDb == null)
            {
                return NotFound();
            }
            commonSolutionInDb.SolutionDescription = commonSolution.SolutionDescription;
            commonSolutionInDb.OtherSolutionDetails = commonSolution.OtherSolutionDetails;
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        // DELETE api/commonSolutions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var commonSolution = await _context.CommonSolutions.FindAsync(id);
            if (commonSolution == null)
            {
                return NotFound();
            }
            _context.CommonSolutions.Remove(commonSolution);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
