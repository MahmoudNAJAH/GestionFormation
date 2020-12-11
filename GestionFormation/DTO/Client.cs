using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GestionFormation.DTO
{
    public class Client
    {

        private static async Task GetApprenant(UserDTOAPI apprenant)
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44326/api")
            };

            HttpResponseMessage response = await httpClient.GetAsync("ApprenantAPI");

            if (response.IsSuccessStatusCode)
            {
                // installer si nécessaire le package nuget Microsoft.AspNet.WebApi.Client
                UserDTOAPI user = await response.Content.ReadAsAsync<UserDTOAPI>();


            }
        }
    }
}