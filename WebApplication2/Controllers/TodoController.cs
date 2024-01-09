using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Databases;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ToDoRepository _todoDatabase;

        public TodoController(ToDoRepository todoDatabase)
        {
            _todoDatabase = todoDatabase;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            var data = _todoDatabase.GetAll();
            return data;
        }

        [HttpGet("{id}")]
        public TodoItem GetById(int id)
        {
            var data = _todoDatabase.GetAll().Find(t => t.Id == id);
            return data;
        }

        [HttpGet("filterBy/{typeName}")]
        public IEnumerable<TodoItem> GetAllFilteredByType(string typeName)
        {
            var data = _todoDatabase.GetAll().Where(t => t.Type == typeName);
            return data;
        }
    }
}
