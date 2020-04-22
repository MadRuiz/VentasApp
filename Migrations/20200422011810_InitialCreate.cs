using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VentasApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id_Categoria = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id_Categoria);
                });

            migrationBuilder.CreateTable(
                name: "Tiendas",
                columns: table => new
                {
                    Id_Tienda = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Encargado = table.Column<string>(maxLength: 50, nullable: false),
                    Contacto = table.Column<int>(nullable: false),
                    Direccion = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiendas", x => x.Id_Tienda);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id_Producto = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Precio = table.Column<double>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Fecha_Vencimiento = table.Column<DateTime>(nullable: false),
                    Id_Categoria = table.Column<int>(nullable: false),
                    CategoriaId_Categoria = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id_Producto);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId_Categoria",
                        column: x => x.CategoriaId_Categoria,
                        principalTable: "Categorias",
                        principalColumn: "Id_Categoria",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id_Venta = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Codigo_Factura = table.Column<string>(maxLength: 50, nullable: false),
                    Nombre_Cliente = table.Column<string>(maxLength: 50, nullable: false),
                    Fecha = table.Column<string>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    Id_Tienda = table.Column<int>(nullable: false),
                    TiendaId_Tienda = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id_Venta);
                    table.ForeignKey(
                        name: "FK_Ventas_Tiendas_TiendaId_Tienda",
                        column: x => x.TiendaId_Tienda,
                        principalTable: "Tiendas",
                        principalColumn: "Id_Tienda",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    Id_DetalleVenta = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Subtotal = table.Column<double>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Id_Venta = table.Column<int>(nullable: false),
                    VentaId_Venta = table.Column<int>(nullable: true),
                    Id_Producto = table.Column<int>(nullable: false),
                    ProductoId_Producto = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentas", x => x.Id_DetalleVenta);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Productos_ProductoId_Producto",
                        column: x => x.ProductoId_Producto,
                        principalTable: "Productos",
                        principalColumn: "Id_Producto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Ventas_VentaId_Venta",
                        column: x => x.VentaId_Venta,
                        principalTable: "Ventas",
                        principalColumn: "Id_Venta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_ProductoId_Producto",
                table: "DetalleVentas",
                column: "ProductoId_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_VentaId_Venta",
                table: "DetalleVentas",
                column: "VentaId_Venta");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId_Categoria",
                table: "Productos",
                column: "CategoriaId_Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_TiendaId_Tienda",
                table: "Ventas",
                column: "TiendaId_Tienda");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Tiendas");
        }
    }
}
