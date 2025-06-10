using GraphRAGLlmApi.Domain.Entities;
using GraphRAGLlmApi.Domain.ValueObjects;
using MediatR;

namespace GraphRAGLlmApi.Application.Queries.GetSimilarDocuments
{
    public class GetSimilarDocumentsQuery : IRequest<List<Document>>
    {
        public EmbeddingVector Vector { get; set; }
        public string Query { get; set; }
        public Guid? DocumentId { get; set; }
        
        // Constructor for vector similarity search
        public GetSimilarDocumentsQuery(EmbeddingVector vector, string query)
        {
            Vector = vector;
            Query = query;
        }
        
        // Constructor for document ID search
        public GetSimilarDocumentsQuery(string documentId)
        {
            DocumentId = Guid.Parse(documentId);
            Query = string.Empty;
        }
    }
}