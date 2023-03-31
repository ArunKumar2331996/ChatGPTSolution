using System.Text.Json.Serialization;

namespace ChatGPTSolution.Models
{
    public class ImageCreateResponse: BaseResponse
    {
        [JsonPropertyName("data")] public List<ImageDataResult> Results { get; set; }

        public class ImageDataResult
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }
            [JsonPropertyName("b64_json")] public string B64 { get; set; }
        }
    }
}
