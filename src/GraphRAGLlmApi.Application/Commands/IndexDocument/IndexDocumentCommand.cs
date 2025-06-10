using MediatR;

namespace GraphRAGLlmApi.Application.Commands.IndexDocument
{
    public class IndexDocumentCommand : IRequest<Unit>
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public IndexDocumentCommand(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}