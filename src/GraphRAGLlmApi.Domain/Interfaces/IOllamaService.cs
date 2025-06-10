using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphRAGLlmApi.Domain.Entities;
using GraphRAGLlmApi.Domain.ValueObjects;

namespace GraphRAGLlmApi.Domain.Interfaces
{
    public interface IOllamaService
    {
        Task<EmbeddingVector> GenerateEmbeddingAsync(string content, CancellationToken cancellationToken = default);

        Task<string> GenerateResponseAsync(string prompt);

        Task<string> GenerateResponseAsync(string query, IEnumerable<Document> similarDocuments,
            IEnumerable<GraphConnection> graphConnections, CancellationToken cancellationToken = default);

    }
}