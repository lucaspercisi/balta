using FullStack.Api.Commom.Api;
using FullStack.Core;
using FullStack.Core.Handlers;
using FullStack.Core.Models;
using FullStack.Core.Requests.Transactions;
using FullStack.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FullStack.Api.Endpoints.Transactions
{
    public class GetAllTransactionsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
            .WithName("Transactions: Get All")
            .WithSummary("Busca todas transaçãos do usuário")
            .WithDescription("Busca todas transaçãos do usuário")
            .WithOrder(2)
            .Produces<PagedResponse<List<Transaction>?>>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user,
            ITransactionHandler handler,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllTransactionsRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                UserId = user.Identity?.Name ?? string.Empty
            };

            var result = await handler.GetAllAsync(request);

            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
