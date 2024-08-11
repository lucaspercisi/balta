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
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
            .WithName("Transactions: Get All")
            .WithSummary("Busca todas transaçãos do usuário")
            .WithDescription("Busca todas transaçãos do usuário")
            .WithOrder(6)
            .Produces<PagedResponse<List<Transaction>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ITransactionHandler handler,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetTransactionsByPeriodRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await handler.GetAllPeriodAsync(request);

            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
