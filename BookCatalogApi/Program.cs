using BookCatalogApi.Data;
using BookCatalogApi.Models;
using BookCatalogApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapGet("/api/books",async  (BookService bookService,AuthorService authorService) =>
{
    var books =  bookService.GetAllBooks();
    //I would use async await if we had to work with real database  
    var authors = await authorService.GetAuthorsOfBooks(books.Select(b=>b.AuthorID).ToList());
    List<BookDto> result = new List<BookDto>();

    foreach (var b in books)
    {
        BookDto dto = new BookDto
        {
            ID = b.ID,
            Title = b.Title,
            AuthorName = authors.FirstOrDefault(a=>a.ID == b.AuthorID)!.Name,
            PublicationYear = b.PublicationYear
        };
        result.Add(dto);
    }    
    return Results.Ok(result);
});

app.MapGet("/api/authors/{id:int}/books", (int id, AuthorService authorService) =>
{
    var author = authorService.GetAuthorById(id);
    if (author is null) return Results.NotFound($"Author with ID {id} not found.");

    var books = authorService.GetBooksByAuthorId(id);

    return Results.Ok(books);
});

app.MapPost("/api/books", (BookCreateDto input, BookService bookService, AuthorService authorService) =>
{
    if (input.Title.Length == 0) return Results.BadRequest("Title cannot be null or empty.");

    if (input.PublicationYear > DateTime.UtcNow.Year)
        return Results.BadRequest("PublicationYear cannot be in the future.");

    var author =  authorService.GetAuthorById(input.AuthorID);
    if (author is null) return Results.BadRequest($"AuthorID {input.AuthorID} does not exist.");
    var book =  bookService.CreateBook(input);
    
    return Results.Created($"/api/books/{book.ID}", book);
});
app.MapPut("/api/books/{id:int}",  (int id, BookCreateDto input,BookService bookService,AuthorService authorService) =>
{
    var book = bookService.GetBookById(id);
    if (book is null) return Results.NotFound($"Book with ID {id} not found.");
    if (input.Title.Length == 0) return Results.BadRequest("Title cannot be null or empty.");

    if (input.PublicationYear > DateTime.UtcNow.Year)
        return Results.BadRequest("PublicationYear cannot be in the future.");

    var author =  authorService.GetAuthorById(input.AuthorID);
    if (author is null) return Results.BadRequest($"AuthorID {input.AuthorID} does not exist.");
    bookService.UpdateBook(id, input);
    return Results.NoContent();
});
app.Run();
