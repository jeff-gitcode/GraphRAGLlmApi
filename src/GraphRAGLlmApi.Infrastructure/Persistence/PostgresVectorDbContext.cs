using GraphRAGLlmApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphRAGLlmApi.Infrastructure.Persistence
{
    public class PostgresVectorDbContext : DbContext
    {
        public PostgresVectorDbContext(DbContextOptions<PostgresVectorDbContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Embedding> Embeddings { get; set; }
        public DbSet<GraphNode> GraphNodes { get; set; }
        public DbSet<GraphConnection> GraphConnections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Embedding>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<GraphNode>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<GraphConnection>()
                .HasKey(gc => gc.Id);


            // Additional configurations can be added here
        }
    }
}