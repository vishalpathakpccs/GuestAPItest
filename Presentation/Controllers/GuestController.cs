
using Application.Commands.AddGuest;
using Application.Queries.GetGuestById;
using Application.Commands.AddPhone;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GuestController> _logger;

        public GuestController(IMediator mediator, ILogger<GuestController> logger)
        {
            _mediator = mediator;
            _logger = logger;   

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGuest([FromBody] AddGuestCommand command)
        {
            var result = await _mediator.Send(command);
            if (!string.IsNullOrEmpty(result.Item1.ErrorMessage))
            {
                _logger.LogError("Failed to add guest: {Message}", result.Item1.ErrorMessage);
                return BadRequest(result.Item1.ErrorMessage);
            }

            _logger.LogInformation("Added guest");
            return Ok(result.Item2);
        }


        [HttpPost("{id}/add-phone")]
        public async Task<IActionResult> AddPhone(Guid id, [FromBody] string phoneNumber)
        {
            var command = new AddPhoneCommand { Id = id, PhoneNumber = phoneNumber };
            var result = await _mediator.Send(command);
            if (result != ValidationResult.Success)
            {
                _logger.LogError("Failed to add phone number: {Message}", result.ErrorMessage);
                return BadRequest(result.ErrorMessage);
            }

            _logger.LogInformation("Added phone number to guest with ID: {Id}", id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuestById(Guid id)
        {
            var query = new GetGuestByIdQuery { Id = id };
            var guest = await _mediator.Send(query);
            if (guest == null)
            {
                _logger.LogWarning("Guest not found with ID: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Retrieved guest with ID: {Id}", id);
            return Ok(guest);
        }

        [HttpGet("GetAllGuests")]
        public async Task<IActionResult> GetAllGuests()
        {
            var query = new GetAllGuestsQuery();
            var guests = await _mediator.Send(query);
            _logger.LogInformation("Retrieved all guests.");
            return Ok(guests);
        }
        
    }
}
