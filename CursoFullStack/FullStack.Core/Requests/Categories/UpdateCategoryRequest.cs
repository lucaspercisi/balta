﻿namespace FullStack.Core.Requests.Categories
{
    public class UpdateCategoryRequest : Request
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
