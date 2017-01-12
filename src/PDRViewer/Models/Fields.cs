using Newtonsoft.Json;

namespace PDRViewer.Models
{
    public class Fields
    {
        [JsonProperty(PropertyName = "summary")]
        public string Title { get; set; }      

        [JsonProperty(PropertyName = "customfield_16000")]
        public float Votes { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}