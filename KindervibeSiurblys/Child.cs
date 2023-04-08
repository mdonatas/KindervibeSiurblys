using Newtonsoft.Json;

public class Child
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string? LastName { get; set; }

    [JsonProperty("birth_day")]
    public string BirthDay { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }

    [JsonProperty("additional")]
    public string Additional { get; set; }

    [JsonProperty("photo")]
    public string Photo { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }

    [JsonProperty("garden_group")]
    public int GardenGroup { get; set; }

    [JsonProperty("garden_group_name")]
    public string GardenGroupName { get; set; }

    [JsonProperty("family")]
    public List<int> Family { get; set; }

    [JsonProperty("kindergarden")]
    public Kindergarden Kindergarden { get; set; }

    [JsonProperty("staff")]
    public List<Staff> Staff { get; set; }
}
