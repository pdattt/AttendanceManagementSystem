using CardManagement.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CardManagement.MVVM.ViewModel.MainView
{
    public class CardManagementViewModel
    {
        private readonly string apiUrl = "https://localhost:7287/";

        public CardManagementViewModel()
        {
            ApiHelper.InitializeClient();
        }

        public async Task<List<Attendee>> GetAllAttendee()
        {
            string url = apiUrl + "api/attendee/get-all-attendees";
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ApiHelper.token);

                    try
                    {
                        using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(requestMessage))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                List<Attendee> attendees = await response.Content.ReadAsAsync<List<Attendee>>();

                                return attendees;
                            }

                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                };
            }
            catch
            {
                return null;
            }

            //try
            //{
            //    using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            //    {
            //        if (response.IsSuccessStatusCode)
            //        {
            //            List<Attendee> attendees = await response.Content.ReadAsAsync<List<Attendee>>();

            //            return attendees;
            //        }

            //        return null;
            //    }
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}
        }

        public string UpdateAttendeeCardId(int attendeeId, string cardId)
        {
            string url = apiUrl + $"api/attendee/update-attendee-card-id?attendeeId={attendeeId}&cardId={cardId}";
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, url))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ApiHelper.token);

                    try
                    {
                        using (HttpResponseMessage response = ApiHelper.ApiClient.SendAsync(requestMessage).Result)
                        {
                            return response.Content.ReadAsAsync<string>().Result;
                        }
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                };
            }
            catch
            {
                return null;
            }
        }
    }
}