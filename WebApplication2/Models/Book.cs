namespace WebApplication2.Models
{
    public class Book
    {
        public Book()
        {
        }

        public Book(int id, string title, string author)
        {
            Id = id;
            Title = title;
            Author = author;
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
    }
}
