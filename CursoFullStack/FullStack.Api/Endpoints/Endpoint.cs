using FullStack.Api.Commom.Api;
using FullStack.Api.Endpoints.Categories;
using FullStack.Core.Models;

namespace FullStack.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app
                .MapGroup("");

            endpoints.MapGroup("v1/categories")
                .WithTags("categories")
                //.RequireAuthorization()
                .MapEndpoint<CreateCategoryEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
