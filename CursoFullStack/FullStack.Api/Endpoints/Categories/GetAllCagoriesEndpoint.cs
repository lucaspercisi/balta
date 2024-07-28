using FullStack.Api.Commom.Api;
using FullStack.Core;
using FullStack.Core.Handlers;
using FullStack.Core.Models;
using FullStack.Core.Requests.Categories;
using FullStack.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.Api.Endpoints.Categories
{
    public class GetAllCagoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
            .WithName("Catogories: Get All")
            .WithSummary("Busca todas categorias do usuário")
            .WithDescription("Busca todas categorias do usuário")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();

        private static async Task<IResult> HandleAsync(
            ICategoryHandler handler,
            [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery]int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllCategoryRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                UserId = "teste@lucas.io"
            };

            var result = await handler.GetAllAsync(request);

            return result.IsSucess
                ? TypedResults.Ok(result.Data)
                : TypedResults.BadRequest(result.Data);
        }
    }
}
