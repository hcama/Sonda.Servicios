using Microsoft.EntityFrameworkCore.Migrations;

namespace Sonda.Data.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
            .Sql("INSERT INTO TipoClientes (Descripcion) Values ('SILVER')");
            migrationBuilder
            .Sql("INSERT INTO TipoClientes (Descripcion) Values ('GOLD')");
            migrationBuilder
            .Sql("INSERT INTO TipoClientes (Descripcion) Values ('PLATINIUM')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM TipoClientes");
        }
    }
}
