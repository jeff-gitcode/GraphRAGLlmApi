public class GraphNode
{
    public int Id { get; set; }
    public int DocumentId { get; set; }
    public List<int> Connections { get; set; } = new List<int>();
}