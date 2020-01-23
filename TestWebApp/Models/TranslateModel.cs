using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp.Models
{
    public class TranslateModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("lang")]
        public string Lang { get; set; }
        [JsonProperty("text")]
        public List<string> Text { get; set; }
    }
}
