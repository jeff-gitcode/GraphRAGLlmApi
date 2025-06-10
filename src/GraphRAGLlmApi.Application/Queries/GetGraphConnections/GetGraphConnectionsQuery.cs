using MediatR;

namespace GraphRAGLlmApi.Application.Queries.GetGraphConnections
{
    public class GetGraphConnectionsQuery : IRequest<GraphConnectionsResponse>
    {
        public int DocumentId { get; set; }

        public GetGraphConnectionsQuery(int documentId)
        {
            DocumentId = documentId;
        }
    }
}