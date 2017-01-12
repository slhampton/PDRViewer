using System.Collections.Generic;
using Newtonsoft.Json;

namespace PDRViewer.Models
{
    public class Issue
    {
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public Fields Fields { get; set; }
    }
}