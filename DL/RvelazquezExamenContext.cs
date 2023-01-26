using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class RvelazquezExamenContext : DbContext
{
    public RvelazquezExamenContext()
    {
    }

    public RvelazquezExamenContext(DbContextOptions<RvelazquezExamenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aula> Aulas { get; set; }

    public virtual DbSet<CiclistaClase> CiclistaClases { get; set; }

    public virtual DbSet<Ciclistum> Ciclista { get; set; }

    public virtual DbSet<Clase> Clases { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Nivel> Nivels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= RVelazquezExamen; Trusted_Connection=True; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aula>(entity =>
        {
            entity.HasKey(e => e.IdAula).HasName("PK__Aula__4F036E2D12DB040C");

            entity.ToTable("Aula");

            entity.Property(e => e.IdAula).ValueGeneratedNever();
            entity.Property(e => e.NombreAula)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CiclistaClase>(entity =>
        {
            entity.HasKey(e => e.IdRelacion).HasName("PK__Ciclista__D27D6AE75648DE43");

            entity.ToTable("CiclistaClase");

            entity.HasOne(d => d.IdCiclistaNavigation).WithMany(p => p.CiclistaClases)
                .HasForeignKey(d => d.IdCiclista)
                .HasConstraintName("FK__CiclistaC__IdCic__398D8EEE");

            entity.HasOne(d => d.IdClaseNavigation).WithMany(p => p.CiclistaClases)
                .HasForeignKey(d => d.IdClase)
                .HasConstraintName("FK__CiclistaC__IdCla__3A81B327");
        });

        modelBuilder.Entity<Ciclistum>(entity =>
        {
            entity.HasKey(e => e.IdCiclista).HasName("PK__Ciclista__43084A519D6583BA");

            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreCiclista)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNivelNavigation).WithMany(p => p.Ciclista)
                .HasForeignKey(d => d.IdNivel)
                .HasConstraintName("FK__Ciclista__IdNive__36B12243");
        });

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.HasKey(e => e.IdClase).HasName("PK__Clase__003FCC6F0A339DC3");

            entity.ToTable("Clase");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAulaNavigation).WithMany(p => p.Clases)
                .HasForeignKey(d => d.IdAula)
                .HasConstraintName("FK__Clase__IdAula__29572725");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany(p => p.Clases)
                .HasForeignKey(d => d.IdHorario)
                .HasConstraintName("FK__Clase__IdHorario__286302EC");

            entity.HasOne(d => d.IdNivelNavigation).WithMany(p => p.Clases)
                .HasForeignKey(d => d.IdNivel)
                .HasConstraintName("FK__Clase__IdNivel__276EDEB3");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK__Horario__1539229BDAE00AA9");

            entity.ToTable("Horario");

            entity.Property(e => e.IdHorario).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Nivel>(entity =>
        {
            entity.HasKey(e => e.IdNivel).HasName("PK__Nivel__A7F93DEC960A8DA2");

            entity.ToTable("Nivel");

            entity.Property(e => e.IdNivel).ValueGeneratedNever();
            entity.Property(e => e.NombreNivel)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
