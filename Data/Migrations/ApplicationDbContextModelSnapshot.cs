﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using proyectoTienda.Data;

#nullable disable

namespace proyectoTienda.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.13");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("proyectoTienda.Models.Categoria", b =>
                {
                    b.Property<int>("IDCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .HasColumnType("TEXT");

                    b.HasKey("IDCategoria");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("proyectoTienda.Models.DetallePedido", b =>
                {
                    b.Property<int>("IDPedido")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IDProducto")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("IDPedido", "IDProducto");

                    b.HasIndex("IDProducto");

                    b.ToTable("DetallesPedidos");
                });

            modelBuilder.Entity("proyectoTienda.Models.Model.ItemCarrito", b =>
                {
                    b.Property<int>("IDItem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Precio")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProductoIDProducto")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("IDItem");

                    b.HasIndex("ProductoIDProducto");

                    b.ToTable("ItemsCarrito");
                });

            modelBuilder.Entity("proyectoTienda.Models.Pago", b =>
                {
                    b.Property<int>("IDPago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("TEXT");

                    b.Property<int>("IDPedido")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MetodoPago")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("IDPago");

                    b.HasIndex("IDPedido")
                        .IsUnique();

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("proyectoTienda.Models.Pedido", b =>
                {
                    b.Property<int>("IDPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Estado")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaPedido")
                        .HasColumnType("TEXT");

                    b.Property<string>("IDCliente")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("IDPedido");

                    b.HasIndex("IDCliente");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("proyectoTienda.Models.Producto", b =>
                {
                    b.Property<int>("IDProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IDCategoria")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImagenURL")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PrecioActual")
                        .HasColumnType("decimal(10, 1)");

                    b.Property<decimal>("PrecioOriginal")
                        .HasColumnType("decimal(10, 1)");

                    b.Property<int>("Stock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Talla")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("IDProducto");

                    b.HasIndex("IDCategoria");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("proyectoTienda.Models.Usuario", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("TipoUsuario")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("proyectoTienda.Models.DetallePedido", b =>
                {
                    b.HasOne("proyectoTienda.Models.Pedido", "Pedido")
                        .WithMany("DetallesPedidos")
                        .HasForeignKey("IDPedido")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("proyectoTienda.Models.Producto", "Producto")
                        .WithMany("DetallesPedidos")
                        .HasForeignKey("IDProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("proyectoTienda.Models.Model.ItemCarrito", b =>
                {
                    b.HasOne("proyectoTienda.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoIDProducto");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("proyectoTienda.Models.Pago", b =>
                {
                    b.HasOne("proyectoTienda.Models.Pedido", "Pedido")
                        .WithOne("Pago")
                        .HasForeignKey("proyectoTienda.Models.Pago", "IDPedido")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("proyectoTienda.Models.Pedido", b =>
                {
                    b.HasOne("proyectoTienda.Models.Usuario", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("IDCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("proyectoTienda.Models.Producto", b =>
                {
                    b.HasOne("proyectoTienda.Models.Categoria", "Categoria")
                        .WithMany("Productos")
                        .HasForeignKey("IDCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("proyectoTienda.Models.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("proyectoTienda.Models.Pedido", b =>
                {
                    b.Navigation("DetallesPedidos");

                    b.Navigation("Pago");
                });

            modelBuilder.Entity("proyectoTienda.Models.Producto", b =>
                {
                    b.Navigation("DetallesPedidos");
                });

            modelBuilder.Entity("proyectoTienda.Models.Usuario", b =>
                {
                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
