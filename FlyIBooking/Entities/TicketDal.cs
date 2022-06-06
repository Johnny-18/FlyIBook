using System;
using System.ComponentModel.DataAnnotations;

namespace FlyIBooking.Entities
{
    public sealed class TicketDal
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid? AccountId { get; set; }

        public Guid PlaneId { get; set; }
        
        public int SeatNumber { get; set; }
        
        public decimal Price { get; set; }
    }
}