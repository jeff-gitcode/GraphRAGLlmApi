namespace GraphRAGLlmApi.Domain.Entities
{
    public class GraphNode
    {
        public int Id { get; set; }
        public Guid DocumentId { get; set; }
        public string Type { get; set; }
        public string Label { get; set; } // Added Label property
        public ICollection<GraphConnection> Connections { get; set; }
    }
}