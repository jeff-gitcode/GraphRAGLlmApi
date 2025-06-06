using System.Threading;
using System.Threading.Tasks;
using MediatR;
using GraphRAGLlmApi.Domain.Interfaces;
using GraphRAGLlmApi.Domain.Entities;

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
            var similarDocuments = await _vectorDbService.GetSimilarDocumentsAsync(request.EmbeddingVector, cancellationToken);

            // Re-rank the documents based on relevance
            var rankedDocuments = _rerankingService.RerankDocuments(similarDocuments, request.Query);

            return rankedDocuments;
        }
    }
}