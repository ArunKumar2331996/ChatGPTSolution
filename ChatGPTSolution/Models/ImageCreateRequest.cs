using System.Text.Json.Serialization;

namespace ChatGPTSolution.Models
{
    public class ImageCreateRequest
    {
        public ImageCreateRequest()
        {
        }

        public ImageCreateRequest(string prompt)
        {
            Prompt = prompt;
        }

        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }
    }
}
