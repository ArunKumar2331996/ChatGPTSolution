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
        public async Task<ImageCreateResponse> CreateImage(ImageCreateRequest imageCreateModel, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(endPoint);
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await _httpClient.PostAsJsonAsync(endPoint, imageCreateModel, cancellationToken);
                var responce = await response.Content.ReadFromJsonAsync<ImageCreateResponse>(cancellationToken: cancellationToken) ?? throw new InvalidOperationException(); ;
                return responce;
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
        Task<List<string>> resultSet(string input);
        Task<ImageCreateResponse> CreateImage(ImageCreateRequest imageCreateModel, CancellationToken cancellationToken = default);
    }
}
