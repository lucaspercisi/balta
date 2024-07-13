var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost(
    "/v1/transaction",
    (Request request, Handler handler) => handler.Handle(request))
    .WithName("Transactions: Create")
    .WithSummary("Cria uma nova transação")
    .Produces<Response>();


app.Run();

//Request
public class Request
{
    public string Title { get; set; } = string.Empty;
    public DateTime? PaidOrReceivedAt { get; set; }
    public int Type { get; set; }
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public string UserId { get; set; } = string.Empty;
}

//Response
public class Response
{
    public string Title { get; set; } = string.Empty;
    public DateTime? PaidOrReceivedAt { get; set; }
    public int Type { get; set; }
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public string UserId { get; set; } = string.Empty;
}

public class Handler()
{
    public Response Handle(Request request)
    {
        return new Response()
        {
            CategoryId = 4,
            Title = request.Title
        };
    }

}