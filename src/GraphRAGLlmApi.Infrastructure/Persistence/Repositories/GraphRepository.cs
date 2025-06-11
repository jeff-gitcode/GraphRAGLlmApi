using System.Collections.Generic;
using System.Threading.Tasks;
using GraphRAGLlmApi.Domain.Entities;
using GraphRAGLlmApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;

namespace GraphRAGLlmApi.Infrastructure.Persistence.Repositories
{
    public class GraphRepository : IGraphService
    {
        private readonly PostgresVectorDbContext _context;

        public GraphRepository(PostgresVectorDbContext context)
        {
            _context = context;
        }

        public async Task<GraphNode> GetGraphNodeAsync(int id)
        {
            return await _context.GraphNodes.FindAsync(id);
        }

        public async Task<IEnumerable<GraphNode>> GetGraphConnectionsAsync(Guid documentId)
        {
            return await _context.GraphNodes
                .Where(n => n.DocumentId.ToString() == documentId.ToString())
                .ToListAsync();
        }

        public async Task AddGraphNodeAsync(GraphNode graphNode)
        {
            await _context.GraphNodes.AddAsync(graphNode);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGraphNodeAsync(GraphNode graphNode)
        {
            _context.GraphNodes.Update(graphNode);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGraphNodeAsync(int id)
        {
            var graphNode = await GetGraphNodeAsync(id);
            if (graphNode != null)
            {
                _context.GraphNodes.Remove(graphNode);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<GraphConnection>> GetGraphConnectionsAsync(Guid documentId, CancellationToken cancellationToken = default)
        {
            return await _context.GraphConnections
                .Where(c => c.SourceNode.DocumentId.ToString() == documentId.ToString() || c.TargetNode.DocumentId.ToString() == documentId.ToString())
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<GraphNode>> GetRelatedNodesAsync(Guid documentId)
        {
            var connections = await _context.GraphConnections
                .Include(c => c.SourceNode)
                .Include(c => c.TargetNode)
                .Where(c => c.SourceNode.DocumentId.ToString() == documentId.ToString() || c.TargetNode.DocumentId.ToString() == documentId.ToString())
                .ToListAsync();

            return connections
                .SelectMany(c => new[] { c.SourceNode, c.TargetNode })
                .Where(n => n.DocumentId.ToString() != documentId.ToString())
                .Distinct()
                .ToList();
        }

        public async Task DeleteGraphNodeAsync(Guid documentId)
        {
            var nodes = await _context.GraphNodes
                .Where(n => n.DocumentId.ToString() == documentId.ToString())
                .ToListAsync();

            _context.GraphNodes.RemoveRange(nodes);
            await _context.SaveChangesAsync();
        }

        public async Task AddDocumentToGraphAsync(Document document, CancellationToken cancellationToken = default)
        {
            var graphNode = new GraphNode
            {
                Id = Guid.NewGuid(),
                Name = document.Title,
                Type = "Document",
                DocumentId = document.Id,
                Document = document,
                OutgoingConnections = new List<GraphConnection>(),
                IncomingConnections = new List<GraphConnection>()
            };

            await _context.GraphNodes.AddAsync(graphNode, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<GraphConnection>> GetConnectionsAsync(object nodeId, CancellationToken cancellationToken = default)
        {
            if (nodeId is Guid guidId)
            {
                return await _context.GraphConnections
                    .Where(c => c.SourceNodeId == guidId || c.TargetNodeId == guidId)
                    .ToListAsync(cancellationToken);
            }
            else if (nodeId is int intId)
            {
                return await _context.GraphConnections
                    .Where(c => c.SourceNodeId == new Guid(intId.ToString()) || c.TargetNodeId == new Guid(intId.ToString()))
                    .ToListAsync(cancellationToken);
            }

            throw new ArgumentException("Invalid node ID type.");
        }

        Task<IEnumerable<GraphNode>> IGraphService.GetGraphConnectionsAsync(Guid documentId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}