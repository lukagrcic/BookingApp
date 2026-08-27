using HotelManagementSystem.Application.Features.Reservations.Commands;
using HotelManagementSystem.Application.Features.Reservations.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationCommand command)
        {
            command.UserId = GetCurrentUserId();
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        private int GetCurrentUserId()
        {
            var claim = User.FindFirst("UserId");
            return int.Parse(claim!.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetReservationByIdQuery(id));
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllReservationsQuery());
            return Ok(result);
        }

        [HttpGet("mine")]
        public async Task<IActionResult> GetMine()
        {
            var result = await _mediator.Send(new GetMyReservationsQuery(GetCurrentUserId()));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateReservationCommand command)
        {
            command.ReservationId = id;
            var success = await _mediator.Send(command);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteReservationCommand(id));
            return success ? NoContent() : NotFound();
        }
    }
}
