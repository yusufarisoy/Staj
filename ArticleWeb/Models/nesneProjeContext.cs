using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ArticleSpiderWeb.Models
{
    public partial class nesneProjeContext : DbContext
    {
        public nesneProjeContext()
        {
        }

        public nesneProjeContext(DbContextOptions<nesneProjeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategory { get; set; }
        public virtual DbSet<ArticleCategoryRelation> ArticleCategoryRelation { get; set; }
        public virtual DbSet<PageUrl> PageUrl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-RL1II1T\\SQLEXPRESS;Database=nesneProje;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasIndex(e => e.Title)
                    .HasName("IX_Article")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ContentSummary).IsUnicode(false);

                entity.Property(e => e.FullContent).IsUnicode(false);

                entity.Property(e => e.ImageSrc)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TitleUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ArticleCategory>(entity =>
            {
                entity.HasIndex(e => e.CategoryName)
                    .HasName("IX_ArticleCategory")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ArticleCategoryRelation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleCategoryRelation)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleCategoryRelation_Article");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ArticleCategoryRelation)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleCategoryRelation_ArticleCategory");
            });

            modelBuilder.Entity<PageUrl>(entity =>
            {
                entity.ToTable("PageURL");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LinkFormat)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
