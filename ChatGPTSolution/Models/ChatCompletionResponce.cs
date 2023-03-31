using System.Text.Json.Serialization;

namespace ChatGPTSolution.Models
{
    public class ChatCompletionResponce
    {
        [JsonPropertyName("model")] 
        public string Model { get; set; }

        [JsonPropertyName("choices")] 
        public List<ChoiceResponse> Choices { get; set; }

    }
    public record ChoiceResponse
    {
        [JsonPropertyName("text")] public string Text { get; set; }        
    }
}
