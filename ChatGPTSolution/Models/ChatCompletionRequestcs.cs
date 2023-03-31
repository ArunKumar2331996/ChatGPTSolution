using System.Text.Json.Serialization;

namespace ChatGPTSolution.Models
{
    public class ChatCompletionRequestcs
    {
        [JsonPropertyName("prompt")]
        public string? Prompt { get; set; }

        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

    }
}
