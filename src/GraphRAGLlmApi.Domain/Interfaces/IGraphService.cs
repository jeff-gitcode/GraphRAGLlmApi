using GraphRAGLlmApi.Domain.Entities;

namespace GraphRAGLlmApi.Domain.Interfaces
{
    public interface IGraphService
    {
        Task<IEnumerable<GraphNode>> GetGraphConnectionsAsync(Guid documentId, CancellationToken cancellationToken);
        Task<IEnumerable<GraphNode>> GetRelatedNodesAsync(Guid nodeId);
        Task AddGraphNodeAsync(GraphNode node);
        Task UpdateGraphNodeAsync(GraphNode node);
        Task DeleteGraphNodeAsync(Guid nodeId);
        Task AddDocumentToGraphAsync(Document document, CancellationToken cancellationToken);
        Task<IEnumerable<GraphConnection>> GetConnectionsAsync(object nodeId, CancellationToken cancellationToken);
    }
}