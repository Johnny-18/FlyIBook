using System;
using FlyIBooking.Repositories;

namespace FlyIBooking.Services
{
    public sealed class TicketService
    {
        private readonly TicketRepository _ticketRepository;

        public TicketService(TicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }
        
        
    }
}