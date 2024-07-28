﻿using FullStack.Api.Commom.Api;
using FullStack.Core.Handlers;
using FullStack.Core.Models;
using FullStack.Core.Requests.Categories;
using FullStack.Core.Responses;

namespace FullStack.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
            .WithName("Catogories: Update")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, UpdateCategoryRequest request, long id)
        {
            request.UserId = "teste@lucas.io";
            request.Id = id;
            var result = await handler.UpdateAsync(request);

            return result.IsSucess
                ? TypedResults.Ok(result.Data)
                : TypedResults.BadRequest(result.Data);
        }
    }
}
