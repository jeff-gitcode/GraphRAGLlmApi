// using System;
// using Xunit;
// using GraphRAGLlmApi.Domain.Entities;

// namespace GraphRAGLlmApi.UnitTests.Domain.Entities
// {
//     public class EmbeddingTests
//     {
//         [Fact]
//         public void Embedding_CanBeCreated_WithValidParameters()
//         {
//             // Arrange
//             var documentId = Guid.NewGuid();
//             var vector = new float[] { 0.1f, 0.2f, 0.3f };

//             // Act
//             var embedding = new Embedding
//             {
//                 Id = Guid.NewGuid(),
//                 DocumentId = documentId,
//                 Vector = vector
//             };

//             // Assert
//             Assert.NotNull(embedding);
//             Assert.Equal(documentId, embedding.DocumentId);
//             Assert.Equal(vector, embedding.Vector);
//         }

//         [Fact]
//         public void Embedding_ThrowsException_WhenVectorIsNull()
//         {
//             // Arrange
//             var documentId = Guid.NewGuid();

//             // Act & Assert
//             var exception = Assert.Throws<ArgumentNullException>(() => new Embedding
//             {
//                 Id = Guid.NewGuid(),
//                 DocumentId = documentId,
//                 Vector = null
//             });

//             Assert.Equal("Vector cannot be null. (Parameter 'Vector')", exception.Message);
//         }
//     }
// }