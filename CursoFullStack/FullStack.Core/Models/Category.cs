using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStack.Core.Models
{
    public class Category : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
