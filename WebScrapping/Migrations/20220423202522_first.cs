using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebScrapping.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Selector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FindSuccess = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urls");
        }
    }
}
