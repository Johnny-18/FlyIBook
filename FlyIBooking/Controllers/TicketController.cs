using System;
using System.Linq;
using System.Threading.Tasks;
using FlyIBooking.Dtos;
using FlyIBooking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlyIBooking.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public sealed class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
        }

        [HttpGet("account/{accountId:guid}")]
        public async Task<IActionResult> GetByAccountId([FromRoute] Guid accountId)
        {
            var tickets = await _ticketService.GetByAccountId(accountId);

            return tickets.Any()
                ? Ok(tickets)
                : NoContent();
        }
        
        [HttpGet("plane/{planeId:guid}")]
        public async Task<IActionResult> GetByPlaneId([FromRoute] Guid planeId)
        {
            var tickets = await _ticketService.GetByAccountId(planeId);

            return tickets.Any()
                ? Ok(tickets)
                : NoContent();
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyingByPlayer([FromBody] BuyTicketDto dto)
        {
            var result = await _ticketService.TryBuyingTicket(dto.AccountId, dto.TicketId);

            return result
                ? Ok()
                : BadRequest("Something wrong when user try to buy ticket");
        }
    }
}