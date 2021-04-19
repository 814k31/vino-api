using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vino_api.Domain;
using Microsoft.AspNetCore.Authorization;

namespace vino_api.Controllers
{
    [ApiController]
    [Route("api/Batches")]
    public class BatchesController : Controller
    {
        private readonly VinoDbContext _context;
        public BatchesController(VinoDbContext context)
        {
            _context = context;
        }

        // GET: api/Batches
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatchDTO>>> GetBatches()
        {
            return await _context.Set<Batch>()
                .Select(batch => BatchToDTO(batch))
                .ToListAsync();
        }

        // GET: api/Batches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BatchDTO>> GetBatch(long id)
        {
            
            var batch = await _context.Set<Batch>().FindAsync(id);

            if (batch == null)
            {
                return NotFound();
            }

            return BatchToDTO(batch);
        }

        // PUT: api/Batches/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatch(long id, BatchDTO batchDTO)
        {
            if (id != batchDTO.Id)
            {
                return BadRequest();
            }

            var batch = await _context.Set<Batch>().FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            batch.DateUpdated = DateTime.Now;
            batch.Name = batchDTO.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BatchExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Batches
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Batch>> PostBatch(BatchDTO batchDTO)
        {
            var batch = new Batch
            {
                DateCreated = DateTime.Now,
                DateUpdated = null,
                Name = batchDTO.Name
            };

            await _context.Set<Batch>().AddAsync(batch);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBatch),
                new { id = batch.Id },
                BatchToDTO(batch)
            );
        }

        // DELETE: api/Batches/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Batch>> DeleteBatch(long id)
        {
            var batch = await _context.Set<Batch>().FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            _context.Set<Batch>().Remove(batch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BatchExists(long id)
        {
            return _context.Set<Batch>().Any(batch => batch.Id == id);
        }

        private static BatchDTO BatchToDTO(Batch batch) =>
        new BatchDTO
        {
            Id = batch.Id,
            DateCreated = batch.DateCreated,
            DateUpdated = batch.DateUpdated,
            Name = batch.Name,
        };
    }
}
