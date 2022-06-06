using System.Collections.Generic;
using System.Threading.Tasks;
using FlyIBooking.DbContext;
using FlyIBooking.Entities;

namespace FlyIBooking.Repositories
{
    public sealed class PlaneRepository : Repository<PlaneDal>
    {
        public PlaneRepository(FlyIBookingDbContext context) : base(context)
        {
        }

        public async Task AddRangeAsync(IReadOnlyCollection<PlaneDal> dals)
        {
            Context.Planes.AddRange(dals);
            await Context.SaveChangesAsync();
        }
    }
}