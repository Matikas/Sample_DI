using WebApplication2.Models;

namespace WebApplication2.Databases
{
    public class BooksRepository : IBooksRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<BooksRepository> _logger;

        public BooksRepository(ApplicationDbContext applicationDbContext, ILogger<BooksRepository> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        public List<Book> GetAll()
        {
            return _applicationDbContext.Books.ToList();
        }

        public void InsertBook(Book book)
        {
            _logger.LogInformation($"Inserting book {book.Title} by {book.Author}");
            _applicationDbContext.Books.Add(book);
            _applicationDbContext.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            var bookFromDb = _applicationDbContext.Books.FirstOrDefault(b => b.Id == book.Id);

            if (bookFromDb == null)
            {
                _logger.LogError($"Book with ID {book.Id} was not found in database");
                throw new ArgumentException($"Book with ID {book.Id} was not found in database");
            }

            if (!string.IsNullOrEmpty(book.Author))
            {
                bookFromDb.Author = book.Author;
            }
            if (!string.IsNullOrEmpty(book.Title))
            {
                bookFromDb.Title = book.Title;
            }

            _applicationDbContext.SaveChanges();
        }

        public void RemoveBook(int id)
        {
            var bookFromDb = _applicationDbContext.Books.FirstOrDefault(b => b.Id == id);
            _applicationDbContext.Books.Remove(bookFromDb);
            _applicationDbContext.SaveChanges();
        }

        public void UpdateTitle(int id, string title)
        {
            var bookFromDb = _applicationDbContext.Books.FirstOrDefault(b => b.Id == id);

            if (bookFromDb == null)
            {
                _logger.LogWarning($"Book with ID {id} was not found in database");
                throw new ArgumentException($"Book with ID {id} was not found in database");
            }

            bookFromDb.Title = title;

            _applicationDbContext.SaveChanges();
        }
    }
}
