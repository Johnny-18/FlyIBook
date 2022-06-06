using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyIBooking.Converters;
using FlyIBooking.Dtos;
using FlyIBooking.Repositories;

namespace FlyIBooking.Services
{
    public sealed class TicketService
    {
        private readonly TicketRepository _ticketRepository;
        private readonly AccountRepository _accountRepository;

        public TicketService(TicketRepository ticketRepository, AccountRepository accountRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<IReadOnlyCollection<TicketDto>> GetByAccountId(Guid accountId)
        {
            var tickets = await _ticketRepository.GetByAccountId(accountId);

            return tickets.Select(x => x.ToDto()).ToArray();
        }
        
        public async Task<IReadOnlyCollection<TicketDto>> GetByPlaneId(Guid planeId)
        {
            var tickets = await _ticketRepository.GetByPlaneId(planeId);

            return tickets.Select(x => x.ToDto()).ToArray();
        }

        public async Task<bool> TryBuyingTicket(Guid accountId, Guid ticketId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account is null)
                return false;
            
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket is null)
                return false;

            if (ticket.AccountId is not null)
                return false;

            ticket.AccountId = accountId;
            await _ticketRepository.UpdateAsync(ticket);

            return true;
        }
    }
}