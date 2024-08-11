using FullStack.Api.Data;
using FullStack.Core;

namespace FullStack.Api.Commom.Api
{
    public static class BuilderExtesions
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }
    }
}
