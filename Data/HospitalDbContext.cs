using System;
using System.Collections.Generic;
using Internship.Models;
using Microsoft.EntityFrameworkCore;

namespace Internship.Data;

public partial class HospitalDbContext : DbContext
{
    public HospitalDbContext()
    {
    }

    public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Clinic> Clinics { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<LuAppointmentTime> LuAppointmentTimes { get; set; }

    public virtual DbSet<LuEthnicity> LuEthnicities { get; set; }

    public virtual DbSet<LuNationality> LuNationalities { get; set; }

    public virtual DbSet<LuProfession> LuProfessions { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=DESKTOP-RRLV08N\\SQLEXPRESS;initial catalog=HospitalDB;integrated security=true;pooling=false;encrypt=false;trust server certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(e => e.AppointmentId).ValueGeneratedNever();

            entity.HasOne(d => d.AppointmentTimeNavigation).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_appointments_lu_appointment_times");

            entity.HasOne(d => d.Clinic).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_appointments_clinics");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_appointments_patients");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.CityId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.Property(e => e.ClinicId).ValueGeneratedNever();
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.Property(e => e.DistrictId).ValueGeneratedNever();
        });

        modelBuilder.Entity<LuAppointmentTime>(entity =>
        {
            entity.Property(e => e.AppointmentTime).ValueGeneratedNever();
        });

        modelBuilder.Entity<LuEthnicity>(entity =>
        {
            entity.Property(e => e.EthnicityId).ValueGeneratedNever();
        });

        modelBuilder.Entity<LuNationality>(entity =>
        {
            entity.Property(e => e.NationalityId).ValueGeneratedNever();
        });

        modelBuilder.Entity<LuProfession>(entity =>
        {
            entity.Property(e => e.ProfessionId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.PatientId).ValueGeneratedNever();
            entity.Property(e => e.PhoneNumber).IsFixedLength();

            entity.HasOne(d => d.City).WithMany(p => p.Patients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_patients_cities");

            entity.HasOne(d => d.District).WithMany(p => p.Patients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_patients_districts");

            entity.HasOne(d => d.Ethnicity).WithMany(p => p.Patients).HasConstraintName("FK_patients_lu_ethnicities");

            entity.HasOne(d => d.Nationality).WithMany(p => p.Patients).HasConstraintName("FK_patients_lu_nationalities");

            entity.HasOne(d => d.Profession).WithMany(p => p.Patients).HasConstraintName("FK_patients_lu_professions");

            entity.HasOne(d => d.Ward).WithMany(p => p.Patients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_patients_wards");
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.Property(e => e.WardId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
