using FullStack.Api.Commom.Api;
using System.Security.Claims;

namespace FullStack.Api.Endpoints.Identity
{
    public class GetRolesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/roles", Handle).RequireAuthorization();

        private static IResult Handle(ClaimsPrincipal user)
        {
            if (user.Identity is null || !user.Identity.IsAuthenticated)
                return Results.Unauthorized();

            var identity = (ClaimsIdentity)user.Identity;
            var roles = identity
                .FindAll(identity.RoleClaimType)
                .Select(x => new
                {
                    x.Issuer,
                    x.OriginalIssuer,
                    x.Type,
                    x.Value,
                    x.ValueType
                });

            return TypedResults.Json(roles);
        }
    }
}
