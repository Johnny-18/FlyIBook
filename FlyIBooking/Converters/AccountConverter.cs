using FlyIBooking.Dtos;
using FlyIBooking.Entities;

namespace FlyIBooking.Converters
{
    public static class AccountConverter
    {
        public static AccountDto ToDto(this AccountDal obj)
        {
            return obj == null
                ? null
                : new AccountDto
                {
                    Id = obj.Id,
                    Email = obj.Email,
                    PasswordHash = obj.PasswordHash
                };
        }
        
        public static AccountDal ToDal(this AccountDto obj)
        {
            return obj == null
                ? null
                : new AccountDal
                {
                    Id = obj.Id,
                    Email = obj.Email,
                    PasswordHash = obj.PasswordHash
                };
        }
    }
}