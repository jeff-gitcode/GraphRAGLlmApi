using System.Threading.Tasks;
using GraphRAGLlmApi.Domain.Interfaces;
using MediatR;

namespace GraphRAGLlmApi.Application.Services
{
    public class GraphRAGService
    {
        private readonly ILlmService _llmService;
        private readonly IVectorDbService _vectorDbService;
        private readonly IGraphService _graphService;

        public GraphRAGService(ILlmService llmService, IVectorDbService vectorDbService, IGraphService graphService)
        {
            _llmService = llmService;
            _vectorDbService = vectorDbService;
            _graphService = graphService;
        }

        public async Task<string> GenerateResponseAsync(string query, CancellationToken cancellationToken = default)
        {
            // inject IEmbeddingService to generate embeddings from text
            var queryEmbedding = await _llmService.GenerateEmbeddingAsync(query, cancellationToken);

            // Find similar embeddings
            var similarEmbeddings = await _vectorDbService.GetSimilarEmbeddingsAsync(queryEmbedding, 10, cancellationToken);

            // Get the documents
            var documentIds = similarEmbeddings.Select(e => e.DocumentId).ToList();
            var similarDocuments = await _vectorDbService.GetDocumentsByIdsAsync(documentIds, cancellationToken);

            // Use the ID of the most similar document (or empty GUID if none found)
            var mostRelevantDocId = similarDocuments.FirstOrDefault()?.Id ?? Guid.Empty;
            var graphConnections = await _graphService.GetGraphConnectionsAsync(mostRelevantDocId, cancellationToken);

            var response = await _llmService.GenerateResponseAsync(query, mostRelevantDocId, graphConnections, cancellationToken);
            return response;
        }
    }
}