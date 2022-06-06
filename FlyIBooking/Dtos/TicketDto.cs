using System;
using System.ComponentModel.DataAnnotations;
using FlyIBooking.Entities;

namespace FlyIBooking.Dtos
{
    public sealed class TicketDto
    {
        [Required]
        public Guid Id { get; set; }
        
        public Guid? AccountId { get; set; }

        [Required]
        public Guid PlaneId { get; set; }
        
        [Required]
        public int SeatNumber { get; set; }
        
        [Required]
        public decimal Price { get; set; }
    }
}