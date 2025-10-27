namespace BookCatalogApi.Models
{
    public class BookCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public int AuthorID { get; set; }
        public int PublicationYear { get; set; }
    }
}
