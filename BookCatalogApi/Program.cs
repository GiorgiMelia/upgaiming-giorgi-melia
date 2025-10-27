using BookCatalogApi.Data;
using BookCatalogApi.Models;
using BookCatalogApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapGet("/api/books", async () =>
{
    var books = await BookService.GetAllBooksAsync();
    List<BookDto> result = new List<BookDto>();
    foreach (var b in books)
    {
        BookDto dto = new BookDto
        {
            ID = b.ID,
            Title = b.Title,
            AuthorName = await AuthorService.GetAuthorNameByIdAsync(b.AuthorID),
            PublicationYear = b.PublicationYear
        };
        result.Add(dto);
    }    
    return Results.Ok(result);
});

app.MapGet("/api/authors/{id:int}/books",async (int id) =>
{
    var author = await AuthorService.GetAuthorByIdAsync(id);
    if (author is null) return Results.NotFound($"Author with ID {id} not found.");

    var books = await AuthorService.GetBooksByAuthorIdAsync(id);

    return Results.Ok(books);
});

app.MapPost("/api/books",async (BookCreateDto input) =>
{
    if (input.Title.Length == 0) return Results.BadRequest("Title cannot be null or empty.");

    if (input.PublicationYear > DateTime.UtcNow.Year)
        return Results.BadRequest("PublicationYear cannot be in the future.");

    var author = await AuthorService.GetAuthorByIdAsync(input.AuthorID);
    if (author is null) return Results.BadRequest($"AuthorID {input.AuthorID} does not exist.");
    var book = await BookService.CreateBookAsync(input);
    
    return Results.Created($"/api/books/{book.ID}", book);
});
app.MapPut("/api/books/{id:int}", async (int id, BookCreateDto input) =>
{
    var book = await BookService.GetBookByIdAsync(id);
    if (book is null) return Results.NotFound($"Book with ID {id} not found.");
    if (input.Title.Length == 0) return Results.BadRequest("Title cannot be null or empty.");

    if (input.PublicationYear > DateTime.UtcNow.Year)
        return Results.BadRequest("PublicationYear cannot be in the future.");

    var author = await AuthorService.GetAuthorByIdAsync(input.AuthorID);
    if (author is null) return Results.BadRequest($"AuthorID {input.AuthorID} does not exist.");
    BookService.UpdateBookAsync(id, input);
    return Results.NoContent();
});
app.Run();
