using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using StudioService.Application.DTOs.Requests;
using StudioService.Application.UseCases;

namespace StudioService.API.Controllers;

[Route("api/studios")]
[ApiController]
[Authorize(Roles = "Admin")]
public class StudioController : ControllerBase
{
    private readonly CreateStudioHandler _createStudioHandler;
    private readonly GetStudiosHandler _getStudiosHandler;
    private readonly UpdateStudioHandler _updateStudioHandler;
    private readonly DeleteStudioHandler _deleteStudioHandler;
    private readonly IValidator<StudioRequest> _studioRequestValidator;
    private readonly IValidator<StudioQueryParams> _studioQueryParamsValidator;

    public StudioController(
        CreateStudioHandler createStudioHandler,
        GetStudiosHandler getStudiosHandler,
        UpdateStudioHandler updateStudioHandler,
        DeleteStudioHandler deleteStudioHandler,
        IValidator<StudioRequest> studioRequestValidator,
        IValidator<StudioQueryParams> studioQueryParamsValidator
    )
    {
        _createStudioHandler = createStudioHandler;
        _getStudiosHandler = getStudiosHandler;
        _updateStudioHandler = updateStudioHandler;
        _deleteStudioHandler = deleteStudioHandler;
        _studioRequestValidator = studioRequestValidator;
        _studioQueryParamsValidator = studioQueryParamsValidator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudioRequest request)
    {
        await _studioRequestValidator.ValidateAsync(request);
        var result = await _createStudioHandler.Handle(request);
        return CreatedAtAction(nameof(Create), result);
    }

    [HttpGet]
    public async Task<IActionResult> GetStudios([FromQuery] StudioQueryParams queryParams)
    {
        await _studioQueryParamsValidator.ValidateAsync(queryParams);
        var result = await _getStudiosHandler.Handle(queryParams);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] StudioRequest request)
    {
        await _studioRequestValidator.ValidateAsync(request);
        var result = await _updateStudioHandler.Handle(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _deleteStudioHandler.Handle(id);
        return NoContent();
    }
}