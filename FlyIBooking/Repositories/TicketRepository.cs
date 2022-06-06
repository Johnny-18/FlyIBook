using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FlyIBooking.DbContext;
using FlyIBooking.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlyIBooking.Repositories
{
    public sealed class TicketRepository : Repository<TicketDal>
    {
        public TicketRepository(FlyIBookingDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TicketDal>> GetByAccountId(Guid accountId)
        {
            return await Context.Tickets.Where(x => x.AccountId == accountId).ToArrayAsync();
        }
        
        public async Task<IEnumerable<TicketDal>> GetByPlaneId(Guid planeId)
        {
            return await Context.Tickets.Where(x => x.PlaneId == planeId).ToArrayAsync();
        }
    }
}