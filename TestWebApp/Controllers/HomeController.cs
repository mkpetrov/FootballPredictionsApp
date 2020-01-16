using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "9f7070847amsh17101106c788debp1cb536jsnd43cf341d933");
                var result = client.GetAsync(new Uri( "https://api-football-v1.p.rapidapi.com/v2/predictions/157462")).Result;
                var content = result.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(content.Result);
                var parsedObjectTostring = parsedObject["api"]["predictions"].ToString();
                var predictions = JsonConvert.DeserializeObject<List<Prediction>>(parsedObjectTostring);
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
