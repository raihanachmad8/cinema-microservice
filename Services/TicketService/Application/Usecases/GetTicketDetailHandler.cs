using AutoMapper;
using TicketService.Application.DTOs.Responses;
using TicketService.Application.Interfaces.Repositories;

namespace TicketService.Application.UseCases
{
    public class GetTicketDetailHandler
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public GetTicketDetailHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<Response<TicketResponse>> Handle(int userId, int ticketId)
        {
            var tickets = await _ticketRepository.GetTicketByIdWithSeatsAsync(ticketId, userId);
            if (tickets == null)
                throw new KeyNotFoundException("Ticket not found");
            return new Response<TicketResponse>().Ok(_mapper.Map<TicketResponse>(tickets),
                "Tickets retrieved successfully.");
        }
    }
}