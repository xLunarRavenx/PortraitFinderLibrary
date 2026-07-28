using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortraitFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portraits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ThumbnailPath = table.Column<string>(type: "TEXT", nullable: true),
                    PortraitFolderPath = table.Column<string>(type: "TEXT", nullable: true),
                    ImageLastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    Race = table.Column<int>(type: "INTEGER", nullable: false),
                    HairColor = table.Column<int>(type: "INTEGER", nullable: false),
                    HairLength = table.Column<int>(type: "INTEGER", nullable: false),
                    HeadFeature = table.Column<int>(type: "INTEGER", nullable: false),
                    Wing = table.Column<int>(type: "INTEGER", nullable: false),
                    Weapon = table.Column<int>(type: "INTEGER", nullable: false),
                    Armor = table.Column<int>(type: "INTEGER", nullable: false),
                    Companion = table.Column<int>(type: "INTEGER", nullable: false),
                    Surrounding = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerClass = table.Column<long>(type: "INTEGER", nullable: false),
                    MythicPath = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portraits", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Portraits");
        }
    }
}
