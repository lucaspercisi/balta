using FullStack.Api.Data;
using FullStack.Core.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
            .Configuration
            .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(connectionString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});
builder.Services.AddTransient<Handler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost(
    "/v1/catogories",
    (Request request, Handler handler) => handler.Handle(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response>();


app.Run();

//Request
public class Request
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

//Response
public class Response
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
}


public class Handler(AppDbContext context)
{
    public Response Handle(Request request)
    {
        var category = new Category()
        {
            Title = request.Title,
            Description = request.Description,
        };

        context.Categories.Add(category);
        context.SaveChanges();

        return new Response()
        {
            Id = category.Id,
            Title = category.Title
        };
    }

}