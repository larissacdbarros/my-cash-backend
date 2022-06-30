using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCash.API.Data.Migrations
{
    public partial class nova : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataVencimentoFatura",
                table: "Faturas");

            migrationBuilder.RenameColumn(
                name: "isFaturaVencida",
                table: "Faturas",
                newName: "isFaturaFechada");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Receitas",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Mes",
                table: "Faturas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFechamentoFatura",
                table: "Faturas",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "Ano",
                table: "Faturas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ano",
                table: "Faturas");

            migrationBuilder.RenameColumn(
                name: "isFaturaFechada",
                table: "Faturas",
                newName: "isFaturaVencida");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Receitas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "Mes",
                table: "Faturas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFechamentoFatura",
                table: "Faturas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVencimentoFatura",
                table: "Faturas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
