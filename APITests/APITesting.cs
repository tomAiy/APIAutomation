using APIFramework;
using APIFramework.Utility;
using APITests.Test_Data;
using NUnit.Framework;
using System.Collections.Generic;

namespace APITests
{
    [TestFixture]
    public class APITesting
    {
        [Test, Order(1)]
        public void ReturnAllBooks()
        {
            Framework<ListBooks> framework = new Framework<ListBooks>();
            List<ListBooks> book = framework.GetBooks("books");

            book.ForEach((item) => {
                Assert.That(item.BookId, Is.Not.Null);
            });

            Assert.That(book.Count, Is.EqualTo(3)); 
        }

        [Test, Order(2)]
        public void CreateNewBook()
        {
            Framework<CreateBooks> framework = new Framework<CreateBooks>();
            string file = HandleContent.GetFilePath(CommonData.createBookFile);
            CreateBooks body = HandleContent.ParseJson<CreateBooks>(file);
            body.BookId = "23";

            CreateBooks book = framework.CreateBooks(body);

            Assert.That(book.BookData.Title, Is.EqualTo(body.BookData.Title));
            Assert.That(book.Features[0].Name, Is.EqualTo(body.Features[0].Name)); 
        }

        [Test, Order(3)]
        public void UpdateBook()
        {
            Framework<UpdateBooks> framework = new Framework<UpdateBooks>();
            string file = HandleContent.GetFilePath(CommonData.updateBookFile);
            UpdateBooks body = HandleContent.ParseJson<UpdateBooks>(file);

            UpdateBooks book = framework.UpdateBooks(body);

            Assert.That(book.Features[2].Name, Is.EqualTo(body.Features[2].Name));
        }

        [Test, Order(4)]
        public void Deletebook()
        {
            Framework<DeleteBooks> framework = new Framework<DeleteBooks>();
            int initialbookCount = framework.GetBooks("books").Count;
            int deletedbookId = 2;

            framework.DeleteBooks(deletedbookId);   
            
            int updatedbookCount = framework.GetBooks("books").Count;

            Assert.That(updatedbookCount, Is.EqualTo(initialbookCount - 1), "book count did not decrease after deletion");
        }

        [Test, Description("Expected to fail"), Order(5)]
        public void FailedDeleteBook()
        {
            Framework<DeleteBooks> framework = new Framework<DeleteBooks>();
            int deletedbookId = 3;

            framework.DeleteBooks(deletedbookId);

            int deletedbookStatusCode = framework.GetBookResponseCode($"book/{deletedbookId}");

            Assert.That(deletedbookStatusCode, Is.EqualTo(404), $"This book with id {deletedbookId} has a status code of {deletedbookStatusCode}"); 
        }
    }
}