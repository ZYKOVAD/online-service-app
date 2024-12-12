using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace online_service_app_business_functions.db_layer;

public partial class OnlineServiceDbContext : DbContext
{
    public OnlineServiceDbContext()
    {
    }

    public OnlineServiceDbContext(DbContextOptions<OnlineServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingStatus> BookingStatuses { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Master> Masters { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<SphereOfOrganization> SphereOfOrganizations { get; set; }

    public virtual DbSet<TypeOfOrganization> TypeOfOrganizations { get; set; }

    public virtual DbSet<Workday> Workdays { get; set; }

    public virtual DbSet<WorkdayByDefault> WorkdayByDefaults { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=172.17.0.1;Port=5438;Database=online_service_db;Username=admin;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("booking_pkey");

            entity.ToTable("booking");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.DateTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_time");
            entity.Property(e => e.MasterId).HasColumnName("master_id");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Client).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_id_fk");

            entity.HasOne(d => d.Master).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("master_id_fk");

            entity.HasOne(d => d.Organization).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("org_id_fk");

            entity.HasOne(d => d.Service).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("service_id_fk");

            entity.HasOne(d => d.Status).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("status_id_fk");
        });

        modelBuilder.Entity<BookingStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("booking_status_pkey");

            entity.ToTable("booking_status");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Patronymic).HasColumnName("patronymic");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Surname).HasColumnName("surname");
        });

        modelBuilder.Entity<Master>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("master_pkey");

            entity.ToTable("master");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Patronymic).HasColumnName("patronymic");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.SpecializationId).HasColumnName("specialization_id");
            entity.Property(e => e.Surname).HasColumnName("surname");

            entity.HasOne(d => d.Organization).WithMany(p => p.Masters)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("org_id_fk");

            entity.HasOne(d => d.Specialization).WithMany(p => p.Masters)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("spec_id_fk");

            entity.HasMany(d => d.Services).WithMany(p => p.Masters)
                .UsingEntity<Dictionary<string, object>>(
                    "ServiceMaster",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("service_id_fk"),
                    l => l.HasOne<Master>().WithMany()
                        .HasForeignKey("MasterId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("master_id_fk"),
                    j =>
                    {
                        j.HasKey("MasterId", "ServiceId").HasName("master_service_pk");
                        j.ToTable("service_master");
                        j.IndexerProperty<int>("MasterId").HasColumnName("master_id");
                        j.IndexerProperty<int>("ServiceId").HasColumnName("service_id");
                    });
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organization_pkey");

            entity.ToTable("organization");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.SphereId).HasColumnName("sphere_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.WebAddress).HasColumnName("web_address");

            entity.HasOne(d => d.Sphere).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.SphereId)
                .HasConstraintName("sphere_id_fk");

            entity.HasOne(d => d.Type).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("type_id_fk");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_pkey");

            entity.ToTable("service");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.Organization).WithMany(p => p.Services)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("org_id_fk");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("specialization_pkey");

            entity.ToTable("specialization");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<SphereOfOrganization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sphere_of_organization_pkey");

            entity.ToTable("sphere_of_organization");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<TypeOfOrganization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_of_organization_pkey");

            entity.ToTable("type_of_organization");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Workday>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workday_pkey");

            entity.ToTable("workday");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BreakEnd).HasColumnName("break_end");
            entity.Property(e => e.BreakStart).HasColumnName("break_start");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.MasterId).HasColumnName("master_id");
            entity.Property(e => e.TimeEnd).HasColumnName("time_end");
            entity.Property(e => e.TimeStart).HasColumnName("time_start");

            entity.HasOne(d => d.Master).WithMany(p => p.Workdays)
                .HasForeignKey(d => d.MasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("master_id_fk");
        });

        modelBuilder.Entity<WorkdayByDefault>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workday_by_default_pkey");

            entity.ToTable("workday_by_default");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BreakEnd).HasColumnName("break_end");
            entity.Property(e => e.BreakStart).HasColumnName("break_start");
            entity.Property(e => e.MasterId).HasColumnName("master_id");
            entity.Property(e => e.TimeEnd).HasColumnName("time_end");
            entity.Property(e => e.TimeStart).HasColumnName("time_start");

            entity.HasOne(d => d.Master).WithMany(p => p.WorkdayByDefaults)
                .HasForeignKey(d => d.MasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("master_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
