using System.Text.Json.Serialization;

namespace DecryptTranslateUi.Data;

public class Image
{
    [JsonPropertyName("id")]
    public String Id { get; set; }

    [JsonPropertyName("name")]
    public String Name { get; set; }
}