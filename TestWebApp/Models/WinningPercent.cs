using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
