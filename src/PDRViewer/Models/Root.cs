using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PDRViewer.Models
{
    public class Root
    {
        [JsonProperty(PropertyName = "expand")]
        public string Expand { get; set; }

        [JsonProperty(PropertyName = "startAt")]
        public int StartAt { get; set; }

        [JsonProperty(PropertyName = "maxResults")]
        public int MaxResults { get; set; }

        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "issues")]
        public List<Issue> Issues { get; set; }

    }
}
