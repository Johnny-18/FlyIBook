using System;
using System.Collections.Generic;

namespace FlyIBooking.Dtos
{
    public sealed class PlaneDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string From { get; set; }
        
        public string To { get; set; }

        public DateTimeOffset DepartureDate { get; set; }

        public DateTimeOffset ArrivalDate { get; set; }
        
        public decimal Price { get; set; }
        
        public IReadOnlyCollection<TicketDto> Tickets { get; set; }

        public PlaneDto()
        {
            Tickets = new List<TicketDto>();
        }
    }
}