﻿using FullStack.Api.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FullStack.Api.Commom.Api
{
    public static class AppExtension
    {
        public static void ConfigureDevEnvironment(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapSwagger().RequireAuthorization();
        }

        public static void UseSecurity(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapIdentityApi<User>();

            app.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapPost("/logout", async (SignInManager<User> signInManager) =>
                {
                    await signInManager.SignOutAsync();
                    return Results.Ok();
                }).RequireAuthorization();

            app.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapGet("/roles", (ClaimsPrincipal user) =>
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

                }).RequireAuthorization();


        }
    }
}
