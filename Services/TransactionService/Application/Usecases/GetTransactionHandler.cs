using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TransactionService.Application.DTOs.Requests;
using TransactionService.Application.DTOs.Responses;
using TransactionService.Application.Interfaces.Repositories;
using TransactionService.Common.Exceptions;

namespace TransactionService.Application.UseCases
{
    public class GetTransactionHandler
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public GetTransactionHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<TransactionResponse>>> Handle(int userId, TransactionQueryParams queryParams)
        {
            // Validate the query parameters (this can also be done in the controller)
            if (queryParams == null)
                throw new BadHttpRequestException("Query parameters cannot be null.");

            // Fetch tickets based on the query parameters
            var transaction = await _transactionRepository.GetTransactionsAsync(queryParams, userId);

            // Map the tickets to TransactionResponse (assuming you have a mapping profile set up)

            return new Response<IEnumerable<TransactionResponse>>().Ok(_mapper.Map<IEnumerable<TransactionResponse>>(transaction), "Tickets retrieved successfully.");
        }
    }
}