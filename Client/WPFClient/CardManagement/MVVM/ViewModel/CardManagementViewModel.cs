using CardManagement.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.MVVM.ViewModel
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

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<Attendee> attendees = await response.Content.ReadAsAsync<List<Attendee>>();

                    return attendees;
                }

                return null;
            }
        }
    }
}