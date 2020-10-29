using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpecFlowProject_FakeWebServerTests.Models
{
    public class LocationData
    {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
    }
}
