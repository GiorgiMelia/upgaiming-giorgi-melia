using BookCatalogApi.Data;
using BookCatalogApi.Models;

namespace BookCatalogApi.Services
{
    public  class AuthorService
    {
        public  string GetAuthorNameById(int Id)
        {
            var author = SampleData.Authors.FirstOrDefault(a => a.ID == Id);
            return author!.Name;
        }
        public  List<Book> GetBooksByAuthorId(int authorId)
        {
            var books = SampleData.Books.Where(b => b.AuthorID == authorId).ToList();
            return books;
        }
        public  Author? GetAuthorById(int Id)
        {
            var author = SampleData.Authors.FirstOrDefault(a => a.ID == Id);
            return author;
        }
        //This would be case if I used Database and not sample data
        public async Task<List<Author>> GetAuthorsOfBooks(List<int> authorIds)
        {

            return  SampleData.Authors.Where(a => authorIds.Contains(a.ID)).ToList();
        }
    }
}
