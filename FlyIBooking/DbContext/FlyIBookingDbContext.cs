using FlyIBooking.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlyIBooking.DbContext
{
    public sealed class FlyIBookingDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<PlaneDal> Planes { get; set; }
        
        public DbSet<TicketDal> Tickets { get; set; }
        
        public DbSet<AccountDal> Accounts { get; set; }

        public FlyIBookingDbContext(DbContextOptions<FlyIBookingDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             // optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FlyBooking;Username=postgres;Password=postgres");
             // base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AccountDal>()
                .HasKey(s => s.Id);

            builder.Entity<AccountDal>()
                .Property(s => s.Email);
            
            builder.Entity<AccountDal>()
                .Property(s => s.PasswordHash);
            
            builder.Entity<TicketDal>()
                .HasKey(s => s.Id);
            
            builder.Entity<TicketDal>()
                .Property(s => s.Price);
            
            builder.Entity<TicketDal>()
                .Property(s => s.AccountId);
            
            builder.Entity<TicketDal>()
                .Property(s => s.PlaneId);
            
            builder.Entity<TicketDal>()
                .Property(s => s.SeatNumber);
            
            builder.Entity<PlaneDal>().HasKey(s => s.Id);

            builder.Entity<TicketDal>().HasIndex(s => s.Id).IsUnique();
        }
    }
}