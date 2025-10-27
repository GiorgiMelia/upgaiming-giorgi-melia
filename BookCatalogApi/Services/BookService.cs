using BookCatalogApi.Data;
using BookCatalogApi.Models;

namespace BookCatalogApi.Services
{
    public  class BookService
    {
        public List<Book> GetAllBooks()
        {
            var books = SampleData.Books;
            return books;
        }
        public Book CreateBook(BookCreateDto dto)
        {
            Book book = new Book
            {
                ID = SampleData.GetBookId(),
                Title = dto.Title,
                AuthorID = dto.AuthorID,
                PublicationYear = dto.PublicationYear
            };
            SampleData.Books.Add(book);
            return book;

        }
        public  Book? GetBookById(int id)
        {
            var book = SampleData.Books.FirstOrDefault(b => b.ID == id);
            return (book);
        }

        public void UpdateBook(int id, BookCreateDto input)
        {
            var book = SampleData.Books.FirstOrDefault(b => b.ID == id)!;
            book.PublicationYear = input.PublicationYear;
            book.Title = input.Title;
            book.AuthorID = input.AuthorID;
            
        }
    }
}
