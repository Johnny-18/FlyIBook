using FlyIBooking.DbContext;
using FlyIBooking.Entities;

namespace FlyIBooking.Repositories
{
    public sealed class PlaneRepository : Repository<PlaneDal>
    {
        public PlaneRepository(FlyIBookingDbContext context) : base(context)
        {
        }
    }
}