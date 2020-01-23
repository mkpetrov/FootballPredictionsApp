using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp.Models
{
    public class AwayTeam
    {
        [JsonProperty("team_id")]
        public int TeamId { get; set; }
        [JsonProperty("team_name")]
        public string TeamName { get; set; }
        [JsonProperty("logo")]
        public string Logo { get; set; }
    }
}
