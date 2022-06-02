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
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AccountDal>().HasKey(s => s.Id);

            builder.Entity<AccountDal>().HasIndex(s => s.Id).IsUnique();

            builder.Entity<AccountDal>()
                .HasMany(s => s.Tickets)
                .WithOne(s => s.Account)
                .HasForeignKey(s => s.AccountId);
        }
    }
}