using APIFramework;
using APIFramework.Utility;
using APITests.Test_Data;
using AutoFixture;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;

namespace APITests
{
    [TestFixture]
    public class APITesting
    {
        [Test, Order(1)]
        public void ReturnAllBooks()
        {
            Framework<Book> framework = new Framework<Book>();
            List<Book> book = framework.GetBooks("books");

            book.ForEach((item) => {
                Assert.That(item.BookId, Is.Not.Null);
            });

            Assert.That(book.Count, Is.EqualTo(3)); 
        }

        [Test, Order(2)]
        public void CreateNewBook()
        {
            Fixture fixture = new Fixture();
            Book newBook = fixture.Create<Book>();

            Framework<Book> framework = new Framework<Book>();

            framework.CreateBook(newBook);

            Book book = framework.GetBookById(newBook.BookId);

            Assert.That(book.BookData.Title, Is.EqualTo(newBook.BookData.Title));
            Assert.That(book.Features[0].Name, Is.EqualTo(newBook.Features[0].Name)); 
        }

        [Test, Order(3)]
        public void UpdateBook()
        {
            Fixture fixture = new Fixture();
            Book newBook = fixture.Create<Book>();

            Framework<Book> framework = new Framework<Book>();
            framework.CreateBook(newBook);

            newBook.Features.Add(fixture.Create<Features>());
            framework.UpdateBook(newBook);

            Book book = framework.GetBookById(newBook.BookId);

            Assert.That(book.Features[2].Name, Is.EqualTo(newBook.Features[2].Name));
        }

        [Test, Order(4)]
        public void Deletebook()
        {
            Fixture fixture = new Fixture();
            Book newBook = fixture.Create<Book>();

            Framework<Book> framework = new Framework<Book>();
            framework.CreateBook(newBook);

            int initialbookCount = framework.GetBooks("books").Count;

            framework.DeleteBook(newBook.BookId);

            HttpStatusCode deletedbookResponseCode = framework.GetBookResponseCodeById(newBook.BookId);

            int updatedbookCount = framework.GetBooks("books").Count;

            Assert.That(deletedbookResponseCode, Is.EqualTo(HttpStatusCode.NotFound), $"This book with id {newBook.BookId} has a status code of {deletedbookResponseCode}");
            Assert.That(updatedbookCount, Is.EqualTo(initialbookCount), $"The initial book count was {initialbookCount} and the updated book count is {updatedbookCount}");
        }

        [Test, Category("Expected to fail"), Order(5)]
        public void FailedDeleteBook()
        {
            Fixture fixture = new Fixture();
            Book newBook = fixture.Create<Book>();

            Framework<Book> framework = new Framework<Book>();
            framework.CreateBook(newBook);

            framework.DeleteBook(newBook.BookId);

            HttpStatusCode deletedbookResponseCode = framework.GetBookResponseCodeById(newBook.BookId);

            Assert.That(deletedbookResponseCode, Is.EqualTo(HttpStatusCode.OK), $"This book with id {newBook.BookId} has a status code of {deletedbookResponseCode}");
        }
    }
}