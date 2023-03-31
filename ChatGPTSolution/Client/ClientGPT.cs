using ChatGPTSolution.Models;
using OpenAI_API.Completions;
using OpenAI_API.Models;
using OpenAI_API;

namespace ChatGPTSolution.Client
{
    public class ClientGPT: IGptClient
    {
        public IConfiguration Configuration { get; }
        public string apiKey
        {
            get
            {
                return Configuration["secretKey"];
            }
        }
        public string endPoint
        {
            get
            {
                return Configuration["ImageApiEndpoint"];
            }
        }

        public string chatEndpoint
        {
            get {
                return Configuration["ChatApiEndpoint"];
            }

        }
        public string audioEndpoint
        {
            get
            {
                return Configuration["AudiotranscriptionEndPoint"];
            }

        }
        
        public ClientGPT(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<List<string>> resultSet(string input)
        {

            try
            {
                List<string> value = new List<string>();
                string result = "";
                var output = await clientReqAsync(input);

                foreach (var item in output.Completions)
                {
                    result = item.Text;
                    value.Add(item.Text);
                }
                return value;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public async Task<AudioCreateresponce> GenerateAudioText(AudioCreaterequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                //httpClient.BaseAddress = new Uri(audioEndpoint);
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                const string fileName = "voice.mp3";
                var sampleFile = await File.ReadAllBytesAsync($"Client/{fileName}");

                var multipartContent = new MultipartFormDataContent
                {
                    {new ByteArrayContent(sampleFile), "file", fileName},
                    {new StringContent("whisper-1"), "model"},
                    {new StringContent("verbose_json"), "response_format"  }
                };
                var res = await httpClient.PostAsync(audioEndpoint, multipartContent, cancellationToken);
                var result = await res.Content.ReadFromJsonAsync<AudioCreateresponce>(cancellationToken: cancellationToken);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ChatCompletionResponce> GenerateCompletion(ChatCompletionRequestcs request, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var responce = await httpClient.PostAsJsonAsync(chatEndpoint, request, cancellationToken);
                var result = await responce.Content.ReadFromJsonAsync<ChatCompletionResponce>(cancellationToken: cancellationToken);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ImageCreateResponse> CreateImage(ImageCreateRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var responce = await httpClient.PostAsJsonAsync(endPoint, request, cancellationToken);
                var result = await responce.Content.ReadFromJsonAsync<ImageCreateResponse>(cancellationToken: cancellationToken);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<CompletionResult> clientReqAsync(string input)
        {
            CompletionResult completionResult = null;
            if (!String.IsNullOrEmpty(apiKey))
            {
                var gpt = new OpenAIAPI(apiKey);
                CompletionRequest completionRequest = new CompletionRequest()
                {
                    Prompt = input,
                    Model = Model.DavinciText,
                    Temperature = 0.5f,
                    MaxTokens = 4000,
                    TopP = 1.0,
                    FrequencyPenalty = 0.0,
                    PresencePenalty = 0.0,
                };
                completionResult = await gpt.Completions.CreateCompletionAsync(completionRequest);
            }
            return completionResult;
        }
    }

    public interface IGptClient
    {
        Task<ImageCreateResponse> CreateImage(ImageCreateRequest imageCreateModel, CancellationToken cancellationToken = default);
        Task<ChatCompletionResponce> GenerateCompletion(ChatCompletionRequestcs request, CancellationToken cancellationToken = default);
        Task<List<string>> resultSet(string input);
        Task<AudioCreateresponce> GenerateAudioText(AudioCreaterequest request, CancellationToken cancellationToken = default);
    }
}
