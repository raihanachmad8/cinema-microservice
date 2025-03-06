using AutoMapper;
using StudioService.Application.Interfaces.Repositories;
using StudioService.Application.DTOs.Requests;
using StudioService.Application.DTOs.Responses;
using StudioService.Application.Interfaces.Services;

namespace StudioService.Application.UseCases;

public class GetStudiosHandler
{
    private readonly IStudioRepository _studioRepository;
    private readonly ISerilog<GetStudiosHandler> _logger;
    private readonly IMapper _mapper;


    public GetStudiosHandler(IStudioRepository studioRepository, ISerilog<GetStudiosHandler> logger,
        IMapper mapper)
    {
        _studioRepository = studioRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<StudioResponse>>> Handle(StudioQueryParams queryParams)
    {
        _logger.LogInformation(
            "Retrieving studios with search: {Search}, orderBy: {OrderBy}, sort: {Sort}, page: {Page}, pageSize: {PageSize}",
            queryParams.Search, queryParams.OrderBy, queryParams.Sort, queryParams.Page, queryParams.PageSize);

        var studios = await _studioRepository.GetStudiosAsync(queryParams.Search!,
            queryParams.OrderBy!, queryParams.Sort!, queryParams.Page, queryParams.PageSize);
        return new Response<IEnumerable<StudioResponse>>().Ok(_mapper.Map<IEnumerable<StudioResponse>>(studios.Studios),
            "List of studios", studios.Metadata);
    }
}