namespace GraphRAGLlmApi.Domain.ValueObjects
{
    public class EmbeddingVector
    {
        public float[] Vector { get; }

        public EmbeddingVector(float[] vector)
        {
            Vector = vector ?? throw new ArgumentNullException(nameof(vector));
        }

        public override bool Equals(object obj)
        {
            if (obj is EmbeddingVector other)
            {
                return Vector.SequenceEqual(other.Vector);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Vector);
        }
    }
}