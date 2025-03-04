using AutoMapper;
using MovieService.Application.Interfaces.Repositories;
using MovieService.Application.DTOs.Requests;
using MovieService.Application.DTOs.Responses;

namespace MovieService.Application.UseCases;

public class GetMoviesHandler
{
    private readonly IMovieRepository _movieRepository;
    private readonly ILogger<GetMoviesHandler> _logger;
    private readonly IMapper _mapper;


    public GetMoviesHandler(IMovieRepository MovieRepository, ILogger<GetMoviesHandler> logger,
        IMapper mapper)
    {
        _movieRepository = MovieRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<MovieResponse>>> Handle(MovieQueryParams queryParams)
    {
        _logger.LogInformation(
            "Retrieving Movies with search: {Search}, orderBy: {OrderBy}, sort: {Sort}, page: {Page}, pageSize: {PageSize}",
            queryParams.Search, queryParams.OrderBy, queryParams.Sort, queryParams.Page, queryParams.PageSize);

        var Movies = await _movieRepository.GetMoviesAsync(queryParams.Search!,
            queryParams.OrderBy!, queryParams.Sort!, queryParams.Page, queryParams.PageSize);
        return new Response<IEnumerable<MovieResponse>>().Ok(_mapper.Map<IEnumerable<MovieResponse>>(Movies.Movies),
            "List of Movies", Movies.Metadata);
    }
}