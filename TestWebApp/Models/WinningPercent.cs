using Newtonsoft.Json;

namespace TestWebApp.Models
{
    public class WinningPercent
    {
        [JsonProperty("home")]
        public string Home { get; set; }

        [JsonProperty("draws")]
        public string Draws { get; set; }

        [JsonProperty("away")]
        public string Away { get; set; }
    }
}
