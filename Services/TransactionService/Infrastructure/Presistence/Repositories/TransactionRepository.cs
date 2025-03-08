using Microsoft.EntityFrameworkCore;
using TransactionService.Application.DTOs.Requests;
using TransactionService.Domain.Entities;
using TransactionService.Application.Interfaces.Repositories;
using TransactionService.Infrastructure.Persistence;

namespace TransactionService.Infrastructure.Presistence.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext _context;

        public TransactionRepository(TransactionDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            await _context.Set<Transaction>().AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Set<Transaction>().ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _context.Set<Transaction>().FindAsync(id);
        }

        public async Task<Transaction> GetByTicketIdAsync(int ticketId)
        {
            return await _context.Set<Transaction>().FindAsync(ticketId);
        }

        public async Task<Transaction> UpdateAsync(Transaction transaction)
        {
            _context.Set<Transaction>().Update(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transaction = await GetByIdAsync(id);
            if (transaction == null)
                return false;

            _context.Set<Transaction>().Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(TransactionQueryParams queryParams,
            int? userId)
        {
            // Mulai query dasar untuk transaksi
            var query = _context.Transactions.AsQueryable();

            // Menyaring berdasarkan status pembayaran jika disediakan
            if (queryParams.PaymentStatus != null && queryParams.PaymentStatus.Count > 0)
            {
                query = query.Where(transacttion => queryParams.PaymentStatus.Contains(transacttion.PaymentStatus.ToString()));
            }

            // Menyaring berdasarkan userId jika disediakan
            if (userId != null)
            {
                query = query.Where(transaction => transaction.UserId == userId);
            }

            // Menerapkan pengurutan jika disediakan
            if (!string.IsNullOrEmpty(queryParams.OrderBy))
            {
                query = queryParams.Sort == "asc"
                    ? query.OrderBy(transaction => EF.Property<object>(transaction, queryParams.OrderBy))
                    : query.OrderByDescending(transaction => EF.Property<object>(transaction, queryParams.OrderBy));
            }

            // Terapkan paginasi
            var transactions = await query.Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return transactions;
        }


        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(int userId)
        {
            return await _context.Set<Transaction>().Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<Transaction?> GetByUserIdAsync(int transaction, int userId)
        {
            return await _context.Set<Transaction>().FirstOrDefaultAsync(t => t.Id == transaction && t.UserId == userId);
        }
    }
}