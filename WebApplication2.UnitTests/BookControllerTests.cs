using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using WebApplication2.Controllers;
using WebApplication2.Databases;
using WebApplication2.Models;

namespace WebApplication2.UnitTests
{
    public class BookControllerTests
    {
        private readonly Mock<IBooksRepository> _mockBooksRepository;
        private readonly BookController _sut;

        public BookControllerTests()
        {
            _mockBooksRepository = new Mock<IBooksRepository>();
            _sut = new BookController(_mockBooksRepository.Object);
        }

        [Theory, AutoData]
        public void WhenCallingGetAllReturnsBooksFromRepository(List<Book> books)
        {
            // Arrange
            _mockBooksRepository.Setup(x => x.GetAll()).Returns(books);

            // Act
            var result = _sut.GetAllBooksFromApi();

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

            _mockBooksRepository.Setup(x => x.GetAll()).Returns(books);

            // Act
            var response = _sut.GetSingleBook(2);

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

            _mockBooksRepository.Setup(x => x.GetAll()).Returns(books);

            // Act
            var response = _sut.GetSingleBook(5);

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public void WhenCallingGetPagedListReturnsPagedListFromRepository()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1" }, //0
                new Book { Id = 2, Title = "Book 2", Author = "Author 2" }, //1
                new Book { Id = 3, Title = "Book 3", Author = "Author 3" }, //2
            };

            _mockBooksRepository.Setup(x => x.GetBooksPage(It.IsAny<int>(), It.IsAny<int>())).Returns(books);

            // Act
            var response = _sut.GetPagedList(56, -20);

            // Assert
            Assert.Equal(books, response);
        }
    }
}