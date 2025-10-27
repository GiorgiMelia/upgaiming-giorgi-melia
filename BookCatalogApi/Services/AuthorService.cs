using BookCatalogApi.Data;
using BookCatalogApi.Models;

namespace BookCatalogApi.Services
{
    public static class AuthorService
    {
        public static async Task<string> GetAuthorNameByIdAsync(int Id)
        {
            var author = SampleData.Authors.FirstOrDefault(a => a.ID == Id);
            return await Task.FromResult(author!.Name);
        }
        public static async Task<List<Book>> GetBooksByAuthorIdAsync(int authorId)
        {
            var books = SampleData.Books.Where(b => b.AuthorID == authorId).ToList();
            return await Task.FromResult(books);
        }
        public static async Task<Author?> GetAuthorByIdAsync(int Id)
        {
            var author = SampleData.Authors.FirstOrDefault(a => a.ID == Id);
            return await Task.FromResult(author);
        }
    }
}
