using WebApplication2.Models;

namespace WebApplication2.Databases
{
    public interface IBooksRepository
    {
        List<Book> GetAll();
        List<Book> GetBooksPage(int pageNum, int pageSize);
        void InsertBook(Book book);
        void UpdateBook(Book book);
        void RemoveBook(int id);
        void UpdateTitle(int id, string title);
    }
}
