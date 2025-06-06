using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GraphRAGLlmApi.Domain.Interfaces;

namespace GraphRAGLlmApi.Infrastructure.LlmServices
{
    public class OllamaService : ILlmService
    {
        private readonly HttpClient _httpClient;
        private readonly string _ollamaApiUrl;

        public OllamaService(HttpClient httpClient, string ollamaApiUrl)
        {
            _httpClient = httpClient;
            _ollamaApiUrl = ollamaApiUrl;
        }

        public async Task<string> GenerateResponseAsync(string prompt)
        {
            var requestBody = new
            {
                prompt = prompt
            };

            var content = new StringContent(JsonConverter.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_ollamaApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(jsonResponse);
                return result?.response;
            }

            throw new HttpRequestException($"Error generating response: {response.ReasonPhrase}");
        }
    }
}