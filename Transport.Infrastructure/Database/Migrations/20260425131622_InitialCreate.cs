using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Transport.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transport_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transport_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transport",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    number_plate = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    max_passengers_count = table.Column<byte>(type: "smallint", nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transport", x => x.id);
                    table.ForeignKey(
                        name: "fk_transport_transport_type_type_id",
                        column: x => x.type_id,
                        principalTable: "transport_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "transport_type",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Самолет" },
                    { 2, "Вертолет" },
                    { 3, "Автобус" },
                    { 4, "Микроавтобус" },
                    { 5, "Легковой автомобиль" },
                    { 6, "Минивэн" },
                    { 7, "Внедорожник" },
                    { 8, "Грузовик" },
                    { 9, "Поезд" },
                    { 10, "Катер" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_transport_number_plate",
                table: "transport",
                column: "number_plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_transport_type_id",
                table: "transport",
                column: "type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transport");

            migrationBuilder.DropTable(
                name: "transport_type");
        }
    }
}
