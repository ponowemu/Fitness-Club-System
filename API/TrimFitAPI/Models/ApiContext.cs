using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using TrimFitAPI.Models;

namespace TrimFitAPI.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {}
        public DbSet<Activity> Activity { get; set; }
        public DbSet<ActivityClub> Activity_club {get; set;}
        public DbSet<Club> Club {get; set;}
        public DbSet<Customer> Customer {get; set;}
        public DbSet<CustomerClub> Customer_club { get; set;}
        public DbSet<Employee> Employee { get; set;}
        public DbSet<EmployeeClub> Employee_club { get; set;}
        public DbSet<Payment> Payment { get; set;}
        public DbSet<PaymentType> Payment_type { get; set;}
        public DbSet<Position> Position { get; set;}
        public DbSet<PositionClub> Position_club { get; set;}
        public DbSet<Registration> Registration { get; set;}
        public DbSet<Room> Room { get; set;}
        public DbSet<RoomClub> Room_club { get; set;}
        public DbSet<TimetableActivity> Timetable_activity { get; set;}
        public DbSet<Voucher> Voucher { get; set;}
        public DbSet<VoucherType> Voucher_type { get; set;}
        public DbSet<Timetable> Timetable { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<VoucherCustomer> VoucherCustomer { get; set; }
        public DbSet<UserDetail> UserDetail { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(SecData.ConnectionString);
        }

    }
}