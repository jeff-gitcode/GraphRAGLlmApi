namespace GraphRAGLlmApi.Domain.Entities
{
    /// <summary>
    /// Represents a connection between entities in a knowledge graph
    /// </summary>
    public class GraphConnection
    {
        public int Id { get; set; }
        public int SourceNodeId { get; set; }
        public int TargetNodeId { get; set; }
        public string RelationType { get; set; }
        public double Weight { get; set; }

        // Optional - reference navigation properties 
        public GraphNode SourceNode { get; set; }
        public GraphNode TargetNode { get; set; }
    }
}