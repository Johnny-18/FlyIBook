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
    [Route("api/[controller]")]
    public sealed class FlyIBookingController : ControllerBase
    {
        private readonly PlaneService _planeService;

        public FlyIBookingController(PlaneService planeService)
        {
            _planeService = planeService ?? throw new ArgumentNullException(nameof(planeService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var flights = await _planeService.GetAll();

            return flights.Any()
                ? Ok(flights)
                : NotFound();
        }
        
        [HttpGet("{planeId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid planeId)
        {
            var flight = await _planeService.GetById(planeId);

            return flight is not null
                ? Ok(flight)
                : NotFound();
        }
        
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Add([FromBody] PlaneDto plane)
        {
            await _planeService.Add(plane);

            return Ok();
        }

        [Authorize]
        [HttpPost("{planeId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid planeId)
        {
            await _planeService.UpdateById(planeId);

            return Ok();
        }
        
        [Authorize]
        [HttpDelete("{planeId:guid}")]
        public async Task<IActionResult> RemoveById([FromRoute] Guid planeId)
        {
            await _planeService.RemoveById(planeId);

            return Ok();
        }
    }
}