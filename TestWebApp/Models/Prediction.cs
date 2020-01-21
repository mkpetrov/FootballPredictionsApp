using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp.Models
{
    public class Prediction
    {
        [JsonProperty("match_winner")]
        public string MatchWinner { get; set; }

        [JsonProperty("advice")]
        public string Advice { get; set; }

        [JsonProperty("winning_percent")]
        public WinningPercent WinningPercent { get; set; }
    }
}