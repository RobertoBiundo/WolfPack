using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 3000, nullable: true),
                    CreatedOnDateTimeOffsetUtc = table.Column<DateTimeOffset>(nullable: false),
                    LastModifiedDateTimeOffsetUtc = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wolfs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 64, nullable: false),
                    InsertionName = table.Column<string>(maxLength: 64, nullable: true),
                    LastName = table.Column<string>(maxLength: 64, nullable: false),
                    Gender = table.Column<byte>(type: "TINYINT", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "DATE", nullable: true),
                    Latitude = table.Column<decimal>(type: "DECIMAL(9,6)", nullable: true),
                    Longitude = table.Column<decimal>(type: "DECIMAL(9,6)", nullable: true),
                    CreatedOnDateTimeOffsetUtc = table.Column<DateTimeOffset>(nullable: false),
                    LastModifiedDateTimeOffsetUtc = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wolfs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WolfPack",
                columns: table => new
                {
                    WolfId = table.Column<Guid>(nullable: false),
                    PackId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WolfPack", x => new { x.WolfId, x.PackId });
                    table.ForeignKey(
                        name: "FK_WolfPack_Packs_PackId",
                        column: x => x.PackId,
                        principalTable: "Packs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WolfPack_Wolfs_WolfId",
                        column: x => x.WolfId,
                        principalTable: "Wolfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WolfPack_PackId",
                table: "WolfPack",
                column: "PackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WolfPack");

            migrationBuilder.DropTable(
                name: "Packs");

            migrationBuilder.DropTable(
                name: "Wolfs");
        }
    }
}
