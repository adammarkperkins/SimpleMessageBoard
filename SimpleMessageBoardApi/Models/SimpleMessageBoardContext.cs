using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SimpleMessageBoardApi.Models
{
    public partial class SimpleMessageBoardContext : DbContext
    {
        public SimpleMessageBoardContext()
        {
        }

        public SimpleMessageBoardContext(DbContextOptions<SimpleMessageBoardContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MessageItem> MessageItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Data:DefaultConnection:ConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<MessageItem>(entity =>
            {
                entity.ToTable("MessageItem");

                entity.Property(e => e.Added)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.User).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
