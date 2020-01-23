using System;

namespace TestWebApp.Models
{
    public class MainGridModel
    {
        public string FootballMatch { get; set; }
        public string HomeWinningPercent { get; set; }
        public string DrawWinningPercent { get; set; }
        public string AwayWinningPercent { get; set; }
        public string MatchWinner { get; set; }
        public string Advice { get; set; }
        public DateTime FixtureDate { get; set; }
    }
}
