using Microsoft.EntityFrameworkCore;

namespace vino_api.Models
{
    public class BatchContext : DbContext
    {
        public BatchContext(DbContextOptions<BatchContext> options) : base(options)
        {

        }

        public DbSet<Batch> Batches { get; set; }
    }
}
