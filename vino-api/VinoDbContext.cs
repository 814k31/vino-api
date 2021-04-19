using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vino_api.Domain;

namespace vino_api
{
    public class VinoDbContext : IdentityDbContext
    {
        public VinoDbContext(DbContextOptions<VinoDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Batch>();
        }
    }
}
