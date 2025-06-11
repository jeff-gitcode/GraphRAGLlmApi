namespace GraphRAGLlmApi.Domain.Entities
{
    public class GraphNode
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid DocumentId { get; set; }
        public Document Document { get; set; }

        public ICollection<GraphConnection> OutgoingConnections { get; set; } = new List<GraphConnection>();
        public ICollection<GraphConnection> IncomingConnections { get; set; } = new List<GraphConnection>();
    }

}