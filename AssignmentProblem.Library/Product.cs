using Newtonsoft.Json;

namespace AssignmentProblem.Library
{
    class Product
    {
        [JsonProperty("id")]
        public int ID { get; set; }
    }
}
