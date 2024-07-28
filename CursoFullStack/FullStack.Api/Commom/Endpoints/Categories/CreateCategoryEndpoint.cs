using FullStack.Api.Commom.Api;
using FullStack.Core.Handlers;
using FullStack.Core.Models;
using FullStack.Core.Requests.Categories;
using FullStack.Core.Responses;

namespace FullStack.Api.Commom.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync).Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request)
        {
            var result = await handler.CreateAsync(request);
            if (result.IsSucess)
                Results.Created($"/{result.Data.Id}", result.Data);

            return Results.BadRequest(result.Data);
        }
    }
}
