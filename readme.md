# Proyecto 1 - Pasos Iniciales

## Crear proyecto

1. Crear solución en blanco

## Crear Capas

### Capa del Modelo de datos, Datos y Lógica de negocio

1. Clic derecho en la solución
2. Agregar, opción Nuevo Proyecto
3. Selecciona la opción de proyecto biblioteca de clases: 
![](./assets//bibClases.png)
4. Lo normal será nombrar de la siguiente manera: Proyecto.Models, Proyecto.DLL, Proyecto.BLL

Para la capa de presentación, se creará un proyecto MVC .NET Core

### Capa Datos - Configuraciones Iniciales
Esta capa nos permitirá la conexión a la BD y la utilización de recursos, para esto instalaremos y utilizaremos los siguientes paquetes:

1. Clic derecho en capa datos,
2. Administrar paquetes Nuget
3. Instalaremos las siguientes librerías:
    * **EntityFrameWorkCore:** biblioteca principal de Entity Framework Core. Proporciona funcionalidades básicas de mapeo objeto-relacional (ORM) para trabajar con bases de datos en aplicaciones .NET Core. Esta biblioteca incluye las clases y métodos necesarios para definir modelos de datos, realizar consultas, realizar operaciones de actualización y persistir datos en la base de datos.
    * **EntityFrameWorkCore.SqlServer:** es un proveedor específico de base de datos para SQL Server en Entity Framework Core. Proporciona compatibilidad específica para SQL Server, lo que permite a Entity Framework Core interactuar con bases de datos SQL Server de manera eficiente. Esta biblioteca incluye clases y métodos que permiten la generación de consultas SQL específicas de SQL Server, optimizaciones de rendimiento y funcionalidades específicas de este motor de base de datos.
    * **EntityFrameWorkCore.Tools:** biblioteca que proporciona herramientas de línea de comandos para Entity Framework Core. Estas herramientas son útiles para realizar tareas comunes durante el desarrollo de aplicaciones, como la creación de migraciones de base de datos, la aplicación de migraciones, la generación de clases de contexto de base de datos a partir de una base de datos existente, y más. EntityFrameworkCore.Tools simplifica el proceso de administración y mantenimiento de la base de datos en tu aplicación utilizando Entity Framework Core.

Una vez instalados los paquetes, agregaremos unos cuantos recursos más de forma manual:

1. Una carpeta para el contexto,
    * **Contexto:** representa una sesión de trabajo con la base de datos, donde se pueden realizar consultas, insertar, actualizar y eliminar registros. 
2. Establecer esta capa como proyecto de inicio
3. Enlazar nuestro modelo de datos a nuestra bd
    * `Scaffold-DbContext "Server=(local); DataBase=PazYSalvo; Integrated Security=true"  Microsoft.EntityFrameworkCore.SqlServer -OutputDir context`
    * Scaffold-DbContext "Data Source=DESKTOP-88MTFLQ\SQLEXPRESS;Initial Catalog=PazSalvo;Integrated Security=true;Encrypt=True;TrustServerCertificate=true;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Context.

Una vez establecida nuestra conexión, podremos visualizar el contexto asociado a nuestro modelo de datos:

```cs
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PazYSalvoAPP.Data.Context;

public partial class PazSalvoContext : DbContext
{
    public PazSalvoContext()
    {
    }

    public PazSalvoContext(DbContextOptions<PazSalvoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallesDeFactura> DetallesDeFacturas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<MediosDePago> MediosDePagos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-88MTFLQ\\SQLEXPRESS;Initial Catalog=PazSalvo;Integrated Security=true;Encrypt=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC07C4165C9B");

            entity.Property(e => e.FechaDeCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Persona).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.PersonaId)
                .HasConstraintName("FK__Clientes__Person__59FA5E80");

            entity.HasOne(d => d.Rol).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Clientes__RolId__5AEE82B9");
        });

        modelBuilder.Entity<DetallesDeFactura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalles__3214EC0792D3505B");

            entity.Property(e => e.FechaDeCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Factura).WithMany(p => p.DetallesDeFacturas)
                .HasForeignKey(d => d.FacturaId)
                .HasConstraintName("FK__DetallesD__Factu__73BA3083");

            entity.HasOne(d => d.Servicio).WithMany(p => p.DetallesDeFacturas)
                .HasForeignKey(d => d.ServicioId)
                .HasConstraintName("FK__DetallesD__Servi__74AE54BC");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estados__3214EC0728D092FD");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaDeCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facturas__3214EC07E69AE139");

            entity.Property(e => e.FechaDeCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Saldo)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__Facturas__Client__6D0D32F4");

            entity.HasOne(d => d.Estado).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK__Facturas__Estado__6FE99F9F");

            entity.HasOne(d => d.MedioDePago).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.MedioDePagoId)
                .HasConstraintName("FK__Facturas__MedioD__6EF57B66");

            entity.HasOne(d => d.ServicioAdquirido).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ServicioAdquiridoId)
                .HasConstraintName("FK__Facturas__Servic__6E01572D");
        });

        modelBuilder.Entity<MediosDePago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MediosDe__3214EC07C45655C3");

            entity.ToTable("MediosDePago");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.FechaDeCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pagos__3214EC07E6CE7979");

            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaDeCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MontoDePago)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Factura).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.FacturaId)
                .HasConstraintName("FK__Pagos__FacturaId__797309D9");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personas__3214EC07946D6505");

            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.DocumentoIdentificacion).HasMaxLength(20);
            entity.Property(e => e.FechaDeCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC076CAC20FF");

            entity.Property(e => e.Activo).HasDefaultValueSql("((0))");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaDeCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Servicio__3214EC07D6BED408");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.FechaDeCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07B61F1220");

            entity.Property(e => e.Contrasena).HasMaxLength(100);
            entity.Property(e => e.FechaDeCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);

            entity.HasOne(d => d.Persona).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.PersonaId)
                .HasConstraintName("FK__Usuarios__Person__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

```

## Referencia entre capas

La referencia entre capas es lo que nos permite dar abstracción a nuestra aplicación, consiste en hacer referencias entre los proyectos, estableciendo una comunicación directa. Dependerá de la misma lógica del proyecto determinar cómo se deben comunicar las capas, pero como estandar, manejaremos las referencias de la siguiente manera:

1. nuestra capa de datos, solo tendrá referencia a `models`. 
2. Nuestra capa de negocio hará referencia tanto a la capa `data`, como a `models`
3. Finalmente, nuestra capa de presentación hará referencia a nuestra capa `business` y `models`.

## Configurar la cadena de conexión

