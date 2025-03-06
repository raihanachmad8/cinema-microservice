using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using MovieService.Application.DTOs.Requests;
using MovieService.Application.UseCases;

namespace MovieService.API.Controllers;

[Route("api/Movies")]
[ApiController]
[Authorize(Roles = "Admin")]
public class MovieController : ControllerBase
{
    private readonly CreateMovieHandler _createMovieHandler;
    private readonly GetMoviesHandler _getMoviesHandler;
    private readonly UpdateMovieHandler _updateMovieHandler;
    private readonly DeleteMovieHandler _deleteMovieHandler;
    private readonly GetAllGenresHandler _getGenreHandler;
    private readonly FindByGenreHandler _findByGenreHandler;
    private readonly IValidator<MovieRequest> _movieRequestValidator;
    private readonly IValidator<MovieQueryParams> _movieQueryParamsValidator;
    private readonly IValidator<string> _genreRoute;

    public MovieController(
        CreateMovieHandler createMovieHandler,
        GetMoviesHandler getMoviesHandler,
        UpdateMovieHandler updateMovieHandler,
        DeleteMovieHandler deleteMovieHandler,
        GetAllGenresHandler getGenreHandler,
        FindByGenreHandler findByGenreHandler,
        IValidator<MovieRequest> movieRequestValidator,
        IValidator<MovieQueryParams> movieQueryParamsValidator,
        IValidator<string> genreRoute
    )
    {
        _createMovieHandler = createMovieHandler;
        _getMoviesHandler = getMoviesHandler;
        _updateMovieHandler = updateMovieHandler;
        _deleteMovieHandler = deleteMovieHandler;
        _getGenreHandler = getGenreHandler;
        _findByGenreHandler = findByGenreHandler;
        _movieRequestValidator = movieRequestValidator;
        _movieQueryParamsValidator = movieQueryParamsValidator;
        _genreRoute = genreRoute;
    }

    [HttpGet("genre")]
    public async Task<ActionResult<IEnumerable<string>>> GetAllGenres()
    {
        var genres = await _getGenreHandler.Handle();
        return Ok(genres);
    }

    [HttpGet("genre/{genre}")]
    public async Task<ActionResult> FindByGenre([FromRoute] string genre)
    {
        await _genreRoute.ValidateAsync(genre);
        var genres = await _findByGenreHandler.Handle(genre);
        return Ok(genres);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MovieRequest request)
    {
        await _movieRequestValidator.ValidateAsync(request);
        var result = await _createMovieHandler.Handle(request);
        return CreatedAtAction(nameof(Create), result);
    }

    [HttpGet]
    public async Task<IActionResult> GetMovies([FromQuery] MovieQueryParams queryParams)
    {
        await _movieQueryParamsValidator.ValidateAsync(queryParams);
        var result = await _getMoviesHandler.Handle(queryParams);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute]int id, [FromBody] MovieRequest request)
    {
        await _movieRequestValidator.ValidateAsync(request);
        var result = await _updateMovieHandler.Handle(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        await _deleteMovieHandler.Handle(id);
        return NoContent();
    }
}