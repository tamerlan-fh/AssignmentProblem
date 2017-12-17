using Newtonsoft.Json;

namespace AssignmentProblem.Library
{
    public class Product
    {
        [JsonProperty("id")]
        public int ID { get; set; }
    }
}
