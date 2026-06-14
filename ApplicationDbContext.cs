using GoBL.Models;
using Microsoft.EntityFrameworkCore;
namespace GoBL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        public DbSet<Igrac> igraci { get; set; }
    }
}
