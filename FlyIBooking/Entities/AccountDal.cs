using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlyIBooking.Entities
{
    public sealed class AccountDal
    {
        [Key]
        public Guid Id { get; set; }
        
        public string Email { get; set; }
        
        [MinLength(6, ErrorMessage = "Password must be minimum 6 symbols length.")]
        public string PasswordHash { get; set; }
        
        public IReadOnlyCollection<TicketDal> Tickets { get; set; }

        public AccountDal()
        {
            Tickets = new List<TicketDal>();
        }
    }
}