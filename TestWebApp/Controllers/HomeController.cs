using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private string _header = "X-RapidAPI-Key";
        private string _testKey = "9f7070847amsh17101106c788debp1cb536jsnd43cf341d933";
        private string _mainKey = "0121d683b143feca8b0ce39c22884440";
        private static int leagueId = 524;
        private string _wrongTranslateDraw = "равенства";
        private string _correctTranslateDraw = "равенство";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        private void GenerateDataSource()
        {
            footballMatches = new List<MainGridModel>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(_header, _mainKey);

                FixturesNames fixturesNames = GetFixturesNames(client);

                if (fixturesNames == null || !fixturesNames.Fixtres.Any())
                    return;

                List<Fixture> fixtures = GetFixtures(client, fixturesNames);

                if (fixtures == null || !fixtures.Any())
                    return;

                foreach (var fixture in fixtures)
                {
                    Prediction predictionResult = GetPredictions(client, fixture);

                    var adviceParts = predictionResult.Advice.Split(':');

                    if (predictionResult.MatchWinner.Contains('N'))
                        predictionResult.MatchWinner = predictionResult.MatchWinner.Replace('N', 'X');

                    var translate = client
                        .GetAsync(new Uri($"https://translate.yandex.net/api/v1.5/tr.json/translate?key=trnsl.1.1.20200123T082649Z.bc3d13ca3ec650a0.be300a3e44159909b89563a85275b8c16b9006cf&text={predictionResult.Advice}&lang=bg"))
                        .Result;
                    var content = translate.Content.ReadAsStringAsync();
                    var parsedTranslate = JObject.Parse(content.Result);
                    var translateModel = JsonConvert.DeserializeObject<TranslateModel>(parsedTranslate.ToString());
                    if(translateModel != null && translateModel.Text.Contains(_wrongTranslateDraw))
                    {
                        var index = translateModel.Text.IndexOf(_wrongTranslateDraw);
                        translateModel.Text[index] = _correctTranslateDraw;
                    }

                    var model = new MainGridModel
                    {
                        Advice = translateModel?.Text.FirstOrDefault(), /*adviceParts.Length > 1 ? adviceParts[1] : predictionResult.Advice,*/
                        AwayWinningPercent = predictionResult.WinningPercent?.Away,
                        DrawWinningPercent = predictionResult.WinningPercent?.Draws,
                        HomeWinningPercent = predictionResult.WinningPercent?.Home,
                        MatchWinner = predictionResult.MatchWinner,
                        FootballMatch = $"{fixture.HomeTeam.TeamName} - {fixture.AwayTeam.TeamName}",
                        FixtureDate = fixture.EventDate
                    };

                    footballMatches.Add(model);
                }
            }
        }

        private static Prediction GetPredictions(HttpClient client, Fixture fixture)
        {
            var result = client.GetAsync(new Uri($"http://v2.api-football.com/predictions/{fixture.FixtureId}")).Result;
            var content = result.Content.ReadAsStringAsync();
            var parsedObject = JObject.Parse(content.Result);
            var parsedObjectTostring = parsedObject["api"]["predictions"].ToString();
            var predictions = JsonConvert.DeserializeObject<List<Prediction>>(parsedObjectTostring);
            var predictionResult = predictions.FirstOrDefault();
            return predictionResult;
        }

        private static List<Fixture> GetFixtures(HttpClient client, FixturesNames fixturesNames)
        {
            var currentFixtureName = fixturesNames.Fixtres.FirstOrDefault();
            var fixturesResult = client.GetAsync(new Uri($"http://v2.api-football.com/fixtures/league/{leagueId}/{currentFixtureName}")).Result;
            var fixturesContent = fixturesResult.Content.ReadAsStringAsync();
            var parsedFixturesObject = JObject.Parse(fixturesContent.Result);
            var parsedFituresObjectToString = parsedFixturesObject["api"]["fixtures"].ToString();
            var fixtures = JsonConvert.DeserializeObject<List<Fixture>>(parsedFituresObjectToString);
            return fixtures;
        }

        private static FixturesNames GetFixturesNames(HttpClient client)
        {
            var result = client.GetAsync(new Uri("http://v2.api-football.com/fixtures/rounds/524/current")).Result;
            var content = result.Content.ReadAsStringAsync();
            var parsedObject = JObject.Parse(content.Result);
            var parsedObjectTostring = parsedObject["api"].ToString();
            var fixturesNames = JsonConvert.DeserializeObject<FixturesNames>(parsedObjectTostring);
            return fixturesNames;
        }

        [HttpGet]
        public IActionResult GetEnglishMatches()
        {
            GenerateDataSource();
            return View("Index", footballMatches);
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