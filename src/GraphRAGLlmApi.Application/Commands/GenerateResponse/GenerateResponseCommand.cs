namespace GraphRAGLlmApi.Application.Commands.GenerateResponse
{
    using MediatR;

    public class GenerateResponseCommand : IRequest<string>
    {
        public string Query { get; set; }
        public string Context { get; set; }

        public GenerateResponseCommand(string query, string context)
        {
            Query = query;
            Context = context;
        }
    }
}