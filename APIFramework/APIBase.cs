using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace APIFramework
{
    public class APIBase<T>
    {
        public RestClient restClient;
        public RestRequest restRequest;
        public string baseUrl = "http://localhost:8080/";

        public RestClient ChangeUrl(string endpoint)
        {
            string url = Path.Combine(baseUrl, endpoint);
            RestClient restClient = new RestClient(url);
            return restClient;
        }

        public RestRequest PostRequest(object requestbody) 
        {
            RestRequest restRequest = new RestRequest("", Method.Post);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddParameter("application/json", requestbody, ParameterType.RequestBody);
            restRequest.RequestFormat = DataFormat.Json;
            return restRequest;
        }

        public RestRequest UpdateRequest(object requestbody) 
        {
            RestRequest restRequest = new RestRequest("", Method.Put);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddParameter("application/json", requestbody, ParameterType.RequestBody);
            restRequest.RequestFormat = DataFormat.Json;
            return restRequest;
        }

        public RestRequest GetRequest()
        {
            RestRequest restRequest = new RestRequest("", Method.Get);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            return restRequest;
        }

        public RestRequest DeleteRequest()   
        {
            RestRequest restRequest = new RestRequest("", Method.Delete);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            return restRequest;
        }

        public RestResponse ExecuteResponse(RestClient client, RestRequest request) 
        {
            return client.Execute(request);
        }

        public int GetResponseCode(RestResponse response)
        {
            HttpStatusCode statusCode = response.StatusCode;
            return (int)statusCode;
        }

        public List<Request> GetAllContent<Request>(RestResponse response)
        {
            string content = response.Content;
            List<Request> requestObj = JsonConvert.DeserializeObject<List<Request>>(content);
            return requestObj;
        }

        public DTO GetContent<DTO>(RestResponse response)
        {
            string content = response.Content;
            DTO requestObj = JsonConvert.DeserializeObject<DTO>(content);
            return requestObj;
        }

        public string Serialize(dynamic content)
        {      
            string json = JsonConvert.SerializeObject(content, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });

            return json;
        }
    }
}