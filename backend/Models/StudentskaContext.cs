using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public partial class StudentskaContext : DbContext
{
    public StudentskaContext()
    {
    }

    public StudentskaContext(DbContextOptions<StudentskaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ispit> Ispits { get; set; }

    public virtual DbSet<IspitniRok> IspitniRoks { get; set; }

    public virtual DbSet<Predmet> Predmets { get; set; }

    public virtual DbSet<Profesor> Profesors { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentPredmet> StudentPredmets { get; set; }

    public virtual DbSet<Zapisnik> Zapisniks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress;Initial Catalog=STUDENTSKA;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ispit>(entity =>
        {
            entity.HasKey(e => e.IdIspita).HasName("PK__ispit__29C6AD7FF093BCBD");

            entity.ToTable("ispit");

            entity.Property(e => e.IdIspita).HasColumnName("ID_ISPITA");
            entity.Property(e => e.Datum).HasColumnName("DATUM");
            entity.Property(e => e.IdPredmeta).HasColumnName("ID_PREDMETA");
            entity.Property(e => e.IdRoka).HasColumnName("ID_ROKA");
        });

        modelBuilder.Entity<IspitniRok>(entity =>
        {
            entity.HasKey(e => e.IdRoka).HasName("PK__ispitni___C7D0FE727CD3D2AF");

            entity.ToTable("ispitni_rok");

            entity.Property(e => e.IdRoka).HasColumnName("ID_ROKA");
            entity.Property(e => e.Naziv)
                .HasMaxLength(15)
                .HasColumnName("NAZIV");
            entity.Property(e => e.SkolskaGod)
                .HasMaxLength(7)
                .HasColumnName("SKOLSKA_GOD");
            entity.Property(e => e.StatusRoka)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("STATUS_ROKA");
        });

        modelBuilder.Entity<Predmet>(entity =>
        {
            entity.HasKey(e => e.IdPredmeta).HasName("PK__predmet__2C40E4E2F7744FD6");

            entity.ToTable("predmet");

            entity.Property(e => e.IdPredmeta)
                .ValueGeneratedNever()
                .HasColumnName("ID_PREDMETA");
            entity.Property(e => e.Espb).HasColumnName("ESPB");
            entity.Property(e => e.IdProfesora).HasColumnName("ID_PROFESORA");
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .HasColumnName("NAZIV");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.IdProfesora).HasName("PK__profesor__63597FD733DC3E32");

            entity.ToTable("profesor");

            entity.Property(e => e.IdProfesora)
                .ValueGeneratedNever()
                .HasColumnName("ID_PROFESORA");
            entity.Property(e => e.DatumZap).HasColumnName("DATUM_ZAP");
            entity.Property(e => e.Ime)
                .HasMaxLength(25)
                .HasColumnName("IME");
            entity.Property(e => e.Prezime)
                .HasMaxLength(50)
                .HasColumnName("PREZIME");
            entity.Property(e => e.Zvanje)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ZVANJE");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.IdStudenta).HasName("PK__student__0FD289789A7D4090");

            entity.ToTable("student");

            entity.Property(e => e.IdStudenta).HasColumnName("ID_STUDENTA");
            entity.Property(e => e.Broj).HasColumnName("BROJ");
            entity.Property(e => e.GodinaUpisa)
                .HasMaxLength(4)
                .HasColumnName("GODINA_UPISA");
            entity.Property(e => e.Ime)
                .HasMaxLength(25)
                .HasColumnName("IME");
            entity.Property(e => e.Prezime)
                .HasMaxLength(40)
                .HasColumnName("PREZIME");
            entity.Property(e => e.Smer)
                .HasMaxLength(5)
                .HasColumnName("SMER");
        });

        modelBuilder.Entity<StudentPredmet>(entity =>
        {
            entity.HasKey(e => new { e.IdStudenta, e.IdPredmeta, e.SkolskaGodina }).HasName("PK__student___BB07D41144E2CFFC");

            entity.ToTable("student_predmet");

            entity.Property(e => e.IdStudenta).HasColumnName("ID_STUDENTA");
            entity.Property(e => e.IdPredmeta).HasColumnName("ID_PREDMETA");
            entity.Property(e => e.SkolskaGodina)
                .HasMaxLength(7)
                .HasColumnName("SKOLSKA_GODINA");
        });

        modelBuilder.Entity<Zapisnik>(entity =>
        {
            entity.HasKey(e => new { e.IdStudenta, e.IdIspita }).HasName("PK__zapisnik__ED4EE3AF749D91B5");

            entity.ToTable("zapisnik");

            entity.Property(e => e.IdStudenta).HasColumnName("ID_STUDENTA");
            entity.Property(e => e.IdIspita).HasColumnName("ID_ISPITA");
            entity.Property(e => e.Bodovi)
                .HasMaxLength(3)
                .HasColumnName("BODOVI");
            entity.Property(e => e.Ocena).HasColumnName("OCENA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
