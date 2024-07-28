using FullStack.Core.Models;
using FullStack.Core.Requests.Transactions;
using FullStack.Core.Responses;

namespace FullStack.Core.Handlers
{
    public interface ITransactionHandler
    {
        Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
        Task<PagedResponse<List<Transaction?>>> GetAllAsync(GetAllTransactionsRequest request);
        Task<PagedResponse<List<Transaction?>>> GetAllPeriodAsync(GetTransactionsByPeriodRequest request);
        Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
        Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
        Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
    }
}
