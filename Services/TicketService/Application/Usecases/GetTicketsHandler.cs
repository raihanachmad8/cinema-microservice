using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TicketService.Application.DTOs.Requests;
using TicketService.Application.DTOs.Responses;
using TicketService.Application.Interfaces.Repositories;
using TicketService.Common.Exceptions;

namespace TicketService.Application.UseCases
{
    public class GetTicketsHandler
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public GetTicketsHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<TicketResponse>>> Handle(int userId, TicketQueryParams queryParams)
        {
            // Validate the query parameters (this can also be done in the controller)
            if (queryParams == null)
                throw new BadHttpRequestException("Query parameters cannot be null.");

            // Fetch tickets based on the query parameters
            var tickets = await _ticketRepository.GetTicketsAsync(queryParams, userId);

            // Map the tickets to TicketResponse (assuming you have a mapping profile set up)

            return new Response<IEnumerable<TicketResponse>>().Ok(_mapper.Map<IEnumerable<TicketResponse>>(tickets), "Tickets retrieved successfully.");
        }
    }
}