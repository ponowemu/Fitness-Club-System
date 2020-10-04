using Microsoft.EntityFrameworkCore;
using Trimfit.Domain.Models;

namespace Trimfit.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {}

        public DbSet<Activity> Activity { get; set; }
        public DbSet<ActivityClub> ActivityClub {get; set;}
        public DbSet<Club> Club {get; set;}
        public DbSet<Customer> Customer {get; set;}
        public DbSet<CustomerClub> CustomerClub { get; set;}
        public DbSet<Employee> Employee { get; set;}
        public DbSet<EmployeeClub> EmployeeClub { get; set;}
        public DbSet<Payment> Payment { get; set;}
        public DbSet<PaymentType> PaymentType { get; set;}
        public DbSet<Position> Position { get; set;}
        public DbSet<PositionClub> PositionClub { get; set;}
        public DbSet<Registration> Registration { get; set;}
        public DbSet<Room> Room { get; set;}
        public DbSet<RoomClub> RoomClub { get; set;}
        public DbSet<TimetableActivity> TimetableActivity { get; set;}
        public DbSet<Voucher> Voucher { get; set;}
        public DbSet<VoucherType> VoucherType { get; set;}
        public DbSet<Timetable> Timetable { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<VoucherCustomer> VoucherCustomer { get; set; }
        public DbSet<UserDetail> UserDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Queue> Queues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VoucherCustomer>()
                .HasKey(v => v.Id);
    
            modelBuilder.Entity<VoucherCustomer>()
                .HasOne(bc => bc.Customer)
                .WithMany(b => b.Vouchers)
                .HasForeignKey(bc => bc.CustomerId);

            //modelBuilder.Entity<VoucherCustomer>()
            //    .HasOne(bc => bc.Voucher)
            //    .WithMany(c => c.Customers)
            //    .HasForeignKey(bc => bc.VoucherId);

            modelBuilder.Entity<CustomerClub>()
                .HasOne(bc => bc.Customer)
                .WithMany(b => b.Clubs)
                .HasForeignKey(bc => bc.CustomerId);

            modelBuilder.Entity<CustomerClub>()
                .HasOne(bc => bc.Club)
                .WithMany(c => c.Customers)
                .HasForeignKey(bc => bc.ClubId);

            //modelBuilder.Entity<EmployeeClub>()
            //    .HasOne(bc => bc.Employee)
            //    .WithMany(b => b.Clubs)
            //    .HasForeignKey(bc => bc.EmployeeId);

            modelBuilder.Entity<EmployeeClub>()
                .HasOne(bc => bc.Club)
                .WithMany(c => c.Employees)
                .HasForeignKey(bc => bc.ClubId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            optionsBuilder.UseNpgsql(SecData.ConnectionString);
        }
    }
}