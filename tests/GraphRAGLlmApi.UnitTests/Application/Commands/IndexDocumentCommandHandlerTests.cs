// using System.Threading;
// using System.Threading.Tasks;
// using FluentAssertions;
// using MediatR;
// using Moq;
// using Xunit;
// using GraphRAGLlmApi.Application.Commands.IndexDocument;
// using GraphRAGLlmApi.Domain.Interfaces;

// namespace GraphRAGLlmApi.UnitTests.Application.Commands
// {
//     public class IndexDocumentCommandHandlerTests
//     {
//         private readonly Mock<IVectorDbService> _vectorDbServiceMock;
//         private readonly Mock<IGraphService> _graphServiceMock;
//         private readonly IndexDocumentCommandHandler _handler;

//         public IndexDocumentCommandHandlerTests()
//         {
//             _vectorDbServiceMock = new Mock<IVectorDbService>();
//             _graphServiceMock = new Mock<IGraphService>();
//             _handler = new IndexDocumentCommandHandler(_vectorDbServiceMock.Object, _graphServiceMock.Object);
//         }

//         [Fact]
//         public async Task Handle_ShouldIndexDocument_WhenCommandIsValid()
//         {
//             // Arrange
//             var command = new IndexDocumentCommand
//             {
//                 Title = "Test Document",
//                 Content = "This is a test document."
//             };

//             // Act
//             var result = await _handler.Handle(command, CancellationToken.None);

//             // Assert
//             result.Should().BeTrue();
//             _vectorDbServiceMock.Verify(v => v.StoreEmbedding(It.IsAny<Embedding>()), Times.Once);
//             _graphServiceMock.Verify(g => g.AddGraphNode(It.IsAny<GraphNode>()), Times.Once);
//         }

//         [Fact]
//         public async Task Handle_ShouldThrowException_WhenCommandIsInvalid()
//         {
//             // Arrange
//             var command = new IndexDocumentCommand
//             {
//                 Title = string.Empty, // Invalid title
//                 Content = "This is a test document."
//             };

//             // Act
//             Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

//             // Assert
//             await act.Should().ThrowAsync<ArgumentException>();
//         }
//     }
// }