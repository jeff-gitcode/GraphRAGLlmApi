using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using GraphRAGLlmApi.Application.Services;
using GraphRAGLlmApi.Domain.Interfaces;

namespace GraphRAGLlmApi.UnitTests.Application.Services
{
    public class GraphRAGServiceTests
    {
        private readonly Mock<ILlmService> _llmServiceMock;
        private readonly Mock<IVectorDbService> _vectorDbServiceMock;
        private readonly Mock<IGraphService> _graphServiceMock;
        private readonly GraphRAGService _graphRAGService;

        public GraphRAGServiceTests()
        {
            _llmServiceMock = new Mock<ILlmService>();
            _vectorDbServiceMock = new Mock<IVectorDbService>();
            _graphServiceMock = new Mock<IGraphService>();
            _graphRAGService = new GraphRAGService(_llmServiceMock.Object, _vectorDbServiceMock.Object, _graphServiceMock.Object);
        }

        [Fact]
        public async Task GenerateResponse_ShouldReturnExpectedResult()
        {
            // Arrange
            var input = "Sample input for LLM";
            var expectedResponse = "Expected response from LLM";
            _llmServiceMock.Setup(x => x.GenerateResponseAsync(input, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _graphRAGService.GenerateResponse(input);

            // Assert
            Assert.Equal(expectedResponse, result);
            _llmServiceMock.Verify(x => x.GenerateResponseAsync(input, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task RetrieveSimilarDocuments_ShouldReturnExpectedDocuments()
        {
            // Arrange
            var inputDocumentId = 1;
            var expectedDocuments = new List<Document> { new Document { Id = 1, Title = "Doc1" } };
            _vectorDbServiceMock.Setup(x => x.GetSimilarDocumentsAsync(inputDocumentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedDocuments);

            // Act
            var result = await _graphRAGService.RetrieveSimilarDocuments(inputDocumentId);

            // Assert
            Assert.Equal(expectedDocuments, result);
            _vectorDbServiceMock.Verify(x => x.GetSimilarDocumentsAsync(inputDocumentId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}