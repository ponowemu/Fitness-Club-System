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
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Activity_club> {get; set;}
        public DbSet<Club> {get; set;}
        public DbSet<Customer> {get; set;}
        public DbSet<Customer_club> {get; set;}
        public DbSet<Employee> {get; set;}
        public DbSet<Employee_club> {get; set;}
        public DbSet<Payment> {get; set;}
        public DbSet<Payment_type> {get; set;}
        public DbSet<Position> {get; set;}
        public DbSet<Position_club> {get; set;}
        public DbSet<Registration> {get; set;}
        public DbSet<Room> {get; set;}
        public DbSet<Room_club> {get; set;}
        public DbSet<Timetable> {get; set;}
        public DbSet<Voucher> {get; set;}
        public DbSet<Voucher_type> {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=trimfit;Username=trimfit;Password=Fit123!@#");
        }
    }
}