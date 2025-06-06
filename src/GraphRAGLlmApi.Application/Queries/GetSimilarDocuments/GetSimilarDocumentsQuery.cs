using GraphRAGLlmApi.Domain.Entities;
using MediatR;

namespace GraphRAGLlmApi.Application.Queries.GetSimilarDocuments
{
    public class GetSimilarDocumentsQuery : IRequest<List<Document>>
    {
        public string QueryText { get; set; }
        public int Limit { get; set; }

        public GetSimilarDocumentsQuery(string queryText, int limit)
        {
            QueryText = queryText;
            Limit = limit;
        }
    }
}