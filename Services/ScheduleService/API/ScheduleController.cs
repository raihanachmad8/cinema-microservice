using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using ScheduleService.Application.DTOs.Requests;
using ScheduleService.Application.UseCases;

namespace ScheduleService.API.Controllers
{
    [Route("api/Schedules")]
    [ApiController]
    // [Authorize(Roles = "Admin")]
    public class ScheduleController : ControllerBase
    {
        private readonly CreateScheduleHandler _createScheduleHandler;
        private readonly GetSchedulesHandler _getSchedulesHandler;
        private readonly UpdateScheduleHandler _updateScheduleHandler;
        private readonly DeleteScheduleHandler _deleteScheduleHandler;
        private readonly IValidator<ScheduleRequest> _scheduleRequestValidator;
        private readonly IValidator<ScheduleQueryParams> _scheduleQueryParamsValidator;

        public ScheduleController(
            CreateScheduleHandler createScheduleHandler,
            GetSchedulesHandler getSchedulesHandler,
            UpdateScheduleHandler updateScheduleHandler,
            DeleteScheduleHandler deleteScheduleHandler,
            IValidator<ScheduleRequest> scheduleRequestValidator,
            IValidator<ScheduleQueryParams> scheduleQueryParamsValidator
        )
        {
            _createScheduleHandler = createScheduleHandler;
            _getSchedulesHandler = getSchedulesHandler;
            _updateScheduleHandler = updateScheduleHandler;
            _deleteScheduleHandler = deleteScheduleHandler;
            _scheduleRequestValidator = scheduleRequestValidator;
            _scheduleQueryParamsValidator = scheduleQueryParamsValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ScheduleRequest request)
        {
            await _scheduleRequestValidator.ValidateAsync(request);
            var result = await _createScheduleHandler.Handle(request);
            return CreatedAtAction(nameof(Create), result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedules([FromQuery] ScheduleQueryParams queryParams)
        {
            await _scheduleQueryParamsValidator.ValidateAsync(queryParams);
            var result = await _getSchedulesHandler.Handle(queryParams);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ScheduleRequest request)
        {
            await _scheduleRequestValidator.ValidateAsync(request);
            var result = await _updateScheduleHandler.Handle(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _deleteScheduleHandler.Handle(id);
            return NoContent();
        }
    }
}