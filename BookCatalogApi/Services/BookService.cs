using BookCatalogApi.Data;
using BookCatalogApi.Models;

namespace BookCatalogApi.Services
{
    public static class BookService
    {
        public static async Task<List<Book>> GetAllBooksAsync()
        {
            var books = SampleData.Books;
            return await Task.FromResult(books);
        }
        public static async Task<Book> CreateBookAsync(BookCreateDto dto)
        {
            Book book = new Book
            {
                ID = SampleData.Books.Count == 0 ? 1 : SampleData.Books.Max(b => b.ID) + 1,
                Title = dto.Title,
                AuthorID = dto.AuthorID,
                PublicationYear = dto.PublicationYear
            };
            SampleData.Books.Add(book);
            return await Task.FromResult(book);

        }
        public static async Task<Book?> GetBookByIdAsync(int id)
        {
            var book = SampleData.Books.FirstOrDefault(b => b.ID == id);
            return await Task.FromResult(book);
        }

        internal static void UpdateBookAsync(int id, BookCreateDto input)
        {
            var book = SampleData.Books.FirstOrDefault(b => b.ID == id);
            book.PublicationYear = input.PublicationYear;
            book.Title = input.Title;
            book.AuthorID = input.AuthorID;
            
        }
    }
}
