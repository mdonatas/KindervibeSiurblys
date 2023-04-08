using Newtonsoft.Json;

public class ChildrenResponse
{
    public int Id { get; set; } = 1;
    
    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("next")]
    public object Next { get; set; }

    [JsonProperty("previous")]
    public object Previous { get; set; }

    [JsonProperty("results")]
    public List<Child> Results { get; set; }
}
