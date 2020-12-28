using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AforoMexAPI.Models
{
    public partial class AforoMexContext : DbContext
    {
        public AforoMexContext()
        {
        }

        public AforoMexContext(DbContextOptions<AforoMexContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Direccione> Direcciones { get; set; }
        public virtual DbSet<Horario> Horarios { get; set; }
        public virtual DbSet<Negocio> Negocios { get; set; }
        public virtual DbSet<Reservacione> Reservaciones { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Direccione>(entity =>
            {
                entity.HasKey(e => e.IdDireccion);

                entity.Property(e => e.Calle)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Ciudad)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Colonia)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Pais)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdNegocioNavigation)
                    .WithMany(p => p.Direcciones)
                    .HasForeignKey(d => d.IdNegocio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Direcciones_Negocios");
            });

            modelBuilder.Entity<Horario>(entity =>
            {
                entity.HasKey(e => e.IdHorario);

                entity.Property(e => e.Dia)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.HoraFin).HasColumnType("time(1)");

                entity.Property(e => e.HoraInicio).HasColumnType("time(1)");

                entity.HasOne(d => d.IdNegocioNavigation)
                    .WithMany(p => p.Horarios)
                    .HasForeignKey(d => d.IdNegocio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Horarios_Negocios");
            });

            modelBuilder.Entity<Negocio>(entity =>
            {
                entity.HasKey(e => e.IdNegocio);

                entity.Property(e => e.AforoReservaciones).HasColumnType("decimal(3, 2)");

                entity.Property(e => e.Categoria)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Celular).HasMaxLength(15);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Facebook).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SitioWeb).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasMaxLength(15);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Negocios)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Negocios_Usuarios");
            });

            modelBuilder.Entity<Reservacione>(entity =>
            {
                entity.HasKey(e => e.IdReservacion);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FechaReserva).HasColumnType("datetime");

                entity.HasOne(d => d.IdNegocioNavigation)
                    .WithMany(p => p.Reservaciones)
                    .HasForeignKey(d => d.IdNegocio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservaciones_Negocios");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Reservaciones)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservaciones_Usuarios");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
