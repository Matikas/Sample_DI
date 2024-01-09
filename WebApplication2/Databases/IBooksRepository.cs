using WebApplication2.Models;

namespace WebApplication2.Databases
{
    public interface IBooksRepository
    {
        List<Book> GetAll();
        void InsertBook(Book book);
        void UpdateBook(Book book);
        void RemoveBook(int id);
        void UpdateTitle(int id, string title);
    }
}
