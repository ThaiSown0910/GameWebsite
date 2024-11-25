using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebAPIDemo1.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checkouts",
                columns: table => new
                {
                    saleid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customerid = table.Column<int>(type: "integer", nullable: false),
                    saledate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    totalinvoiceamount = table.Column<decimal>(type: "numeric", nullable: false),
                    discount = table.Column<decimal>(type: "numeric", nullable: false),
                    paymentnaration = table.Column<string>(type: "text", nullable: false),
                    deliveryaddress1 = table.Column<string>(type: "text", nullable: false),
                    deliveryaddress2 = table.Column<string>(type: "text", nullable: false),
                    deliverycity = table.Column<string>(type: "text", nullable: false),
                    deliverypincode = table.Column<string>(type: "text", nullable: false),
                    deliverylandmark = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkouts", x => x.saleid);
                    table.ForeignKey(
                        name: "FK_Checkouts_Logins_customerid",
                        column: x => x.customerid,
                        principalTable: "Logins",
                        principalColumn: "customerid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_customerid",
                table: "Checkouts",
                column: "customerid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checkouts");
        }
    }
}
