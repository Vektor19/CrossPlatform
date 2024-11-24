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
    public class StaffsController : ControllerBase
    {
        private readonly CallCentersDbContext _context;
        // GET: api/staffs
        public StaffsController(CallCentersDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> Get()
        {
            var staffs = await _context.Staffs.ToListAsync();
            if (staffs == null)
            {
                return NotFound();
            }
            return Ok(staffs);
        }

        // GET api/staffs/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Staff>> Get(string id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        // POST api/staffs
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StaffModel staff)
        {
            var staffToCreate = new Staff
            {
                EmailAddress = staff.EmailAddress,
                PhoneNumber = staff.PhoneNumber,
                OtherDetails = staff.OtherDetails,
                StaffId = Guid.NewGuid().ToString()
            };
            await _context.Staffs.AddAsync(staffToCreate);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/staffs/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] StaffModel staff)
        {
            var staffInDb = await _context.Staffs.FindAsync(id);
            if (staffInDb == null)
            {
                return NotFound();
            }
            staffInDb.EmailAddress = staff.EmailAddress;
            staffInDb.PhoneNumber = staff.PhoneNumber;
            staffInDb.OtherDetails = staff.OtherDetails;
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        // DELETE api/staffs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            _context.Staffs.Remove(staff);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
