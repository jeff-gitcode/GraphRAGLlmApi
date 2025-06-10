using System.Threading;
using System.Threading.Tasks;
using MediatR;
using GraphRAGLlmApi.Domain.Interfaces;
using GraphRAGLlmApi.Domain.Entities;
using System.Linq;

namespace GraphRAGLlmApi.Application.Queries.GetSimilarDocuments
{
    public class GetSimilarDocumentsQueryHandler : IRequestHandler<GetSimilarDocumentsQuery, List<Document>>
    {
        private readonly IVectorDbService _vectorDbService;
        private readonly IRerankingService _rerankingService;

        public GetSimilarDocumentsQueryHandler(IVectorDbService vectorDbService, IRerankingService rerankingService)
        {
            _vectorDbService = vectorDbService;
            _rerankingService = rerankingService;
        }

        public async Task<List<Document>> Handle(GetSimilarDocumentsQuery request, CancellationToken cancellationToken)
        {
            // Retrieve similar documents from the vector database
            var similarEmbeddings = await _vectorDbService.GetSimilarEmbeddingsAsync(request!.Vector, 10, cancellationToken);
            
            // Get the full documents based on the embedding IDs
            var documentIds = similarEmbeddings.Select(e => e.DocumentId).ToList();
            var documents = await _vectorDbService.GetDocumentsByIdsAsync(documentIds, cancellationToken);
            
            // Re-rank the documents based on relevance
            var rankedDocuments = await _rerankingService.RerankDocumentsAsync(documents, request.Query);

            return rankedDocuments;
        }
    }
}