using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using GraphRAGLlmApi.Application.Queries.GetSimilarDocuments;
using GraphRAGLlmApi.Application.Queries.GetGraphConnections;

namespace GraphRAGLlmApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QueriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("similar-documents")]
        public async Task<IActionResult> GetSimilarDocuments([FromQuery] string documentId)
        {
            var query = new GetSimilarDocumentsQuery(documentId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("graph-connections")]
        public async Task<IActionResult> GetGraphConnections([FromQuery] string documentId)
        {
            if (!int.TryParse(documentId, out var documentIdInt))
            {
                return BadRequest("Invalid documentId. It must be an integer.");
            }
            var query = new GetGraphConnectionsQuery(documentIdInt);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}