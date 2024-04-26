using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_ScanGo.Migrations
{
    /// <inheritdoc />
    public partial class compraIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrito",
                columns: table => new
                {
                    id_carrito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    total_pagar = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    total_articulos = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Carrito__83A2AD9CBE547550", x => x.id_carrito);
                });

            migrationBuilder.CreateTable(
                name: "Descuento",
                columns: table => new
                {
                    id_descuento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    porcentaje = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Descuent__4F9A1A80336A33DF", x => x.id_descuento);
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    id_inventario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventar__013AEB517298FE6E", x => x.id_inventario);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Producto",
                columns: table => new
                {
                    id_tipo_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    precio_por_kilo = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tipo_Pro__F5E0BFB83B092371", x => x.id_tipo_producto);
                });

            migrationBuilder.CreateTable(
                name: "Establecimiento",
                columns: table => new
                {
                    id_establecimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    imagen = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    id_inventario = table.Column<int>(type: "int", nullable: true),
                    direccion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    longitud = table.Column<decimal>(type: "decimal(10,6)", nullable: true),
                    latitud = table.Column<decimal>(type: "decimal(10,6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Establec__AFEAEA2088B7779A", x => x.id_establecimiento);
                    table.ForeignKey(
                        name: "FK__Estableci__id_in__44FF419A",
                        column: x => x.id_inventario,
                        principalTable: "Inventario",
                        principalColumn: "id_inventario");
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    id_producto = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    imagen = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    id_tipo_producto = table.Column<int>(type: "int", nullable: true),
                    id_descuento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__FF341C0D99FE9EB9", x => x.id_producto);
                    table.ForeignKey(
                        name: "FK__Producto__id_des__3C69FB99",
                        column: x => x.id_descuento,
                        principalTable: "Descuento",
                        principalColumn: "id_descuento");
                    table.ForeignKey(
                        name: "FK__Producto__id_tip__3B75D760",
                        column: x => x.id_tipo_producto,
                        principalTable: "Tipo_Producto",
                        principalColumn: "id_tipo_producto");
                });

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    id_administrador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    correo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    contraseña = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    id_establecimiento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Administ__0FE822AA0ABFEC6D", x => x.id_administrador);
                    table.ForeignKey(
                        name: "FK__Administr__id_es__59063A47",
                        column: x => x.id_establecimiento,
                        principalTable: "Establecimiento",
                        principalColumn: "id_establecimiento");
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    numero_telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    correo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    contrasena = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    id_carrito = table.Column<int>(type: "int", nullable: true),
                    id_establecimiento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cliente__E2891F4519CDE59D", x => x.numero_telefono);
                    table.ForeignKey(
                        name: "FK__Cliente__id_carr__4D94879B",
                        column: x => x.id_carrito,
                        principalTable: "Carrito",
                        principalColumn: "id_carrito");
                    table.ForeignKey(
                        name: "FK__Cliente__id_esta__4E88ABD4",
                        column: x => x.id_establecimiento,
                        principalTable: "Establecimiento",
                        principalColumn: "id_establecimiento");
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    id_venta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha_venta = table.Column<DateTime>(type: "datetime", nullable: true),
                    total_pagado = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    id_establecimiento = table.Column<int>(type: "int", nullable: true),
                    idTransaccion = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Venta__459533BF3B9DC6DA", x => x.id_venta);
                    table.ForeignKey(
                        name: "FK__Venta__id_establ__5BE2A6F2",
                        column: x => x.id_establecimiento,
                        principalTable: "Establecimiento",
                        principalColumn: "id_establecimiento");
                });

            migrationBuilder.CreateTable(
                name: "ProductoInventario",
                columns: table => new
                {
                    id_producto_inventario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_inventario = table.Column<int>(type: "int", nullable: true),
                    id_producto = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__7362DAA55F93E78C", x => x.id_producto_inventario);
                    table.ForeignKey(
                        name: "FK__ProductoI__id_in__4222D4EF",
                        column: x => x.id_inventario,
                        principalTable: "Inventario",
                        principalColumn: "id_inventario");
                    table.ForeignKey(
                        name: "FK__ProductoI__id_pr__412EB0B6",
                        column: x => x.id_producto,
                        principalTable: "Producto",
                        principalColumn: "id_producto");
                });

            migrationBuilder.CreateTable(
                name: "ProductosEnCarrito",
                columns: table => new
                {
                    id_productoEncarrito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_producto = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    id_carrito = table.Column<int>(type: "int", nullable: true),
                    nombre_producto = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__65992ACE0AA2451B", x => x.id_productoEncarrito);
                    table.ForeignKey(
                        name: "FK__Productos__id_ca__4AB81AF0",
                        column: x => x.id_carrito,
                        principalTable: "Carrito",
                        principalColumn: "id_carrito");
                    table.ForeignKey(
                        name: "FK__Productos__id_pr__49C3F6B7",
                        column: x => x.id_producto,
                        principalTable: "Producto",
                        principalColumn: "id_producto");
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    id_compra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero_telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    total_pagado = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    total_productos = table.Column<int>(type: "int", nullable: true),
                    fecha_compra = table.Column<DateTime>(type: "date", nullable: true),
                    id_establecimiento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Compra__C4BAA6040B015E15", x => x.id_compra);
                    table.ForeignKey(
                        name: "FK__Compra__id_estab__52593CB8",
                        column: x => x.id_establecimiento,
                        principalTable: "Establecimiento",
                        principalColumn: "id_establecimiento");
                    table.ForeignKey(
                        name: "FK__Compra__numero_t__5165187F",
                        column: x => x.numero_telefono,
                        principalTable: "Cliente",
                        principalColumn: "numero_telefono");
                });

            migrationBuilder.CreateTable(
                name: "DetalleVenta",
                columns: table => new
                {
                    id_detalle_venta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_venta = table.Column<int>(type: "int", nullable: true),
                    id_producto = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    precio_unitario = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleV__5B265D471211943C", x => x.id_detalle_venta);
                    table.ForeignKey(
                        name: "FK__DetalleVe__id_pr__5FB337D6",
                        column: x => x.id_producto,
                        principalTable: "Producto",
                        principalColumn: "id_producto");
                    table.ForeignKey(
                        name: "FK__DetalleVe__id_ve__5EBF139D",
                        column: x => x.id_venta,
                        principalTable: "Venta",
                        principalColumn: "id_venta");
                });

            migrationBuilder.CreateTable(
                name: "CompraDetalles",
                columns: table => new
                {
                    id_detalle_compra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_compra = table.Column<int>(type: "int", nullable: true),
                    id_producto = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    nombre_producto = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CompraDe__BD16E279EE9D9862", x => x.id_detalle_compra);
                    table.ForeignKey(
                        name: "FK__CompraDet__id_co__5535A963",
                        column: x => x.id_compra,
                        principalTable: "Compra",
                        principalColumn: "id_compra");
                    table.ForeignKey(
                        name: "FK__CompraDet__id_pr__5629CD9C",
                        column: x => x.id_producto,
                        principalTable: "Producto",
                        principalColumn: "id_producto");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_id_establecimiento",
                table: "Administrador",
                column: "id_establecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_id_carrito",
                table: "Cliente",
                column: "id_carrito");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_id_establecimiento",
                table: "Cliente",
                column: "id_establecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_id_establecimiento",
                table: "Compra",
                column: "id_establecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_numero_telefono",
                table: "Compra",
                column: "numero_telefono");

            migrationBuilder.CreateIndex(
                name: "IX_CompraDetalles_id_compra",
                table: "CompraDetalles",
                column: "id_compra");

            migrationBuilder.CreateIndex(
                name: "IX_CompraDetalles_id_producto",
                table: "CompraDetalles",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVenta_id_producto",
                table: "DetalleVenta",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVenta_id_venta",
                table: "DetalleVenta",
                column: "id_venta");

            migrationBuilder.CreateIndex(
                name: "IX_Establecimiento_id_inventario",
                table: "Establecimiento",
                column: "id_inventario");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_id_descuento",
                table: "Producto",
                column: "id_descuento");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_id_tipo_producto",
                table: "Producto",
                column: "id_tipo_producto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoInventario_id_inventario",
                table: "ProductoInventario",
                column: "id_inventario");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoInventario_id_producto",
                table: "ProductoInventario",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosEnCarrito_id_carrito",
                table: "ProductosEnCarrito",
                column: "id_carrito");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosEnCarrito_id_producto",
                table: "ProductosEnCarrito",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_id_establecimiento",
                table: "Venta",
                column: "id_establecimiento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "CompraDetalles");

            migrationBuilder.DropTable(
                name: "DetalleVenta");

            migrationBuilder.DropTable(
                name: "ProductoInventario");

            migrationBuilder.DropTable(
                name: "ProductosEnCarrito");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Descuento");

            migrationBuilder.DropTable(
                name: "Tipo_Producto");

            migrationBuilder.DropTable(
                name: "Carrito");

            migrationBuilder.DropTable(
                name: "Establecimiento");

            migrationBuilder.DropTable(
                name: "Inventario");
        }
    }
}
