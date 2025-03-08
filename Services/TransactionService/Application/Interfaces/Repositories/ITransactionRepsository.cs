using TransactionService.Application.DTOs.Requests;
using TransactionService.Domain.Entities;

namespace TransactionService.Application.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(int id);
        Task<Transaction> GetByTicketIdAsync(int ticketId);
        Task<Transaction> UpdateAsync(Transaction transaction);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<Transaction>> GetTransactionsAsync(TransactionQueryParams queryParams, int? userId);

        Task<IEnumerable<Transaction>> GetByUserIdAsync(int userId);
        Task<Transaction?> GetByUserIdAsync(int transaction, int userId);
    }
}