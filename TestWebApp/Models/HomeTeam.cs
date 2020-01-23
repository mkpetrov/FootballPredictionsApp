using Newtonsoft.Json;

namespace TestWebApp.Models
{
    public class HomeTeam
    {
        [JsonProperty("team_id")]
        public int TeamId { get; set; }
        [JsonProperty("team_name")]
        public string TeamName { get; set; }
        [JsonProperty("logo")]
        public string Logo { get; set; }
    }
}
