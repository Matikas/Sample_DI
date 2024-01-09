using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using WebApplication2.Databases;
using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBooksRepository _bookDataBase;

        public BookController(IBooksRepository bookDataBase)
        {
            _bookDataBase = bookDataBase;
        }

        [HttpGet("list")]
        public IEnumerable<Book> GetAllBooksFromApi()
        {
            var data = _bookDataBase.GetAll();
            return data;
        }

        [HttpGet("single/{id}")]
        public ActionResult<Book> GetSingleBook(int id)
        {
            var booksFromRepository = _bookDataBase.GetAll();
            var data = booksFromRepository.Find(b => b.Id == id);

            if(data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpGet("pagedlist/{pageNum}")]
        public IEnumerable<Book> GetPagedList(int pageNum, int pageSize)
        {
            var data = _bookDataBase.GetAll().Skip((pageNum - 1) * pageSize).Take(pageSize);
            return data;            
        }

        [HttpGet("sortedlist")]
        public IEnumerable<Book> GetSortedBookList()
        {
            var data = _bookDataBase.GetAll().OrderBy(b => b.Title);
            return data;
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            _bookDataBase.InsertBook(book);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public void UpdateBook(Book book)
        {
            _bookDataBase.UpdateBook(book);
        }

        [HttpPut("updateTitle")]
        public void UpdateBookTitle(BookWithTitle book)
        {
            _bookDataBase.UpdateTitle(book.Id, book.Title);
        }

        [HttpDelete("{id}")]
        public void DeleteBook(int id) 
        {
            _bookDataBase.RemoveBook(id);
        }
    }
}
