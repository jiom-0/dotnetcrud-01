using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Host.Migrations
{
    /// <inheritdoc />
    public partial class Crud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: true),
                    preco = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    sld_atual = table.Column<int>(type: "integer", nullable: false),
                    categoria = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("produto_pkey", x => x.id);
                });
            
            migrationBuilder.InsertData(
            table: "produto",
            columns: new[] { "nome", "descricao", "preco", "sld_atual", "categoria" },
            values: new object[,]
            {
                { "Produto A", "Descrição do Produto A", 19.99m, 100, "A" },
                { "Produto B", "Descrição do Produto B", 29.99m, 200, "B" },
                { "Produto C", "Descrição do Produto C", 39.99m, 150, "C" },
                { "Produto D", "Descrição do Produto D", 49.99m, 80, "D" },
                { "Produto E", "Descrição do Produto E", 59.99m, 60, "A" },
                { "Produto F", "Descrição do Produto F", 69.99m, 120, "A" },
                { "Produto G", "Descrição do Produto G", 79.99m, 90, "C" },
                { "Produto H", "Descrição do Produto H", 89.99m, 50, "A" },
                { "Produto I", "Descrição do Produto I", 99.99m, 30, "B" },
                { "Produto J", "Descrição do Produto J", 109.99m, 10, "C" }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "produto");
        }
    }
}
