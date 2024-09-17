using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiRestNetforemost.ModelBDNetforemost
{
    public partial class NetforemostBDToDoListContext : DbContext
    {
        public NetforemostBDToDoListContext()
        {
        }

        public NetforemostBDToDoListContext(DbContextOptions<NetforemostBDToDoListContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblPriority> TblPriorities { get; set; } = null!;
        public virtual DbSet<TblTask> TblTasks { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-GKTE05O\\SQLEXPRESS;Database=NetforemostBDToDoList;TrustServerCertificate=True;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblPriority>(entity =>
            {
                entity.HasKey(e => e.IdPriority)
                    .HasName("PK__TblPrior__20A1DC1A5DD97727");

                entity.ToTable("TblPriority", "Task");

                entity.Property(e => e.NamePriority).HasMaxLength(255);
            });

            modelBuilder.Entity<TblTask>(entity =>
            {
                entity.HasKey(e => e.IdTask)
                    .HasName("PK__TblTask__9FCAD1C55DF0B43E");

                entity.ToTable("TblTask", "Task");

                entity.Property(e => e.IdTask).HasMaxLength(50);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_At");

                entity.Property(e => e.DescriptionTask).HasMaxLength(255);

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiration_date");

                entity.Property(e => e.IdUser).HasMaxLength(50);

                entity.Property(e => e.Tags).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_At");

                entity.HasOne(d => d.IdPriorityNavigation)
                    .WithMany(p => p.TblTasks)
                    .HasForeignKey(d => d.IdPriority)
                    .HasConstraintName("FK__TblTask__IdPrior__4E88ABD4");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.TblTasks)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__TblTask__IdUser__4D94879B");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__TblUsers__B7C92638D8234930");

                entity.ToTable("TblUsers", "Users");

                entity.Property(e => e.IdUser).HasMaxLength(50);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_At");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Telephone).HasMaxLength(30);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_At");

                entity.Property(e => e.UserPassword).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
