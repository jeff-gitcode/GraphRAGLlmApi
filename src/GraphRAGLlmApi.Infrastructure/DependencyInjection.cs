using GraphRAGLlmApi.Application.Services;
using GraphRAGLlmApi.Domain.Interfaces;
using GraphRAGLlmApi.Infrastructure.LlmServices;
using GraphRAGLlmApi.Infrastructure.Persistence;
using GraphRAGLlmApi.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;

namespace GraphRAGLlmApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Create a data source with vector support
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.UseVector();
            var dataSource = dataSourceBuilder.Build();

            // Register DbContext with the configured data source
            services.AddDbContext<PostgresVectorDbContext>(options =>
                options.UseNpgsql(dataSource));

            // Register HttpClient without passing string parameter
            services.AddHttpClient<OllamaService>();

            // Register repositories
            services.AddScoped<IVectorDbService, VectorRepository>();
            services.AddScoped<IGraphService, GraphRepository>();

            // Register OllamaService to ILlmService
            services.AddScoped<ILlmService, OllamaService>();

            // Add other infrastructure services
            // services.AddScoped<IEmbeddingService, EmbeddingService>();
            services.AddScoped<IRerankingService, RerankingService>();

            return services;
        }
    }
}