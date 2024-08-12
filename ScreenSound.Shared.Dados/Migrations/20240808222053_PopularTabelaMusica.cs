using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class PopularTabelaMusica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Musicas",new string[] { "Nome", "AnoLancamento" },  new object[] {"Pode Vir Comigo", "2012"});
            migrationBuilder.InsertData("Musicas",new string[] { "Nome", "AnoLancamento" },  new object[] {"Oceano", "1989"});
            migrationBuilder.InsertData("Musicas",new string[] { "Nome", "AnoLancamento" },  new object[] {"Best Of You", "2005"});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Musicas");
        }
    }
}
