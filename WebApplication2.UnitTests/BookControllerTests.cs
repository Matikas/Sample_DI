using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication2.Controllers;
using WebApplication2.Databases;
using WebApplication2.Models;

namespace WebApplication2.UnitTests
{
    public class BookControllerTests
    {
        [Fact]
        public void WhenCallingGetAllReturnsBooksFromRepository()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1" },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2" },
                new Book { Id = 3, Title = "Book 3", Author = "Author 3" },
            };

            var mockBooksRepository = new Mock<IBooksRepository>();
            mockBooksRepository.Setup(x => x.GetAll()).Returns(books);
            var sut = new BookController(mockBooksRepository.Object);

            // Act
            var result = sut.GetAllBooksFromApi();

            // Assert
            Assert.Equal(books, result);
        }

        [Fact]
        public void WhenCallingGetSingleBookReturnsBookFromRepository()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1" }, //0
                new Book { Id = 2, Title = "Book 2", Author = "Author 2" }, //1
                new Book { Id = 3, Title = "Book 3", Author = "Author 3" }, //2
            };

            var mockBooksRepository = new Mock<IBooksRepository>();
            mockBooksRepository.Setup(x => x.GetAll()).Returns(books);
            var sut = new BookController(mockBooksRepository.Object);

            // Act
            var response = sut.GetSingleBook(2);

            // Assert
            Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(books[1], ((OkObjectResult)response.Result).Value);
        }

        [Fact]
        public void WhenCallingGetSingleBookWithNonExistingIdReturnsNotFoundResult()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1" }, //0
                new Book { Id = 2, Title = "Book 2", Author = "Author 2" }, //1
                new Book { Id = 3, Title = "Book 3", Author = "Author 3" }, //2
            };

            var mockBooksRepository = new Mock<IBooksRepository>();
            mockBooksRepository.Setup(x => x.GetAll()).Returns(books);
            var sut = new BookController(mockBooksRepository.Object);

            // Act
            var response = sut.GetSingleBook(5);

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }
    }
}