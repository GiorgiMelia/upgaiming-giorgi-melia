namespace BookCatalogApi.Models
{
    public class BookDto
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
    }
}
