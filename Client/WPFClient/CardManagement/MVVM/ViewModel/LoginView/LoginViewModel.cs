using CardManagement.Core;
using CardManagement.DTOs;
using CardManagement.MVVM.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.MVVM.ViewModel.LoginView
{
    public class LoginViewModel : ObservableObject
    {
        private readonly string apiUrl = "https://localhost:7287/";

        public LoginViewModel()
        {
            ApiHelper.InitializeClient();
        }

        public void ResetToken()
        {
            ApiHelper.token = "";
        }

        public async Task<string> LogIn(string username, string password)
        {
            string url = apiUrl + "api/user/login";

            if (username == null || password == null)
                return null;

            var userLogin = new Dictionary<string, string>
            {
                {"username", username},
                {"password", password}
            };

            var content = JsonConvert.SerializeObject(userLogin);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, byteContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    string token = await response.Content.ReadAsAsync<string>();

                    if (token != null)
                        ApiHelper.token = token;

                    return token;
                }
            }

            return null;
        }
    }
}