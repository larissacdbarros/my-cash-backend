using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCash.API.Data.Migrations
{
    public partial class pagarfatura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DespesasCartao_SubcategoriasDespesas_SubcategoriaDespesaId",
                table: "DespesasCartao");

            migrationBuilder.AlterColumn<int>(
                name: "SubcategoriaDespesaId",
                table: "DespesasCartao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DespesasCartao_SubcategoriasDespesas_SubcategoriaDespesaId",
                table: "DespesasCartao",
                column: "SubcategoriaDespesaId",
                principalTable: "SubcategoriasDespesas",
                principalColumn: "SubcategoriaDespesaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DespesasCartao_SubcategoriasDespesas_SubcategoriaDespesaId",
                table: "DespesasCartao");

            migrationBuilder.AlterColumn<int>(
                name: "SubcategoriaDespesaId",
                table: "DespesasCartao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DespesasCartao_SubcategoriasDespesas_SubcategoriaDespesaId",
                table: "DespesasCartao",
                column: "SubcategoriaDespesaId",
                principalTable: "SubcategoriasDespesas",
                principalColumn: "SubcategoriaDespesaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
