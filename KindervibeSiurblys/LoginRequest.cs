using Newtonsoft.Json;

public class LoginRequest
{
    [JsonProperty("app_type")]
    public int AppType { get; set; }

    [JsonProperty("pntnew")]
    public string Pntnew { get; set; }

    [JsonProperty("pntold")]
    public string Pntold { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("platform")]
    public int Platform { get; set; }

    [JsonProperty("version")]
    public string Version { get; set; }
}
