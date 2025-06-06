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
            var similarDocuments = await _vectorDbService.GetSimilarDocumentsAsync(query, cancellationToken);
            var graphConnections = await _graphService.GetGraphConnectionsAsync(similarDocuments, cancellationToken);
            var response = await _llmService.GenerateResponseAsync(query, similarDocuments, graphConnections, cancellationToken);
            return response;
        }
    }
}