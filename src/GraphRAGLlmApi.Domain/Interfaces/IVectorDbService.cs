using GraphRAGLlmApi.Domain.Entities;
using GraphRAGLlmApi.Domain.ValueObjects;

namespace GraphRAGLlmApi.Domain.Interfaces
{
    public interface IVectorDbService
    {
        Task StoreEmbeddingAsync(Embedding embedding);
        Task<Embedding> GetEmbeddingAsync(Guid documentId);
        Task<List<Embedding>> GetSimilarEmbeddingsAsync(EmbeddingVector vector, int limit, CancellationToken cancellationToken = default);
        Task StoreDocumentAsync(Document document, CancellationToken cancellationToken);
        Task<List<Embedding>> GetEmbeddingsAsync(List<Guid> list);
        // Update IVectorDbService.cs to include:
        Task<List<Document>> GetDocumentsByIdsAsync(List<Guid> documentIds, CancellationToken cancellationToken = default);

    }
}