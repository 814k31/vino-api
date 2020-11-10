using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vino_api.Models;

namespace vino_api.Controllers
{
    [Route("api/Batches")]
    [ApiController]
    public class BatchesController : ControllerBase
    {
        private readonly BatchContext _context;

        public BatchesController(BatchContext context)
        {
            _context = context;
        }

        // GET: api/Batches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatchDTO>>> GetBatches()
        {
            return await _context.Batches
                .Select(batch => BatchToDTO(batch))
                .ToListAsync();
        }

        // GET: api/Batches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BatchDTO>> GetBatch(long id)
        {
            var batch = await _context.Batches.FindAsync(id);

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

            var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            batch.Name = batchDTO.Name;
            batch.IsComplete = batchDTO.IsComplete;

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
                IsComplete = batchDTO.IsComplete,
                Name = batchDTO.Name
            };

            _context.Batches.Add(batch);
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
            var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            _context.Batches.Remove(batch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BatchExists(long id)
        {
            return _context.Batches.Any(batch => batch.Id == id);
        }

        private static BatchDTO BatchToDTO(Batch batch) =>
        new BatchDTO
        {
            Id = batch.Id,
            Name = batch.Name,
            IsComplete = batch.IsComplete
        };
    }
}
