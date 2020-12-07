using GestionFormation.DAO;
using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GestionFormation.Controllers
{
    public class ApprenantController : Controller
    {
        // GET: Apprenant
        [LoginRequiredFilter]
        public async Task<ActionResult> Index()
        {
            // je dois afficher la page web get byID de web service
            UserDTO2 app = new UserDTO2();
            await GetApprenant(app);
            return View();
        }

        private static async Task GetApprenant(UserDTO2 apprenant)
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44326/api")
            };

            HttpResponseMessage response = await httpClient.GetAsync("ApprenantAPI");

            if (response.IsSuccessStatusCode)
            {
                // installer si nécessaire le package nuget Microsoft.AspNet.WebApi.Client
                UserDTO2 user = await response.Content.ReadAsAsync<UserDTO2>();


            }
        }
        public async Task<ActionResult> Create()
        {
            UserDTO2 app = null;

            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44326/api/")
            };

            HttpResponseMessage response = await httpClient.GetAsync("ApprenantAPI");

            if (response.IsSuccessStatusCode)
            {
              UserDTO2  user = await response.Content.ReadAsAsync<UserDTO2>();
                string apiResponse = await response.Content.ReadAsStringAsync();
                //UserDTO2 user = JsonConvert.DeserializeObject<UserDTO2>(apiResponse);

            }

            return View(app);
        }


    }
}
