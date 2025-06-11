using GraphRAGLlmApi.Domain.Entities;
using GraphRAGLlmApi.Domain.ValueObjects;

public class Embedding
{
    public Guid Id { get; set; }
    public Guid DocumentId { get; set; }
    public EmbeddingVector Vector { get; set; }
    public Document Document { get; set; }
}
