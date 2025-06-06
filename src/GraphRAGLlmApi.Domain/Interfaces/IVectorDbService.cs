using GraphRAGLlmApi.Domain.Entities;
using GraphRAGLlmApi.Domain.ValueObjects;

namespace GraphRAGLlmApi.Domain.Interfaces
{
    public interface IVectorDbService
    {
        Task StoreEmbeddingAsync(Embedding embedding);
        Task<Embedding> GetEmbeddingAsync(Guid documentId);
        Task<IEnumerable<Embedding>> GetSimilarEmbeddingsAsync(EmbeddingVector vector, int limit);
        Task<Guid> GetSimilarDocumentsAsync(string query, CancellationToken cancellationToken);
        Task StoreDocumentAsync(Document document, CancellationToken cancellationToken);
        Task<List<Embedding>> GetEmbeddingsAsync(List<int> list);

    }
}