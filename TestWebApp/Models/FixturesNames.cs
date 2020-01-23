using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp.Models
{
    public class FixturesNames
    {
        [JsonProperty("results")]
        public int Result { get; set; }

        [JsonProperty("fixtures")]
        public List<string> Fixtres { get; set; }
    }
}
