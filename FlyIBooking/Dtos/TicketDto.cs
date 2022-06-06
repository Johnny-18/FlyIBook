using System;
using System.ComponentModel.DataAnnotations;

namespace FlyIBooking.Dtos
{
    public sealed class TicketDto
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public Guid PlaneId { get; set; }
        
        [Required]
        public int SeatNumber { get; set; }
        
        [Required]
        public decimal Price { get; set; }
    }
}