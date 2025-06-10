// using System.Net;
// using System.Net.Http;
// using System.Threading.Tasks;
// using Xunit;
// using Microsoft.AspNetCore.Mvc;
// using GraphRAGLlmApi.WebApi.Controllers;
// using GraphRAGLlmApi.Application.Commands.IndexDocument;
// using MediatR;
// using Moq;

// namespace GraphRAGLlmApi.IntegrationTests.Api
// {
//     public class DocumentsControllerTests
//     {
//         private readonly DocumentsController _controller;
//         private readonly Mock<IMediator> _mediatorMock;

//         public DocumentsControllerTests()
//         {
//             _mediatorMock = new Mock<IMediator>();
//             _controller = new DocumentsController(_mediatorMock.Object);
//         }

//         [Fact]
//         public async Task IndexDocument_ReturnsOk_WhenDocumentIsIndexed()
//         {
//             // Arrange
//             var command = new IndexDocumentCommand
//             {
//                 Title = "Test Document",
//                 Content = "This is a test document."
//             };

//             _mediatorMock.Setup(m => m.Send(It.IsAny<IndexDocumentCommand>(), default))
//                 .ReturnsAsync(new Unit());

//             // Act
//             var result = await _controller.IndexDocument(command);

//             // Assert
//             var okResult = Assert.IsType<OkResult>(result);
//             Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
//         }

//         [Fact]
//         public async Task IndexDocument_ReturnsBadRequest_WhenModelIsInvalid()
//         {
//             // Arrange
//             _controller.ModelState.AddModelError("Title", "Required");

//             var command = new IndexDocumentCommand
//             {
//                 Title = "",
//                 Content = "This is a test document."
//             };

//             // Act
//             var result = await _controller.IndexDocument(command);

//             // Assert
//             var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
//             Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
//         }
//     }
// }