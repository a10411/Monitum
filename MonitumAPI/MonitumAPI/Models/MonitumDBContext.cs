using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MonitumAPI.Models
{
    public partial class MonitumDBContext : DbContext
    {
        public MonitumDBContext()
        {
        }

        public MonitumDBContext(DbContextOptions<MonitumDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estabelecimento> Estabelecimentos { get; set; }
        public virtual DbSet<EstabelecimentoGestor> EstabelecimentoGestors { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Gestor> Gestors { get; set; }
        public virtual DbSet<HorarioSala> HorarioSalas { get; set; }
        public virtual DbSet<LogsMetrica> LogsMetricas { get; set; }
        public virtual DbSet<Metrica> Metricas { get; set; }
        public virtual DbSet<Sala> Salas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MonitumDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estabelecimento>(entity =>
            {
                entity.HasKey(e => e.IdEstabelecimento)
                    .HasName("PK__Estabele__A6291D9913D4C402");

                entity.ToTable("Estabelecimento");

                entity.Property(e => e.IdEstabelecimento).HasColumnName("id_estabelecimento");

                entity.Property(e => e.Morada)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("morada");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<EstabelecimentoGestor>(entity =>
            {
                entity.HasKey(e => new { e.IdEstabelecimento, e.IdGestor })
                    .HasName("PK__Estabele__24C96FFA7BA06C73");

                entity.ToTable("Estabelecimento_Gestor");

                entity.Property(e => e.IdEstabelecimento).HasColumnName("id_estabelecimento");

                entity.Property(e => e.IdGestor).HasColumnName("id_gestor");

                entity.HasOne(d => d.IdEstabelecimentoNavigation)
                    .WithMany(p => p.EstabelecimentoGestors)
                    .HasForeignKey(d => d.IdEstabelecimento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKEstabeleci532222");

                entity.HasOne(d => d.IdGestorNavigation)
                    .WithMany(p => p.EstabelecimentoGestors)
                    .HasForeignKey(d => d.IdGestor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKEstabeleci479765");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__Estados__86989FB241BC5437");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.Estado1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("estado");
            });

            modelBuilder.Entity<Gestor>(entity =>
            {
                entity.HasKey(e => e.IdGestor)
                    .HasName("PK__Gestor__2E0726395BF919B7");

                entity.ToTable("Gestor");

                entity.Property(e => e.IdGestor).HasColumnName("id_gestor");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<HorarioSala>(entity =>
            {
                entity.HasKey(e => e.IdHorario)
                    .HasName("PK__Horario___C5836D696269768F");

                entity.ToTable("Horario_Sala");

                entity.Property(e => e.IdHorario).HasColumnName("id_horario");

                entity.Property(e => e.DiaSemana)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("dia_semana");

                entity.Property(e => e.HoraEntrada).HasColumnName("hora_entrada");

                entity.Property(e => e.HoraSaida).HasColumnName("hora_saida");

                entity.Property(e => e.IdSala).HasColumnName("id_sala");

                entity.HasOne(d => d.IdSalaNavigation)
                    .WithMany(p => p.HorarioSalas)
                    .HasForeignKey(d => d.IdSala)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKHorario_Sa576410");
            });

            modelBuilder.Entity<LogsMetrica>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                    .HasName("PK__Logs_Met__6CC851FEA8675572");

                entity.ToTable("Logs_Metricas");

                entity.Property(e => e.IdLog).HasColumnName("id_log");

                entity.Property(e => e.DataHora).HasColumnName("data_hora");

                entity.Property(e => e.IdMetrica).HasColumnName("id_metrica");

                entity.Property(e => e.IdSala).HasColumnName("id_sala");

                entity.Property(e => e.ValorMetrica).HasColumnName("valor_metrica");

                entity.HasOne(d => d.IdMetricaNavigation)
                    .WithMany(p => p.LogsMetricas)
                    .HasForeignKey(d => d.IdMetrica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKLogs_Metri706591");

                entity.HasOne(d => d.IdSalaNavigation)
                    .WithMany(p => p.LogsMetricas)
                    .HasForeignKey(d => d.IdSala)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKLogs_Metri10861");
            });

            modelBuilder.Entity<Metrica>(entity =>
            {
                entity.HasKey(e => e.IdMetrica)
                    .HasName("PK__Metricas__A9B3207C6210D528");

                entity.Property(e => e.IdMetrica).HasColumnName("id_metrica");

                entity.Property(e => e.Medida)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("medida");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.IdSala)
                    .HasName("PK__Sala__D18B015B595CD207");

                entity.ToTable("Sala");

                entity.Property(e => e.IdSala).HasColumnName("id_sala");

                entity.Property(e => e.IdEstabelecimento).HasColumnName("id_estabelecimento");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.HasOne(d => d.IdEstabelecimentoNavigation)
                    .WithMany(p => p.Salas)
                    .HasForeignKey(d => d.IdEstabelecimento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSala746893");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Salas)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSala297249");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
