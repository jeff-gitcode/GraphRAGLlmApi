using GraphRAGLlmApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Pgvector.EntityFrameworkCore;

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
        // REMOVE THIS LINE: public DbSet<EmbeddingVector> EmbeddingVectors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Enable vector extension
            modelBuilder.HasPostgresExtension("vector");

            modelBuilder.Entity<Document>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Embedding>()
                .HasKey(e => e.Id);

            // Configure Embedding.Vector as owned entity
            modelBuilder.Entity<Embedding>()
                .OwnsOne(e => e.Vector, eb =>
                {
                    eb.Property(v => v.Values)
                       .HasColumnType("vector");
                });

            modelBuilder.Entity<GraphNode>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<GraphConnection>()
                .HasKey(gc => gc.Id);

            // GraphConnection: SourceNode
            modelBuilder.Entity<GraphConnection>()
                .HasOne(gc => gc.SourceNode)
                .WithMany(gn => gn.OutgoingConnections)
                .HasForeignKey(gc => gc.SourceNodeId)
                .OnDelete(DeleteBehavior.Restrict);

            // GraphConnection: TargetNode
            modelBuilder.Entity<GraphConnection>()
                .HasOne(gc => gc.TargetNode)
                .WithMany(gn => gn.IncomingConnections)
                .HasForeignKey(gc => gc.TargetNodeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Document to Embedding relationship (optional)
            modelBuilder.Entity<Document>()
                .HasOne(d => d.Embedding)
                .WithOne(e => e.Document)
                .HasForeignKey<Embedding>(e => e.DocumentId);
        }
    }
}