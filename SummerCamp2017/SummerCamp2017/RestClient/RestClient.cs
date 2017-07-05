using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Plugin.RestClient
{

        public class RestClient<T>
        {
            public string WebServiceUrl = "http://taskmodel.azurewebsites.net/api/TaskModels/";


        //public List<T> GetByAnnouncement(int id)
        //{
        //    var httpClient = new HttpClient();

        //    var json = httpClient.GetStringAsync(WebServiceUrl + id).Result;

        //    var obj = JsonConvert.DeserializeObject<List<T>>(json);

        //    return obj;
        //}
            public List<T> GetAsync()
            {
                var httpClient = new HttpClient();
                var json = httpClient.GetStringAsync(WebServiceUrl).Result;
                var taskModels = JsonConvert.DeserializeObject<List<T>>(json);

                return taskModels;
            }

            public T GetByIdAsync(int id)
            {
                var httpClient = new HttpClient();

                var json = httpClient.GetStringAsync(WebServiceUrl + id).Result;

                var taskModels = JsonConvert.DeserializeObject<T>(json);

                return taskModels;
            }

            public bool PostAsync(T t)
            {
                var httpClient = new HttpClient();

                var json = JsonConvert.SerializeObject(t);

                HttpContent httpContent = new StringContent(json);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = httpClient.PostAsync(WebServiceUrl, httpContent).Result;

                return result.IsSuccessStatusCode;
            }
            

            public bool Close(T t)
            {
                var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = httpClient.PostAsync(WebServiceUrl, httpContent).Result;

                return response.IsSuccessStatusCode;
            }

            public bool Extend(T t)
            {
                var httpClient = new HttpClient();
                var json = JsonConvert.SerializeObject(t);

                HttpContent httpContent = new StringContent(json);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = httpClient.PostAsync(WebServiceUrl, httpContent).Result;

                return response.IsSuccessStatusCode;
            }

            public bool PutAsync(int id, T t)
            {
                var httpClient = new HttpClient();

                var json = JsonConvert.SerializeObject(t);

                HttpContent httpContent = new StringContent(json);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = httpClient.PutAsync(WebServiceUrl + id, httpContent).Result;

                return result.IsSuccessStatusCode;
            }

        }
    
}