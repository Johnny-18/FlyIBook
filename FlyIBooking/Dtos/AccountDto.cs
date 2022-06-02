using System;

namespace FlyIBooking.Dtos
{
    public sealed class AccountDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}