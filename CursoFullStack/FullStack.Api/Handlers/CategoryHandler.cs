using FullStack.Api.Data;
using FullStack.Core.Handlers;
using FullStack.Core.Models;
using FullStack.Core.Requests.Categories;
using FullStack.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace FullStack.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category()
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 201, "Categoria criada com sucesso.");
            }
            catch(Exception ex)
            {
                //seriloger
                Console.WriteLine(ex);
                return new Response<Category?>(null, 500, "Não foi possível criar a categoria.");
            }
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category == null)
                    return new Response<Category?>(null, 404, "Catogoria não encontrada.");

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 200, "Catogoria excluída com sucesso.");
            }
            catch (Exception ex)
            {
                //seriloger
                Console.WriteLine(ex);
                return new Response<Category?>(null, 500, "Não foi possível atualizar a categoria.");
            }
        }

        public Task<Response<List<Category?>>> GetAllAsync(GetAllCategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category?>> GetAsync(GetCategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if(category == null)
                    return new Response<Category?>(null, 404, "Catogoria não encontrada.");


                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category);
            }
            catch (Exception ex)
            {
                //seriloger
                Console.WriteLine(ex);
                return new Response<Category?>(null, 500, "Não foi possível atualizar a categoria.");
            }
        }
    }
}
