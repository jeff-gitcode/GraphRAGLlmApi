using GraphRAGLlmApi.Domain.Entities;
using GraphRAGLlmApi.Domain.ValueObjects;

namespace GraphRAGLlmApi.Domain.Interfaces
{
  public interface ILlmService
  {
        Task<EmbeddingVector> GenerateEmbeddingAsync(string content, CancellationToken cancellationToken);
        Task<string> GenerateResponseAsync(
      string query,
      Guid similarDocuments,
      IEnumerable<GraphNode> graphConnections,
      CancellationToken cancellationToken);
    Task<string> GetEmbeddingAsync(string text);
  }
}