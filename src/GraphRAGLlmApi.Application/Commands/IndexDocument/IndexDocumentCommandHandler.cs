using System.Threading;
using System.Threading.Tasks;
using MediatR;
using GraphRAGLlmApi.Domain.Interfaces;
using GraphRAGLlmApi.Domain.Entities;

namespace GraphRAGLlmApi.Application.Commands.IndexDocument
{
    public class IndexDocumentCommandHandler : IRequestHandler<IndexDocumentCommand, Unit>
    {
        private readonly IVectorDbService _vectorDbService;
        private readonly ILlmService _llmService;
        private readonly IGraphService _graphService;

        public IndexDocumentCommandHandler(IVectorDbService vectorDbService, ILlmService llmService, IGraphService graphService)
        {
            _vectorDbService = vectorDbService;
            _llmService = llmService;
            _graphService = graphService;
        }

        public async Task<Unit> Handle(IndexDocumentCommand request, CancellationToken cancellationToken)
        {
            // Create a new Document entity
            var document = new Document
            {
                Title = request.Title,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow
            };

            // Store the document in the vector database
            await _vectorDbService.StoreDocumentAsync(document, cancellationToken);

            // Generate the embedding for the document
            var embedding = await _llmService.GenerateEmbeddingAsync(document.Content, cancellationToken);

            // Store the embedding in the vector database
            await _vectorDbService.StoreEmbeddingAsync(new Embedding
            {
                DocumentId = document.Id,
                Vector = embedding
            });

            // Update the graph structure
            await _graphService.AddDocumentToGraphAsync(document, cancellationToken);

            return Unit.Value;
        }
    }
}