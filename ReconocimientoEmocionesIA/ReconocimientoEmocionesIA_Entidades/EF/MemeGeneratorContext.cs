using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReconocimientoEmocionesIA_Entidades.EF;

//PM> Scaffold-DbContext "Server=localhost;Database=meme_generator;User=root;Password=admin;" MySql.EntityFrameworkCore -OutputDir EF
public partial class MemeGeneratorContext : DbContext
{
    public MemeGeneratorContext()
    {
    }

    public MemeGeneratorContext(DbContextOptions<MemeGeneratorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Emotion> Emotions { get; set; }

    public virtual DbSet<Phrase> Phrases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=meme_generator;User=root;Password=admin;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Emotion>(entity =>
        {
            entity.HasKey(e => e.IdEmotion).HasName("PRIMARY");

            entity.ToTable("emotion");

            entity.Property(e => e.IdEmotion).HasColumnName("id_emotion");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Phrase>(entity =>
        {
            entity.HasKey(e => e.IdPhrase).HasName("PRIMARY");

            entity.ToTable("phrase");

            entity.HasIndex(e => e.IdEmotion, "id_emotion");

            entity.Property(e => e.IdPhrase).HasColumnName("id_phrase");
            entity.Property(e => e.Average).HasColumnName("average");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IdEmotion).HasColumnName("id_emotion");

            entity.HasOne(d => d.IdEmotionNavigation).WithMany(p => p.Phrases)
                .HasForeignKey(d => d.IdEmotion)
                .HasConstraintName("phrase_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
