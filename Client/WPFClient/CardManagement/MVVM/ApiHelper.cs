using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CardManagement.MVVM
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }
        public static string token { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("https://localhost:7287/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}