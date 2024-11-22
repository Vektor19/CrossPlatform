using Lab6.Database;
using Lab6.Database.Models;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace Lab6.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly CallCentersDbContext _context;
        // GET: api/contracts
        public ContractsController(CallCentersDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contract>>> Get()
        {
            var contracts = await _context.Contracts.ToListAsync();
            if (contracts == null)
            {
                return NotFound();
            }
            return Ok(contracts);
        }

        // GET api/contracts/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Contract>> Get(string id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            return Ok(contract);
        }

        // POST api/customers
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ContractModel contract)
        {
            var contractToCreate = new Contract
            {
                ContractId = Guid.NewGuid().ToString(),
                CustomerId = contract.CustomerId,
                ContractStartDate = contract.ContractStartDate,
                ContractEndDate = contract.ContractEndDate,
                OtherDetails = contract.OtherDetails
            };
            await _context.Contracts.AddAsync(contractToCreate);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] ContractModel contract)
        {
            var contractInDb = await _context.Contracts.FindAsync(id);
            if (contractInDb == null)
            {
                return NotFound();
            }
            contractInDb.CustomerId = contract.CustomerId;
            contractInDb.ContractStartDate = contract.ContractStartDate;
            contractInDb.ContractEndDate = contract.ContractEndDate;
            contractInDb.OtherDetails = contract.OtherDetails;

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        // DELETE api/contracts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            _context.Contracts.Remove(contract);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
