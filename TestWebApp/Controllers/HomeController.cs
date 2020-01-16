using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<MainGridModel> footballMatches;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            GenerateDataSource();
        }

        private void GenerateDataSource()
        {
            footballMatches = new List<MainGridModel>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("api-football-v1.p.rapidapi.com", "9f7070847amsh17101106c788debp1cb536jsnd43cf341d933");
                var res = client.GetAsync("https://api-football-v1.p.rapidapi.com/v2/predictions/157462").Result;
            }
        }

        public IActionResult Index()
        {
            return View(footballMatches);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
