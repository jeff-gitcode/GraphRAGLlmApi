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
            // Retrieve relevant documents from the vector database
            var similarDocuments = await _vectorDbService.GetSimilarDocumentsAsync(request.Query, cancellationToken);

            // Retrieve graph connections if needed
            var graphConnections = await _graphService.GetGraphConnectionsAsync(similarDocuments, cancellationToken);

            // Generate response using the LLM service
            var response = await _llmService.GenerateResponseAsync(request.Query, similarDocuments, graphConnections, cancellationToken);

            return response;
        }
    }
}