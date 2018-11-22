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
    }
}