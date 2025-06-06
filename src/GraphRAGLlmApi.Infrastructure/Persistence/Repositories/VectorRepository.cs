using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphRAGLlmApi.Domain.Entities;
using GraphRAGLlmApi.Domain.Interfaces;
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
    }
}