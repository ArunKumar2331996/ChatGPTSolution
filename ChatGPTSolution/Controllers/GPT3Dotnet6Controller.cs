using ChatGPTSolution.Client;
using ChatGPTSolution.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatGPTSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GPT3Dotnet6Controller : ControllerBase
    {
        private readonly IGptClient _gptClient;
        public GPT3Dotnet6Controller(IGptClient gptClient)
        {
            _gptClient = gptClient;
        }

        [HttpPost("image")]
        public async Task<ImageCreateResponse> CreateImage(ImageCreateRequest imageCreateModel, CancellationToken cancellationToken = default)
        {
            return await _gptClient.CreateImage(imageCreateModel, cancellationToken);
        }

        [HttpPost("chat")]
        public async Task<List<string>> GetResult([FromBody] string input)
        {
            return await _gptClient.resultSet(input);
        }

        [HttpPost("chatHttpClient")]
        public async Task<ChatCompletionResponce> GenerateChat(ChatCompletionRequestcs request, CancellationToken token = default)
        {
            return await _gptClient.GenerateCompletion(request, token);
        }

        [HttpPost("AudioHttpClient")]
        public async Task<AudioCreateresponce> GenerateAudioText(AudioCreaterequest request, CancellationToken token = default)
        {
            return await _gptClient.GenerateAudioText(request, token);
        }
    }
}
