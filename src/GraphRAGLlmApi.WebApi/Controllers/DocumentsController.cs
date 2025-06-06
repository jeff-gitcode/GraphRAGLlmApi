using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using GraphRAGLlmApi.Application.Commands.IndexDocument;
using GraphRAGLlmApi.Application.Commands.GenerateResponse;
using GraphRAGLlmApi.Application.Queries.GetSimilarDocuments;
using GraphRAGLlmApi.Application.Queries.GetGraphConnections;

namespace GraphRAGLlmApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("index")]
        public async Task<IActionResult> IndexDocument([FromBody] IndexDocumentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("generate-response")]
        public async Task<IActionResult> GenerateResponse([FromBody] GenerateResponseCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("similar-documents")]
        public async Task<IActionResult> GetSimilarDocuments([FromQuery] GetSimilarDocumentsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("graph-connections")]
        public async Task<IActionResult> GetGraphConnections([FromQuery] GetGraphConnectionsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}