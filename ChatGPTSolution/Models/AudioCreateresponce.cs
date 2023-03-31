using System.Text.Json.Serialization;

namespace ChatGPTSolution.Models
{
    public class AudioCreateresponce
    {
        [JsonPropertyName("text")] public string Text { get; set; }
    }
}
