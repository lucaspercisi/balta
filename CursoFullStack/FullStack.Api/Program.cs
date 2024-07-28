using FullStack.Api.Data;
using FullStack.Api.Handlers;
using FullStack.Core.Handlers;
using FullStack.Core.Models;
using FullStack.Core.Requests.Categories;
using FullStack.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
            .Configuration
            .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(connectionString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost(
    "/v1/catogories",
    async (CreateCategoryRequest request, ICategoryHandler handler) => await handler.CreateAsync(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category>>();


app.MapPut(
    "/v1/catogories/{id}",
    async (long id, UpdateCategoryRequest request, ICategoryHandler handler) =>
    {
        request.Id = id;
        return await handler.UpdateAsync(request);
    })
    .WithName("Categories: Update")
    .WithSummary("Atualiza uma categoria")
    .Produces<Response<Category>>();


app.MapDelete(
    "/v1/catogories/{id}",
    async (long id, ICategoryHandler handler) =>
    {
        var request = new DeleteCategoryRequest { Id = id };
        return await handler.DeleteAsync(request);
    })
    .WithName("Categories: Delete")
    .WithSummary("Exclui uma categoria")
    .Produces<Response<Category>>();

app.MapGet(
    "/v1/catogories/",
    async (ICategoryHandler handler) =>
    {
        var request = new GetAllCategoryRequest { UserId = "teste@lucas.io" };
        return await handler.GetAllAsync(request);
    })
    .WithName("Categories: Get All")
    .WithSummary("Retorna todas categoria de um usuário")
    .Produces<PagedResponse<List<Category>?>>();

app.MapGet(
    "/v1/catogories/{id}",
    async (long id, ICategoryHandler handler) =>
    {
        var request = new GetCategoryRequest { Id = id };
        return await handler.GetByIdAsync(request);
    })
    .WithName("Categories: Get by Id")
    .WithSummary("Retorna uma categoria")
    .Produces<Response<Category>>();


app.Run();