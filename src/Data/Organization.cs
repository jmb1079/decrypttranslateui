using System.Text.Json.Serialization;

namespace DecryptTranslateUi.Data;

public class Organization
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public String Name { get; set; }

    [JsonPropertyName("shortName")]
    public String ShortName { get; set; }

    [JsonPropertyName("website")]
    public String Website { get; set; }
}