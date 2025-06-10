using Microsoft.Extensions.DependencyInjection;
using MediatR;
using GraphRAGLlmApi.Application.Commands.IndexDocument;
using GraphRAGLlmApi.Application.Commands.GenerateResponse;
using GraphRAGLlmApi.Application.Queries.GetSimilarDocuments;
using GraphRAGLlmApi.Application.Queries.GetGraphConnections;
using GraphRAGLlmApi.Application.Services;
using GraphRAGLlmApi.Domain.Interfaces;
using GraphRAGLlmApi.Domain.Entities;
using System.Collections.Generic;

namespace GraphRAGLlmApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Updated MediatR registration syntax for v12+
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // Register command handlers
            services.AddTransient<IRequestHandler<IndexDocumentCommand, Unit>, IndexDocumentCommandHandler>();
            services.AddTransient<IRequestHandler<GenerateResponseCommand, string>, GenerateResponseCommandHandler>();

            // Register query handlers
            services.AddTransient<IRequestHandler<GetSimilarDocumentsQuery, List<Document>>, GetSimilarDocumentsQueryHandler>();
            services.AddTransient<IRequestHandler<GetGraphConnectionsQuery, GraphConnectionsResponse>, GetGraphConnectionsQueryHandler>();

            // Register application services
            services.AddTransient<GraphRAGService>();
            services.AddTransient<RerankingService>();

            return services;
        }
    }
}