using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProgressPacer.Models;

public partial class Prog6212PoeSt10033475Context : DbContext
{
    public Prog6212PoeSt10033475Context()
    {
    }

    public Prog6212PoeSt10033475Context(DbContextOptions<Prog6212PoeSt10033475Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Study> Studies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NAIYAH;Initial Catalog=PROG6212_POE_ST10033475; Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.MId).HasName("PK__Module__DF513ED401DBE6B2");

            entity.ToTable("Module");

            entity.Property(e => e.MId).HasColumnName("mId");
            entity.Property(e => e.ClassHours)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("classHours");
            entity.Property(e => e.ModuleCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("moduleCode");
            entity.Property(e => e.ModuleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("moduleName");
            entity.Property(e => e.NumCredits).HasColumnName("numCredits");
            entity.Property(e => e.NumWeeks)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("numWeeks");
            entity.Property(e => e.SelfStudy)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("selfStudy");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Study>(entity =>
        {
            entity.HasKey(e => e.StudyId).HasName("PK__Study__E528154E1A1710F9");

            entity.ToTable("Study");

            entity.Property(e => e.StudyId).HasColumnName("studyID");
            entity.Property(e => e.MId).HasColumnName("mId");
            entity.Property(e => e.ModuleCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("moduleCode");
            entity.Property(e => e.ModuleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("moduleName");
            entity.Property(e => e.RemainingHrs)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("remainingHrs");
            entity.Property(e => e.SelfStudyHours).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StudyDate).HasColumnName("studyDate");
            entity.Property(e => e.TotalSelfStudy).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.MIdNavigation).WithMany(p => p.Studies)
                .HasForeignKey(d => d.MId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Study__mId__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
