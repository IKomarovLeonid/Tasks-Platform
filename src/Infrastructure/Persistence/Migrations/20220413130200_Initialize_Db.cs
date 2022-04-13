using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Initialize_Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    key = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: false),
                    updated_utc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.key);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    id = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    state = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    created_utc = table.Column<DateTime>(nullable: false),
                    updated_utc = table.Column<DateTime>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    status = table.Column<string>(nullable: false),
                    expiration_utc = table.Column<DateTime>(nullable: true),
                    category = table.Column<string>(nullable: true),
                    priority = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
