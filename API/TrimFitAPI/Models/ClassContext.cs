using Microsoft.EntityFrameworkCore;
using TrimFitAPI.Models;

namespace TrimFitAPI.Models
{
    public class ClassContext : DbContext
    {
        public ClassContext(DbContextOptions<ClassContext> options)
            : base(options)
        {
        }

        public DbSet<Class> Classes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=23202.p.tld.pl;Database=pg23202_trimfit;Username=pg23202_trimfit;Password=Bazatf8!");
        }
    }
}