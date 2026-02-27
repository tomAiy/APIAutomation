using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace APIFramework
{
    public class Framework<T> : APIBase<T>
    {
        public void Sleep(int seconds) => Thread.Sleep(TimeSpan.FromSeconds(seconds));

        public RestResponse GetBookResponseById(string bookId)
        {
            APIBase<Book> book = new APIBase<Book>();
            RestClient url = book.ChangeUrl($"book/{bookId}");
            RestRequest request = book.GetRequest();
            RestResponse response = book.ExecuteResponse(url, request);
            return response;
        }

        public HttpStatusCode GetBookResponseCodeById(string bookId)
        {
            APIBase<Book> book = new APIBase<Book>();
            RestClient url = book.ChangeUrl($"book/{bookId}");
            RestRequest request = book.GetRequest();
            RestResponse response = book.ExecuteResponse(url, request);
            HttpStatusCode statuscode = response.StatusCode;
            return statuscode;
        }

        public List<Book> GetBooks(string endpoint)
        {
            APIBase<Book> book = new APIBase<Book>();
            RestClient url = book.ChangeUrl(endpoint);
            RestRequest request = book.GetRequest();
            RestResponse response = book.ExecuteResponse(url, request);
            List<Book> content = book.GetAllContent<Book>(response);
            return content;
        }

        public Book GetBookById(string bookId)
        {     
            int retries = 0;
            int maxRetries = 3;
            RestResponse response = null;

            while (retries < maxRetries)
            {
                retries++;

                APIBase<Book> book = new APIBase<Book>();
                RestClient url = book.ChangeUrl($"book/{bookId}");
                RestRequest request = book.GetRequest();
                response = book.ExecuteResponse(url, request);
                Book content = book.GetContent<Book>(response);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return content;
                }

                Sleep(5); // Wait for 5 seconds before retrying
            }

            throw new Exception($"API request failed after {maxRetries} attempts with status code: {response.StatusCode}\n {response.Content}");
        }

        public string CreateBook(Book newBook)
        {
            int retries = 0;
            int maxRetries = 3;
            RestResponse response = null;

            while (retries < maxRetries)
            {
                retries++;

                APIBase<Book> book = new APIBase<Book>();
                RestClient url = book.ChangeUrl("book");
                //string jsonString = book.Serialize(newBook);
                RestRequest request = book.PostRequest(newBook);
                response = book.ExecuteResponse(url, request);

                if (response.StatusCode == HttpStatusCode.Accepted)
                {
                    return response.Content;
                }

                Sleep(5); // Wait for 5 seconds before retrying
            }

            throw new Exception($"API request failed after {maxRetries} attempts with status code: {response.StatusCode}\n {response.Content}");

        }

        public string UpdateBook(Book updateBook)
        {
            int retries = 0;
            int maxRetries = 3;
            RestResponse response = null;

            while (retries < maxRetries)
            {
                retries++;
                APIBase<Book> book = new APIBase<Book>();
                RestClient url = book.ChangeUrl("book");
                RestRequest request = book.UpdateRequest(updateBook);
                response = book.ExecuteResponse(url, request);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return response.Content;
                }

                Sleep(5); // Wait for 5 seconds before retrying
            }

            throw new Exception($"API request failed after {maxRetries} attempts with status code: {response.StatusCode}\n {response.Content}");
        }

        public string DeleteBook(string bookId)
        {
            int retries = 0;
            int maxRetries = 3;
            RestResponse response = null;

            while (retries < maxRetries)
            {
                retries++;
                APIBase<Book> book = new APIBase<Book>();
                RestClient url = book.ChangeUrl($"book/{bookId}");
                RestRequest request = book.DeleteRequest();
                response = book.ExecuteResponse(url, request);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return response.Content;
                }

                Sleep(5); // Wait for 5 seconds before retrying
            }

            throw new Exception($"API request failed after {maxRetries} attempts with status code: {response.StatusCode}\n {response.Content}");
        }
    }
}