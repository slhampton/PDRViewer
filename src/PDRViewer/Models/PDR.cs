using System;
using System.Collections;
using Newtonsoft.Json;

namespace PDRViewer.Models
{
    // ReSharper disable once InconsistentNaming
    public class PDR
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Votes { get; set; }
    }
}