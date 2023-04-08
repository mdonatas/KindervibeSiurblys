using Newtonsoft.Json;

public class Staff
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("photo")]
    public string Photo { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }
}
