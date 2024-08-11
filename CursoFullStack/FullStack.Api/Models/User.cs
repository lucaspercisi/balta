using Microsoft.AspNetCore.Identity;

namespace FullStack.Api.Models
{
    public class User : IdentityUser<long>
    {
        public List<IdentityRole<long>>? Roles { get; set; } 
    }
}
