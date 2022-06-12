using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PisoAppBackend.Models
{
    public partial class PisoAppContext : DbContext
    {
        public PisoAppContext()
        {
        }

        public PisoAppContext(DbContextOptions<PisoAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AsignadosTarea> AsignadosTareas { get; set; }
        public virtual DbSet<IntegrantesPiso> IntegrantesPisos { get; set; }
        public virtual DbSet<Piso> Pisos { get; set; }
        public virtual DbSet<Tarea> Tareas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(Startup.ConnectionString);
                //optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AsignadosTarea>(entity =>
            {
                entity.ToTable("Asignados_Tareas");

                entity.HasIndex(e => e.TaskId, "FK_Asignados_Tareas_Tarea");

                entity.HasIndex(e => e.UserId, "FK_Asignados_Tareas_Usuario");

                entity.HasIndex(e => e.AssignedBy, "FK_Asignados_Tareas_Usuario_2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AssignedBy).HasColumnName("assigned_by");

                entity.Property(e => e.AssignedOn).HasColumnName("assigned_on");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.AssignedByNavigation)
                    .WithMany(p => p.AsignadosTareaAssignedByNavigations)
                    .HasForeignKey(d => d.AssignedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asignados_Tareas_Usuario_2");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.AsignadosTareas)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asignados_Tareas_Tarea");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AsignadosTareaUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asignados_Tareas_Usuario");
            });

            modelBuilder.Entity<IntegrantesPiso>(entity =>
            {
                entity.ToTable("Integrantes_Pisos");

                entity.HasIndex(e => e.PisoId, "FK__Pisos");

                entity.HasIndex(e => e.UserId, "FK__Usuario");

                entity.HasIndex(e => e.AssignerId, "FK__Usuario_2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AssignerId).HasColumnName("assigner_id");

                entity.Property(e => e.JoinDate).HasColumnName("join_date");

                entity.Property(e => e.PisoId).HasColumnName("piso_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Assigner)
                    .WithMany(p => p.IntegrantesPisoAssigners)
                    .HasForeignKey(d => d.AssignerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario_2");

                entity.HasOne(d => d.Piso)
                    .WithMany(p => p.IntegrantesPisos)
                    .HasForeignKey(d => d.PisoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pisos");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IntegrantesPisoUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario");
            });

            modelBuilder.Entity<Piso>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("name")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.ToTable("Tarea");

                entity.HasIndex(e => e.CreatedBy, "FK_Tarea_Usuario");

                entity.HasIndex(e => e.FinishedBy, "FK_Tarea_Usuario_2");

                entity.HasIndex(e => e.CancelledBy, "FK_Tarea_Usuario_3");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CancelledBy).HasColumnName("cancelled_by");

                entity.Property(e => e.CancelledOn).HasColumnName("cancelled_on");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn).HasColumnName("created_on");

                entity.Property(e => e.DueTo).HasColumnName("due_to");

                entity.Property(e => e.FinishedBy).HasColumnName("finished_by");

                entity.Property(e => e.FinishedOn).HasColumnName("finished_on");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("name")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.NotifyAssignees)
                    .IsRequired()
                    .HasColumnType("bit(1)")
                    .HasColumnName("notify_assignees")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint")
                    .HasColumnName("status");

                entity.HasOne(d => d.CancelledByNavigation)
                    .WithMany(p => p.TareaCancelledByNavigations)
                    .HasForeignKey(d => d.CancelledBy)
                    .HasConstraintName("FK_Tarea_Usuario_3");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TareaCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tarea_Usuario");

                entity.HasOne(d => d.FinishedByNavigation)
                    .WithMany(p => p.TareaFinishedByNavigations)
                    .HasForeignKey(d => d.FinishedBy)
                    .HasConstraintName("FK_Tarea_Usuario_2");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.HasIndex(e => e.Username, "username")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HashedPassword)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("hashedPassword")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("name")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Stars).HasColumnName("stars");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("username")
                    .HasDefaultValueSql("'0'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
