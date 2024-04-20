using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace L02P02_2021_CC_650_2021_YD_650.Models;

public partial class LibreriaDbContext : DbContext
{
    public LibreriaDbContext()
    {
    }

    public LibreriaDbContext(DbContextOptions<LibreriaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ComentariosLibro> ComentariosLibros { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<PedidoDetalle> PedidoDetalles { get; set; }

    public virtual DbSet<PedidoEncabezado> PedidoEncabezados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-FKE33D2; Database=libreriaDb; Trusted_Connection=TRUE; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__autores__3213E83FBD3A6069");

            entity.ToTable("autores");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Autor)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("autor");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F27AAF95B");

            entity.ToTable("categorias");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Categoria1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("categoria");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__clientes__3213E83F87123078");

            entity.ToTable("clientes");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .HasColumnName("apellido");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<ComentariosLibro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comentar__3213E83F864266A9");

            entity.ToTable("comentarios_libros");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Comentarios)
                .IsUnicode(false)
                .HasColumnName("comentarios");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IdLibro).HasColumnName("id_libro");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.ComentariosLibros)
                .HasForeignKey(d => d.IdLibro)
                .HasConstraintName("FK__comentari__id_li__46E78A0C");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__libros__3213E83F3F9E8FE0");

            entity.ToTable("libros");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.IdAutor).HasColumnName("id_autor");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.UrlImagen)
                .HasMaxLength(255)
                .HasColumnName("url_imagen");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdAutor)
                .HasConstraintName("FK__libros__id_autor__45F365D3");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__libros__id_categ__47DBAE45");
        });

        modelBuilder.Entity<PedidoDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pedido_d__3213E83FB790D23F");

            entity.ToTable("pedido_detalle");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IdLibro).HasColumnName("id_libro");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => d.IdLibro)
                .HasConstraintName("FK__pedido_de__id_li__44FF419A");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__pedido_de__id_pe__440B1D61");
        });

        modelBuilder.Entity<PedidoEncabezado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pedido_e__3213E83F39E551D9");

            entity.ToTable("pedido_encabezado");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CantidadLibros).HasColumnName("cantidad_libros");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.PedidoEncabezados)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__pedido_en__id_cl__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
