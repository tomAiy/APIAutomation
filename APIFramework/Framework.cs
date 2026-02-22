using System;
using System.Collections.Generic;
using System.Threading;

namespace APIFramework
{
    public class Framework<T> : APIBase<T>
    {
        public void Sleep(int seconds) => Thread.Sleep(TimeSpan.FromSeconds(seconds));

        public List<ListBooks> GetBooks(string endpoint)
        {
            APIBase<ListBooks> book = new APIBase<ListBooks>();
            RestSharp.RestClient url = book.ChangeUrl(endpoint);
            RestSharp.RestRequest request = book.GetRequest();
            RestSharp.RestResponse response = book.ExecuteResponse(url, request);
            List<ListBooks> content = book.GetAllContent<ListBooks>(response);
            return content;
        }

        public ListBooks GetBook(string endpoint)
        {
            APIBase<ListBooks> book = new APIBase<ListBooks>();
            RestSharp.RestClient url = book.ChangeUrl(endpoint);
            RestSharp.RestRequest request = book.GetRequest();
            RestSharp.RestResponse response = book.ExecuteResponse(url, request);
            ListBooks content = book.GetContent<ListBooks>(response);
            return content;
        }

        public int GetBookResponseCode(string endpoint)
        {
            APIBase<ListBooks> book = new APIBase<ListBooks>();
            RestSharp.RestClient url = book.ChangeUrl(endpoint);
            RestSharp.RestRequest request = book.GetRequest();
            RestSharp.RestResponse response = book.ExecuteResponse(url, request);
            int statuscode = book.GetResponseCode(response);
            return statuscode;
        }

        public CreateBooks CreateBooks(dynamic body)
        {
            APIBase<CreateBooks> book = new APIBase<CreateBooks>();
            RestSharp.RestClient url = book.ChangeUrl("book");
            dynamic jsonString = book.Serialize(body);
            dynamic request = book.PostRequest(jsonString);
            dynamic response = book.ExecuteResponse(url, request);
            dynamic statuscode = book.GetResponseCode(response);

            if (statuscode == 202)
            {
                Sleep(5);
                RestSharp.RestClient newurl = book.ChangeUrl($"book/{body.BookId}");
                RestSharp.RestRequest newRequest = book.GetRequest();
                RestSharp.RestResponse newResponse = book.ExecuteResponse(newurl, newRequest);

                CreateBooks content = book.GetContent<CreateBooks>(newResponse);
                return content;
            }
            else
            {
                throw new Exception($"API request failed with status code: {statuscode}\n {response.Content}"); 
            }
        }
        public UpdateBooks UpdateBooks(dynamic body)
        {
            APIBase<UpdateBooks> book = new APIBase<UpdateBooks>();
            RestSharp.RestClient url = book.ChangeUrl("book");
            dynamic jsonString = book.Serialize(body);
            dynamic request = book.UpdateRequest(jsonString);
            dynamic response = book.ExecuteResponse(url, request);
            dynamic statuscode = book.GetResponseCode(response);

            if (statuscode == 204)
            {
                Sleep(5);
                RestSharp.RestClient newurl = book.ChangeUrl($"book/{body.BookId}");
                RestSharp.RestRequest newRequest = book.GetRequest();
                RestSharp.RestResponse newResponse = book.ExecuteResponse(newurl, newRequest);

                UpdateBooks content = book.GetContent<UpdateBooks>(newResponse);
                return content;
            }
            else
            {
                throw new Exception($"API request failed with status code: {statuscode}");
            }
        }

        public List<DeleteBooks> DeleteBooks(int bookId)
        {
            APIBase<DeleteBooks> book = new APIBase<DeleteBooks>();
            RestSharp.RestClient url = book.ChangeUrl($"book/{bookId}");
            RestSharp.RestRequest request = book.DeleteRequest();
            RestSharp.RestResponse response = book.ExecuteResponse(url, request);
            int statuscode = book.GetResponseCode(response);

            if (statuscode == 204)
            {
                Sleep(5);
                RestSharp.RestClient newurl = book.ChangeUrl("books");
                RestSharp.RestRequest newRequest = book.GetRequest();
                RestSharp.RestResponse newResponse = book.ExecuteResponse(newurl, newRequest);

                List<DeleteBooks> content = book.GetAllContent<DeleteBooks>(newResponse);
                return content;
            }
            else
            {
                throw new Exception($"API request failed with status code: {statuscode}");
            }
        }
    }
}