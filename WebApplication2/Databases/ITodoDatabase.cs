using WebApplication2.Models;

namespace WebApplication2.Databases
{
    public interface ITodoDatabase
    {
        List<TodoItem> GetAll();
    }
}
