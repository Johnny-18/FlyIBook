using FlyIBooking.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlyIBooking.DbContext
{
    public sealed class FlyIBookingDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<PlaneDal> Planes { get; set; }
        
        public DbSet<TicketDal> Tickets { get; set; }
        
        public DbSet<UserDal> Users { get; set; }

        public FlyIBookingDbContext(DbContextOptions<FlyIBookingDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // builder.Entity<Account>().HasKey(s => s.Id);
            //
            // builder.Entity<BetPool>()
            //     .HasMany(s => s.Bets)
            //     .WithOne(s => s.BetPool)
            //     .HasForeignKey(s => s.Event_Id);
        }
    }
}