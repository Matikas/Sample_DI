using WebApplication2.Models;

namespace WebApplication2.Databases
{
    public interface ITodoRepository
    {
        List<TodoItem> GetAll();
    }
}
