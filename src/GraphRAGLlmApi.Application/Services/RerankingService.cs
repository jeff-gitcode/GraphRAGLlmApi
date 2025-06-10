using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphRAGLlmApi.Domain.Entities;
using GraphRAGLlmApi.Domain.Interfaces;

namespace GraphRAGLlmApi.Application.Services
{
    public class RerankingService : IRerankingService
    {
        private readonly IVectorDbService _vectorDbService;

        public RerankingService(IVectorDbService vectorDbService)
        {
            _vectorDbService = vectorDbService;
        }

        public async Task<List<Document>> RerankDocumentsAsync(List<Document> documents, string query, CancellationToken cancellationToken = default)
        {
            var embeddings = await _vectorDbService.GetEmbeddingsAsync(documents.Select(d => d.Id).ToList());
            var rerankedDocuments = documents
                .Select(doc => new
                {
                    Document = doc,
                    RelevanceScore = CalculateRelevanceScore(doc, embeddings, query)
                })
                .OrderByDescending(x => x.RelevanceScore)
                .Select(x => x.Document)
                .ToList();

            return rerankedDocuments;
        }


        private double CalculateRelevanceScore(Document document, List<Embedding> embeddings, string query)
        {
            // Implement your scoring logic here based on the document, its embeddings, and the query
            // This is a placeholder for the actual scoring algorithm
            return embeddings.FirstOrDefault(e => e.DocumentId == document.Id)?.Vector.Vector.Length ?? 0;
        }
    }
}