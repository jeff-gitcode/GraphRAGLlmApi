using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphRAGLlmApi.Domain.Entities;

namespace GraphRAGLlmApi.Domain.Interfaces
{
    public interface IRerankingService
    {
        public List<Document> RerankDocuments(object similarDocuments, object query);

        public Task<IEnumerable<Document>> RerankAsync(IEnumerable<Document> documents, string query, CancellationToken cancellationToken = default);
    }
}