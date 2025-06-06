using Microsoft.Extensions.DependencyInjection;
using GraphRAGLlmApi.Domain.Interfaces;
using GraphRAGLlmApi.Infrastructure.LlmServices;
using GraphRAGLlmApi.Infrastructure.Persistence.Repositories;
using GraphRAGLlmApi.Application.Services;
using MediatR;

namespace GraphRAGLlmApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IVectorDbService, VectorRepository>();
            services.AddScoped<IGraphService, GraphRepository>();
            services.AddScoped<ILlmService, OllamaService>();
            services.AddScoped<RerankingService>();
            services.AddScoped<GraphRAGService>();

            return services;
        }
    }
}