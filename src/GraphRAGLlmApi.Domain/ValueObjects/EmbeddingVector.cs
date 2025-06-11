namespace GraphRAGLlmApi.Domain.ValueObjects
{
    public class EmbeddingVector
    {
        public float[] Values { get; set; }

        public EmbeddingVector() { }

        public EmbeddingVector(float[] values)
        {
            Values = values;
        }
    }
}