using System.Text.Json.Serialization;

namespace DecryptTranslateUi.Data
{
    public class Case
    {
        [JsonPropertyName("number")]
        public int CaseNumber { get; set; }

        [JsonPropertyName("primaryInvestigator")]
        public string PrimaryInvestigator { get; set; } = string.Empty;

        [JsonPropertyName("organization")]
        public int Organization { get; set; }
    }
}