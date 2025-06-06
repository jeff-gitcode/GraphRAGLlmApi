using System.Threading;
using System.Threading.Tasks;
using MediatR;
using GraphRAGLlmApi.Domain.Interfaces;

namespace GraphRAGLlmApi.Application.Queries.GetGraphConnections
{
    public class GetGraphConnectionsQueryHandler : IRequestHandler<GetGraphConnectionsQuery, GraphConnectionsResponse>
    {
        private readonly IGraphService _graphService;

        public GetGraphConnectionsQueryHandler(IGraphService graphService)
        {
            _graphService = graphService;
        }

        public async Task<GraphConnectionsResponse> Handle(GetGraphConnectionsQuery request, CancellationToken cancellationToken)
        {
            var connections = await _graphService.GetConnectionsAsync(request.DocumentId, cancellationToken);
            return new GraphConnectionsResponse
            {
                Connections = connections
            };
        }
    }
}