using GraphRAGLlmApi.Domain.ValueObjects;

public class Embedding
{
    public int Id { get; set; }
    public Guid DocumentId { get; set; }
    public EmbeddingVector Vector { get; set; }
}