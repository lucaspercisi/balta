using FullStack.Api.Data;
using FullStack.Core.Handlers;
using FullStack.Core.Models;
using FullStack.Core.Requests.Transactions;
using FullStack.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace FullStack.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                var transaction = new Transaction()
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceuveAt,
                    Title = request.Title,
                    Type = request.Type
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso.");
            }
            catch (Exception ex)
            {
                //seriloger
                Console.WriteLine(ex);
                return new Response<Transaction?>(null, 500, "Não foi possível criar a transação.");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada.");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação excluída com sucesso.");
            }
            catch (Exception ex)
            {
                //seriloger
                Console.WriteLine(ex);
                return new Response<Transaction?>(null, 500, "Não foi possível atualizar a transação.");
            }
        }

        public async Task<PagedResponse<List<Transaction?>>> GetAllAsync(GetAllTransactionsRequest request)
        {
            try
            {
                var query = context
                    .Transactions
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId)
                    .OrderBy(x => x.Title);

                var transaction = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction?>>(transaction, count, request.PageNumber, request.PageSize);
            }
            catch (Exception ex)
            {
                //seriloger
                Console.WriteLine(ex);
                return new PagedResponse<List<Transaction?>>(null, 500, "Não foi possível buscar as categorias");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                return transaction is null
                    ? new Response<Transaction?>(data: null, code: 404, message: "Categoria não encontrada.")
                    : new Response<Transaction?>(transaction);
            }
            catch (Exception ex)
            {
                //seriloger
                Console.WriteLine(ex);
                return new Response<Transaction?>(null, 500, "Não foi possível buscar a categoria.");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada.");

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.PaidOrReceivedAt = request.PaidOrReceuveAt;
                transaction.Title = request.Title;
                transaction.Type = request.Type;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);
            }
            catch (Exception ex)
            {
                //seriloger
                Console.WriteLine(ex);
                return new Response<Transaction?>(null, 500, "Não foi possível atualizar a transação.");
            }
        }
    }
}
