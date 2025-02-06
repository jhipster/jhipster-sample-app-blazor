using System.Text.Json.Serialization;

namespace JhipsterSampleApplication.Client.Models;

public class JwtToken
{
    [JsonPropertyName("id_token")]
    public string IdToken { get; set; }
}
