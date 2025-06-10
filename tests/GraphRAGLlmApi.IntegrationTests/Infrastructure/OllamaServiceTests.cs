// using System.Net.Http;
// using System.Net;
// using System.Threading.Tasks;
// using Xunit;
// using Moq;
// using GraphRAGLlmApi.Domain.Interfaces;
// using GraphRAGLlmApi.Infrastructure.LlmServices;

// namespace GraphRAGLlmApi.IntegrationTests.Infrastructure
// {
//     public class OllamaServiceTests
//     {
//         private readonly OllamaService _ollamaService;
//         private readonly Mock<IVectorDbService> _vectorDbServiceMock;

//         public OllamaServiceTests()
//         {
//             _vectorDbServiceMock = new Mock<IVectorDbService>();
//             _ollamaService = new OllamaService(_vectorDbServiceMock.Object);
//         }

//         [Fact]
//         public async Task GenerateResponse_ShouldReturnValidResponse_WhenCalled()
//         {
//             // Arrange
//             var input = "What is the capital of France?";
//             var expectedResponse = "The capital of France is Paris.";
//             _vectorDbServiceMock.Setup(v => v.GetResponseAsync(It.IsAny<string>()))
//                 .ReturnsAsync(expectedResponse);

//             // Act
//             var response = await _ollamaService.GenerateResponseAsync(input);

//             // Assert
//             Assert.Equal(expectedResponse, response);
//         }

//         [Fact]
//         public async Task GenerateResponse_ShouldThrowException_WhenServiceFails()
//         {
//             // Arrange
//             var input = "What is the capital of France?";
//             _vectorDbServiceMock.Setup(v => v.GetResponseAsync(It.IsAny<string>()))
//                 .ThrowsAsync(new HttpRequestException("Service unavailable"));

//             // Act & Assert
//             await Assert.ThrowsAsync<HttpRequestException>(() => _ollamaService.GenerateResponseAsync(input));
//         }
//     }
// }