using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphRAGLlmApi.Domain.Interfaces
{
    public interface ISemanticKernelService
    {
        Task<string> GenerateEmbeddingAsync(string text, CancellationToken cancellationToken = default);

        Task<string> GenerateResponseAsync(string prompt, CancellationToken cancellationToken = default);

        // Add other methods that your implementation provides
    }
}