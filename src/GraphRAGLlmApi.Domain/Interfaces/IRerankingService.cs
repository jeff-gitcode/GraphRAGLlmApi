using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphRAGLlmApi.Domain.Entities;

namespace GraphRAGLlmApi.Domain.Interfaces
{
    public interface IRerankingService
    {
        public Task<List<Document>> RerankDocumentsAsync(List<Document> documents, string query, CancellationToken cancellationToken = default);
    }
}