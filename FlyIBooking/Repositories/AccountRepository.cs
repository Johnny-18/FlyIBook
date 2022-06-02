using System.Threading.Tasks;
using FlyIBooking.DbContext;
using FlyIBooking.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlyIBooking.Repositories
{
    public sealed class AccountRepository : Repository<AccountDal>
    {
        public AccountRepository(FlyIBookingDbContext context) : base(context)
        {
        }

        public async Task<AccountDal> GetAccountByEmailAsync(string email)
        {
            return await Set.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}