using FullStack.Api.Commom.Api;
using FullStack.Core.Handlers;
using FullStack.Core.Models;
using FullStack.Core.Requests.Categories;
using FullStack.Core.Responses;

namespace FullStack.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
            .WithName("Catogories: Get by Id")
            .WithSummary("Busca uma categoria por Id")
            .WithDescription("Busca uma categoria por Id")
            .WithOrder(4)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
        {
            var request = new GetCategoryByIdRequest
            {
                Id = id,
                UserId = "teste@lucas.io"
            };

            var result = await handler.GetByIdAsync(request);

            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
