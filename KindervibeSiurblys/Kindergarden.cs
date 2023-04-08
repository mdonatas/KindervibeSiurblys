using Newtonsoft.Json;

public class Kindergarden
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}
