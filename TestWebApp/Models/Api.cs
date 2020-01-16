using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp.Models
{
    public class Api
    {
        [JsonProperty("result")]
        public int Result { get; set; }

        public List<Prediction> Predictions { get; set; }
    }
}
