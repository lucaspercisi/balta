using FullStack.Api.Commom.Api;
using FullStack.Core;
using FullStack.Core.Handlers;
using FullStack.Core.Models;
using FullStack.Core.Requests.Transactions;
using FullStack.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.Api.Endpoints.Transactions
{
    public class GetAllTransactionsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
            .WithName("Catogories: Get All")
            .WithSummary("Busca todas transaçãos do usuário")
            .WithDescription("Busca todas transaçãos do usuário")
            .WithOrder(8)
            .Produces<PagedResponse<List<Transaction>?>>();

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllTransactionsRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                UserId = "teste@lucas.io"
            };

            var result = await handler.GetAllAsync(request);

            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
