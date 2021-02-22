using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities.Models
{
    public partial class OCR_DBContext : DbContext
    {
        public OCR_DBContext()
        {
        }

        public OCR_DBContext(DbContextOptions<OCR_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answerscriptloc> Answerscriptloc { get; set; }
        public virtual DbSet<Main> Main { get; set; }
        public virtual DbSet<Marksheet> Marksheet { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning: 'To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.'
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root#1234;database=OCR_DB");
#pragma warning restore CS1030 // #warning: 'To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.'
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answerscriptloc>(entity =>
            {
                entity.HasKey(e => e.IdAnswerScriptLoc)
                    .HasName("PRIMARY");

                entity.ToTable("answerscriptloc");

                entity.Property(e => e.IdAnswerScriptLoc).HasColumnName("idAnswerScriptLoc");

                entity.Property(e => e.AnswerScriptLoc1)
                    .HasColumnName("AnswerScriptLoc")
                    .HasMaxLength(256);

                entity.Property(e => e.CropImgLoc).HasMaxLength(256);
            });

            modelBuilder.Entity<Main>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("main");
            });

            modelBuilder.Entity<Marksheet>(entity =>
            {
                entity.HasKey(e => e.MarksheetTableId)
                    .HasName("PRIMARY");

                entity.ToTable("marksheet");

                entity.HasIndex(e => e.StudentId)
                    .HasName("StudentId_idx");

                entity.HasIndex(e => e.SubjectId)
                    .HasName("SubjectId_idx");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Marksheet)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("StudentId");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Marksheet)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("SubjectId");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.Name).HasMaxLength(45);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.IdSubjec)
                    .HasName("PRIMARY");

                entity.ToTable("subject");

                entity.Property(e => e.IdSubjec).HasColumnName("idSubjec");

                entity.Property(e => e.SubjectName).HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
