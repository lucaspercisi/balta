using FullStack.Core.Models;
using FullStack.Core.Requests.Categories;
using FullStack.Core.Responses;

namespace FullStack.Core.Handlers
{
    public interface ICategoryHandler
    {
        Task<Response<Category?>> GetByIdAsync(GetCategoryRequest request);
        Task<Response<List<Category?>>> GetAllAsync(GetAllCategoryRequest request);
        Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
        Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
        Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
    }
}
