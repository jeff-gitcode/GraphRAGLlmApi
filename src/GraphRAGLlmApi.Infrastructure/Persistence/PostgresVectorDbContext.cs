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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Embedding>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<GraphNode>()
                .HasKey(g => g.Id);

            // Additional configurations can be added here
        }
    }
}