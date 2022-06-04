using Demo.Foundation.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Demo.WebHost.Data
{
    public class AppDbContext : BaseDbContext<AppDbContext>
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }
    }
}
