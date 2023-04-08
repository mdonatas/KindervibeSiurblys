using Newtonsoft.Json;

public class LoginResponse
{
    [JsonProperty("token")]
    public string Token { get; set; }
}
