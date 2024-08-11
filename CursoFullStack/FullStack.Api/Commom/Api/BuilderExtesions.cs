using FullStack.Api.Data;
using FullStack.Api.Handlers;
using FullStack.Api.Models;
using FullStack.Core;
using FullStack.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FullStack.Api.Commom.Api
{
    public static class BuilderExtesions
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
            Configuration.FrontEndUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });
        }

        public static void AddSecurity(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
            builder.Services.AddAuthorization();
        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(x => { x.UseSqlServer(Configuration.ConnectionString); });

            builder.Services
                .AddIdentityCore<User>()
                .AddRoles<IdentityRole<long>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddApiEndpoints();
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
            builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(op 
                => op.AddPolicy(ApiConfiguration.CorsPolicyName,policy 
                    => policy
                        .WithOrigins([Configuration.BackendUrl, Configuration.FrontEndUrl])
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials())
                );
        }
    }
}
