using System.Threading;
using System.Threading.Tasks;
using MediatR;
using GraphRAGLlmApi.Domain.Interfaces;

namespace GraphRAGLlmApi.Application.Commands.GenerateResponse
{
    public class GenerateResponseCommandHandler : IRequestHandler<GenerateResponseCommand, string>
    {
        private readonly ILlmService _llmService;
        private readonly IVectorDbService _vectorDbService;
        private readonly IGraphService _graphService;

        public GenerateResponseCommandHandler(ILlmService llmService, IVectorDbService vectorDbService, IGraphService graphService)
        {
            _llmService = llmService;
            _vectorDbService = vectorDbService;
            _graphService = graphService;
        }

        public async Task<string> Handle(GenerateResponseCommand request, CancellationToken cancellationToken)
        {
            // inject IEmbeddingService to generate embeddings from text
            var queryEmbedding = await _llmService.GenerateEmbeddingAsync(request.Query, cancellationToken);

            // Find similar embeddings
            var similarEmbeddings = await _vectorDbService.GetSimilarEmbeddingsAsync(queryEmbedding, 10, cancellationToken);

            // Get the documents
            var documentIds = similarEmbeddings.Select(e => e.DocumentId).ToList();
            var similarDocuments = await _vectorDbService.GetDocumentsByIdsAsync(documentIds, cancellationToken);

            // Use the ID of the most similar document (or empty GUID if none found)
            var mostRelevantDocId = similarDocuments.FirstOrDefault()?.Id ?? Guid.Empty;

            // Retrieve graph connections if needed
            var graphConnections = await _graphService.GetGraphConnectionsAsync(mostRelevantDocId, cancellationToken);

            // Generate response using the LLM service
            var response = await _llmService.GenerateResponseAsync(request.Query, mostRelevantDocId, graphConnections, cancellationToken);

            return response;
        }
    }
}