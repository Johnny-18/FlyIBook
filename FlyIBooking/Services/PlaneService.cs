using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyIBooking.Converters;
using FlyIBooking.Dtos;
using FlyIBooking.Repositories;

namespace FlyIBooking.Services
{
    public sealed class PlaneService
    {
        private readonly PlaneRepository _planeRepository;
        private readonly TicketRepository _ticketRepository;

        public PlaneService(PlaneRepository planeRepository, TicketRepository ticketRepository)
        {
            _planeRepository = planeRepository ?? throw new ArgumentNullException(nameof(planeRepository));
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }

        public async Task<IReadOnlyCollection<PlaneDto>> GetAll()
        {
            var planes = await _planeRepository.GetAllAsync();

            return planes.Select(x => x.ToDto()).ToArray();
        }
        
        public async Task<PlaneDto?> GetById(Guid planeId)
        {
            var planes = await _planeRepository.GetByIdAsync(planeId);

            return planes?.ToDto();
        }
        
        public async Task Add(PlaneDto plane)
        {
            var tickets = plane.Tickets;

            var tasks = new List<Task>();
            
            foreach (var ticket in tickets)
            {
                ticket.PlaneId = plane.Id;
                
                tasks.Add(_ticketRepository.AddAsync(ticket.ToDal()));
            }

            await Task.WhenAll(tasks);
            
            await _planeRepository.AddAsync(plane.ToDal());
        }
        
        public async Task UpdateById(Guid planeId)
        {
            var plane = await _planeRepository.GetByIdAsync(planeId);
            if (plane is null)
            {
                return;
            }
            
            await _planeRepository.UpdateAsync(plane);
        }

        public async Task RemoveById(Guid planeId)
        {
            await _planeRepository.RemoveByIdAsync(planeId);
        }
    }
}