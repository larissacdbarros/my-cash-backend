using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCash.API.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bancos",
                columns: table => new
                {
                    BancoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bancos", x => x.BancoId);
                });

            migrationBuilder.CreateTable(
                name: "CategoriasContas",
                columns: table => new
                {
                    CategoriaContaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasContas", x => x.CategoriaContaId);
                });

            migrationBuilder.CreateTable(
                name: "CategoriasDespesas",
                columns: table => new
                {
                    CategoriaDespesaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasDespesas", x => x.CategoriaDespesaId);
                });

            migrationBuilder.CreateTable(
                name: "CategoriasReceitas",
                columns: table => new
                {
                    CategoriaReceitaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasReceitas", x => x.CategoriaReceitaId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "SubcategoriasDespesas",
                columns: table => new
                {
                    SubcategoriaDespesaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaDespesaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcategoriasDespesas", x => x.SubcategoriaDespesaId);
                    table.ForeignKey(
                        name: "FK_SubcategoriasDespesas_CategoriasDespesas_CategoriaDespesaId",
                        column: x => x.CategoriaDespesaId,
                        principalTable: "CategoriasDespesas",
                        principalColumn: "CategoriaDespesaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubcategoriasReceitas",
                columns: table => new
                {
                    SubcategoriaReceitaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaReceitaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcategoriasReceitas", x => x.SubcategoriaReceitaId);
                    table.ForeignKey(
                        name: "FK_SubcategoriasReceitas_CategoriasReceitas_CategoriaReceitaId",
                        column: x => x.CategoriaReceitaId,
                        principalTable: "CategoriasReceitas",
                        principalColumn: "CategoriaReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    ContaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BancoId = table.Column<int>(type: "int", nullable: false),
                    SaldoAtual = table.Column<double>(type: "float", nullable: false),
                    CategoriaContaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.ContaId);
                    table.ForeignKey(
                        name: "FK_Contas_Bancos_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Bancos",
                        principalColumn: "BancoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contas_CategoriasContas_CategoriaContaId",
                        column: x => x.CategoriaContaId,
                        principalTable: "CategoriasContas",
                        principalColumn: "CategoriaContaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespesasConta",
                columns: table => new
                {
                    DespesaContaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubcategoriaDespesaId = table.Column<int>(type: "int", nullable: false),
                    ContaId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    Data = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesasConta", x => x.DespesaContaId);
                    table.ForeignKey(
                        name: "FK_DespesasConta_Contas_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Contas",
                        principalColumn: "ContaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DespesasConta_SubcategoriasDespesas_SubcategoriaDespesaId",
                        column: x => x.SubcategoriaDespesaId,
                        principalTable: "SubcategoriasDespesas",
                        principalColumn: "SubcategoriaDespesaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faturas",
                columns: table => new
                {
                    FaturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataFechamentoFatura = table.Column<DateTime>(type: "Date", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    ValorFatura = table.Column<double>(type: "float", nullable: false),
                    isFaturaFechada = table.Column<bool>(type: "bit", nullable: false),
                    isFaturaPaga = table.Column<bool>(type: "bit", nullable: false),
                    ContaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturas", x => x.FaturaId);
                    table.ForeignKey(
                        name: "FK_Faturas_Contas_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Contas",
                        principalColumn: "ContaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receitas",
                columns: table => new
                {
                    ReceitaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubcategoriaReceitaId = table.Column<int>(type: "int", nullable: false),
                    ContaId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "Date", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receitas", x => x.ReceitaId);
                    table.ForeignKey(
                        name: "FK_Receitas_Contas_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Contas",
                        principalColumn: "ContaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receitas_SubcategoriasReceitas_SubcategoriaReceitaId",
                        column: x => x.SubcategoriaReceitaId,
                        principalTable: "SubcategoriasReceitas",
                        principalColumn: "SubcategoriaReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespesasCartao",
                columns: table => new
                {
                    DespesaCartaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubcategoriaDespesaId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    Data = table.Column<DateTime>(type: "Date", nullable: false),
                    FaturaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesasCartao", x => x.DespesaCartaoId);
                    table.ForeignKey(
                        name: "FK_DespesasCartao_Faturas_FaturaId",
                        column: x => x.FaturaId,
                        principalTable: "Faturas",
                        principalColumn: "FaturaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DespesasCartao_SubcategoriasDespesas_SubcategoriaDespesaId",
                        column: x => x.SubcategoriaDespesaId,
                        principalTable: "SubcategoriasDespesas",
                        principalColumn: "SubcategoriaDespesaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contas_BancoId",
                table: "Contas",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contas_CategoriaContaId",
                table: "Contas",
                column: "CategoriaContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contas_UsuarioId",
                table: "Contas",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DespesasCartao_FaturaId",
                table: "DespesasCartao",
                column: "FaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasCartao_SubcategoriaDespesaId",
                table: "DespesasCartao",
                column: "SubcategoriaDespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasConta_ContaId",
                table: "DespesasConta",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasConta_SubcategoriaDespesaId",
                table: "DespesasConta",
                column: "SubcategoriaDespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_ContaId",
                table: "Faturas",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_ContaId",
                table: "Receitas",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_SubcategoriaReceitaId",
                table: "Receitas",
                column: "SubcategoriaReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubcategoriasDespesas_CategoriaDespesaId",
                table: "SubcategoriasDespesas",
                column: "CategoriaDespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubcategoriasReceitas_CategoriaReceitaId",
                table: "SubcategoriasReceitas",
                column: "CategoriaReceitaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DespesasCartao");

            migrationBuilder.DropTable(
                name: "DespesasConta");

            migrationBuilder.DropTable(
                name: "Receitas");

            migrationBuilder.DropTable(
                name: "Faturas");

            migrationBuilder.DropTable(
                name: "SubcategoriasDespesas");

            migrationBuilder.DropTable(
                name: "SubcategoriasReceitas");

            migrationBuilder.DropTable(
                name: "Contas");

            migrationBuilder.DropTable(
                name: "CategoriasDespesas");

            migrationBuilder.DropTable(
                name: "CategoriasReceitas");

            migrationBuilder.DropTable(
                name: "Bancos");

            migrationBuilder.DropTable(
                name: "CategoriasContas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
