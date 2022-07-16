using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Product.Microservice.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        { }
        public DbSet<Entities.Product> Products { get; set; }
        public async Task<int> SaveChangesToDB()
        {
            return await base.SaveChangesAsync();
        }
    }
}
