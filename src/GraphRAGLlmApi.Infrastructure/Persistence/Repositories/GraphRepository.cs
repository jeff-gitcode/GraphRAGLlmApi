using System.Collections.Generic;
using System.Threading.Tasks;
using GraphRAGLlmApi.Domain.Entities;
using GraphRAGLlmApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<GraphNode>> GetGraphConnectionsAsync(int documentId)
        {
            return await _context.GraphNodes
                .Include(node => node.Connections)
                .Where(node => node.DocumentId == documentId)
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
    }
}