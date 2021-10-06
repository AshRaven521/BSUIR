using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WEB_953506_KONDRAHOV.BLAZOR.Client.Models
{
    public class ListViewModel
    {
        [JsonPropertyName("engineId")]
        public int EngineId { get; set; }
        [JsonPropertyName("engineName")]
        public string EngineName { get; set; } 
    }

    public class DetailsViewModel
    {
        [JsonPropertyName("engineName")]
        public string EngineName { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("price")]
        public int Price { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}
