using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using TicketService.Application.DTOs.Requests;
using TicketService.Application.UseCases;

namespace TicketService.API.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly CreateTicketHandler _createTicketHandler;

        private readonly GetTicketsHandler _getTicketsHandler;
        private readonly GetTicketDetailHandler _getTicketDetailHandler;
        private readonly IValidator<TicketRequest> _ticketRequestValidator;
        private readonly IValidator<TicketQueryParams> _ticketQueryParamsValidator;

        public TicketController(
            CreateTicketHandler createTicketHandler,
            GetTicketsHandler getTicketsHandler,
            GetTicketDetailHandler getTicketDetailHandler,
            IValidator<TicketRequest> ticketRequestValidator,
            IValidator<TicketQueryParams> ticketQueryParamsValidator
        )
        {
            _createTicketHandler = createTicketHandler;
            _getTicketsHandler = getTicketsHandler;
            _getTicketDetailHandler = getTicketDetailHandler;
            _ticketRequestValidator = ticketRequestValidator;
            _ticketQueryParamsValidator = ticketQueryParamsValidator;
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create([FromBody] TicketRequest request)
        {
            await _ticketRequestValidator.ValidateAsync(request);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _createTicketHandler.Handle(int.Parse(userId), request);
            return CreatedAtAction(nameof(Create), result);
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetTickets([FromQuery] TicketQueryParams queryParams)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _ticketQueryParamsValidator.ValidateAsync(queryParams);
            var result = await _getTicketsHandler.Handle(int.Parse(userId), queryParams);
            return Ok(result);
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetTicketDetails(int ticketId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _getTicketDetailHandler.Handle(int.Parse(userId), ticketId);
            return Ok(result);
        }
    }
}