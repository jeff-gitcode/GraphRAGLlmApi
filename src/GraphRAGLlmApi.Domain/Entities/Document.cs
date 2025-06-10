namespace GraphRAGLlmApi.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Document()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
