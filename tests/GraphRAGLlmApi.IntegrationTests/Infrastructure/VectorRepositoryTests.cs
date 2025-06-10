// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using Xunit;
// using GraphRAGLlmApi.Infrastructure.Persistence.Repositories;
// using GraphRAGLlmApi.Infrastructure.Persistence;
// using GraphRAGLlmApi.Domain.Entities;

// namespace GraphRAGLlmApi.IntegrationTests.Infrastructure
// {
//     public class VectorRepositoryTests : IClassFixture<DatabaseFixture>
//     {
//         private readonly VectorRepository _vectorRepository;
//         private readonly PostgresVectorDbContext _context;

//         public VectorRepositoryTests(DatabaseFixture fixture)
//         {
//             _context = fixture.Context;
//             _vectorRepository = new VectorRepository(_context);
//         }

//         [Fact]
//         public async Task AddEmbedding_ShouldAddEmbeddingToDatabase()
//         {
//             // Arrange
//             var embedding = new Embedding
//             {
//                 DocumentId = 1,
//                 Vector = new float[] { 0.1f, 0.2f, 0.3f } // Example vector
//             };

//             // Act
//             await _vectorRepository.AddAsync(embedding);
//             await _context.SaveChangesAsync();

//             // Assert
//             var result = await _context.Embeddings.FirstOrDefaultAsync(e => e.DocumentId == embedding.DocumentId);
//             Assert.NotNull(result);
//             Assert.Equal(embedding.Vector, result.Vector);
//         }

//         [Fact]
//         public async Task GetEmbedding_ShouldReturnEmbeddingFromDatabase()
//         {
//             // Arrange
//             var embedding = new Embedding
//             {
//                 DocumentId = 2,
//                 Vector = new float[] { 0.4f, 0.5f, 0.6f } // Example vector
//             };
//             await _vectorRepository.AddAsync(embedding);
//             await _context.SaveChangesAsync();

//             // Act
//             var result = await _vectorRepository.GetByDocumentIdAsync(2);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(embedding.Vector, result.Vector);
//         }
//     }
// }