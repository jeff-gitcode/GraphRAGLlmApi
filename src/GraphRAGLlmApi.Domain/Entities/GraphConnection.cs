namespace GraphRAGLlmApi.Domain.Entities
{
    /// <summary>
    /// Represents a connection between entities in a knowledge graph
    /// </summary>

    public class GraphConnection
    {
        public Guid Id { get; set; }
        public Guid SourceNodeId { get; set; }
        public Guid TargetNodeId { get; set; }
        public string RelationType { get; set; }
        public double Weight { get; set; }

        public GraphNode SourceNode { get; set; }
        public GraphNode TargetNode { get; set; }
    }
}