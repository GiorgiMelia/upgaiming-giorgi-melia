var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

/*GET /api/books

GET /api/authors/{id}/books

POST /api/books
*/