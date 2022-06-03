using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Heliconia.Infrastructure.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Heliconia");

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "Heliconia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeliconiasUsers",
                schema: "Heliconia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lasname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncryptedPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeliconiasUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                schema: "Heliconia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    DatePurchase = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPurchasePrice = table.Column<double>(type: "float", nullable: false),
                    TotalUnits = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(36)", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Heliconia",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "Heliconia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeliconiaUserId = table.Column<string>(type: "varchar(36)", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_HeliconiasUsers_HeliconiaUserId",
                        column: x => x.HeliconiaUserId,
                        principalSchema: "Heliconia",
                        principalTable: "HeliconiasUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyLedgers",
                schema: "Heliconia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    MoneyTotalSales = table.Column<double>(type: "float", nullable: false),
                    TotalProductsSold = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<string>(type: "varchar(36)", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyLedgers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyLedgers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Heliconia",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                schema: "Heliconia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    CompanyId = table.Column<string>(type: "varchar(36)", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lasname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncryptedPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Heliconia",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                schema: "Heliconia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<string>(type: "varchar(36)", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Heliconia",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryElement",
                schema: "Heliconia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    CategoryId = table.Column<string>(type: "varchar(36)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMain = table.Column<bool>(type: "bit", nullable: true),
                    StoreId = table.Column<string>(type: "varchar(36)", nullable: true),
                    TotalProducts = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    CodigoBarras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryElementId = table.Column<string>(type: "varchar(36)", nullable: true),
                    NameLastState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseId = table.Column<string>(type: "varchar(36)", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryElement_CategoryElement_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Heliconia",
                        principalTable: "CategoryElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryElement_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalSchema: "Heliconia",
                        principalTable: "Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryElement_Stores_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Heliconia",
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                schema: "Heliconia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    StoreId = table.Column<string>(type: "varchar(36)", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lasname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncryptedPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_Stores_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Heliconia",
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstadosCategoria",
                schema: "Heliconia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoEnum = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<string>(type: "varchar(36)", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosCategoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstadosCategoria_CategoryElement_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Heliconia",
                        principalTable: "CategoryElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryElement_CategoryId",
                schema: "Heliconia",
                table: "CategoryElement",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryElement_PurchaseId",
                schema: "Heliconia",
                table: "CategoryElement",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryElement_StoreId",
                schema: "Heliconia",
                table: "CategoryElement",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_HeliconiaUserId",
                schema: "Heliconia",
                table: "Companies",
                column: "HeliconiaUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyLedgers_CompanyId",
                schema: "Heliconia",
                table: "DailyLedgers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosCategoria_ProductId",
                schema: "Heliconia",
                table: "EstadosCategoria",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_CompanyId",
                schema: "Heliconia",
                table: "Managers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CustomerId",
                schema: "Heliconia",
                table: "Purchases",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_CompanyId",
                schema: "Heliconia",
                table: "Stores",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_StoreId",
                schema: "Heliconia",
                table: "Workers",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyLedgers",
                schema: "Heliconia");

            migrationBuilder.DropTable(
                name: "EstadosCategoria",
                schema: "Heliconia");

            migrationBuilder.DropTable(
                name: "Managers",
                schema: "Heliconia");

            migrationBuilder.DropTable(
                name: "Workers",
                schema: "Heliconia");

            migrationBuilder.DropTable(
                name: "CategoryElement",
                schema: "Heliconia");

            migrationBuilder.DropTable(
                name: "Purchases",
                schema: "Heliconia");

            migrationBuilder.DropTable(
                name: "Stores",
                schema: "Heliconia");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "Heliconia");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "Heliconia");

            migrationBuilder.DropTable(
                name: "HeliconiasUsers",
                schema: "Heliconia");
        }
    }
}
