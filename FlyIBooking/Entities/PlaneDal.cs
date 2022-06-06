using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlyIBooking.Entities
{
    public sealed class PlaneDal
    {
        [Key]
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string From { get; set; }
        
        public string To { get; set; }

        public DateTimeOffset DepartureDate { get; set; }

        public DateTimeOffset ArrivalDate { get; set; }
        
        public decimal Price { get; set; }
    }
}