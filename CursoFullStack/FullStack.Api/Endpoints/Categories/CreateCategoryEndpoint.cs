using FullStack.Api.Commom.Api;
using FullStack.Core.Handlers;
using FullStack.Core.Models;
using FullStack.Core.Requests.Categories;
using FullStack.Core.Responses;

namespace FullStack.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
            .WithName("Catogories: Create")
            .WithSummary("Cria uma nova categoria")
            .WithDescription("Cria uma nova categoria")
            .WithOrder(1)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request)
        {
            request.UserId = "teste@lucas.io";
            var result = await handler.CreateAsync(request);

            return result.IsSucess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
