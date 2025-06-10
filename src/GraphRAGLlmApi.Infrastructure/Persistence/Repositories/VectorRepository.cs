using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphRAGLlmApi.Domain.Entities;
using GraphRAGLlmApi.Domain.Interfaces;
using GraphRAGLlmApi.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GraphRAGLlmApi.Infrastructure.Persistence.Repositories
{
    public class VectorRepository : IVectorDbService
    {
        private readonly PostgresVectorDbContext _context;

        public VectorRepository(PostgresVectorDbContext context)
        {
            _context = context;
        }

        public async Task<Embedding> AddEmbeddingAsync(Embedding embedding)
        {
            _context.Embeddings.Add(embedding);
            await _context.SaveChangesAsync();
            return embedding;
        }

        public async Task<List<Embedding>> GetSimilarEmbeddingsAsync(EmbeddingVector vector, int limit)
        {
            return await _context.Embeddings
                .FromSqlRaw("SELECT * FROM embeddings ORDER BY vector <-> {0} LIMIT {1}", vector, limit)
                .ToListAsync();
        }

        public async Task<Embedding> GetEmbeddingByIdAsync(int id)
        {
            return await _context.Embeddings.FindAsync(id);
        }

        public async Task<List<Embedding>> GetAllEmbeddingsAsync()
        {
            return await _context.Embeddings.ToListAsync();
        }

        public Task StoreEmbeddingAsync(Embedding embedding)
        {
            _context.Embeddings.Add(embedding);
            return _context.SaveChangesAsync();
        }

        public Task<Embedding> GetEmbeddingAsync(Guid documentId)
        {
            return _context.Embeddings
                .FirstOrDefaultAsync(e => e.DocumentId == documentId);
        }

        public Task StoreDocumentAsync(Document document, CancellationToken cancellationToken)
        {
            _context.Documents.Add(document);
            return _context.SaveChangesAsync(cancellationToken);
        }

        public Task<List<Embedding>> GetEmbeddingsAsync(List<Guid> list)
        {
            return _context.Embeddings
                .Where(e => list.Contains(e.DocumentId))
                .ToListAsync();
        }

        public async Task<List<Document>> GetDocumentsByIdsAsync(List<Guid> documentIds, CancellationToken cancellationToken = default)
        {
            return await _context.Documents
                .Where(d => documentIds.Contains(d.Id))
                .ToListAsync(cancellationToken);
        }

        public Task<List<Embedding>> GetSimilarEmbeddingsAsync(EmbeddingVector vector, int limit, CancellationToken cancellationToken = default)
        {
            return _context.Embeddings
                .FromSqlRaw("SELECT * FROM embeddings ORDER BY vector <-> {0} LIMIT {1}", vector, limit)
                .ToListAsync(cancellationToken);
        }
    }
}