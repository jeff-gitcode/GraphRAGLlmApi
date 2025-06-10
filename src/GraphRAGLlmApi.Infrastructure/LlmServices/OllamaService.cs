using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GraphRAGLlmApi.Domain.Entities;
using GraphRAGLlmApi.Domain.Interfaces;
using GraphRAGLlmApi.Domain.ValueObjects;

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

        public Task<EmbeddingVector> GenerateEmbeddingAsync(string content, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public async Task<string> GenerateResponseAsync(string prompt)
        {
            var requestBody = new
            {
                prompt = prompt
            };

            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_ollamaApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = System.Text.Json.JsonDocument.Parse(jsonResponse);
                return result.RootElement.GetProperty("response").GetString();
            }

            throw new HttpRequestException($"Error generating response: {response.ReasonPhrase}");
        }

        public Task<string> GenerateResponseAsync(string query, Guid similarDocuments, IEnumerable<GraphNode> graphConnections, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmbeddingAsync(string text)
        {
            throw new NotImplementedException();
        }
    }
}