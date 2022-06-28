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
                name: "BandeirasCartoes",
                columns: table => new
                {
                    BandeiraCartaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BandeirasCartoes", x => x.BandeiraCartaoId);
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
                name: "Objetivos",
                columns: table => new
                {
                    ObjetivoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorMensal = table.Column<double>(type: "float", nullable: false),
                    ValorFinal = table.Column<double>(type: "float", nullable: false),
                    ValorAtual = table.Column<double>(type: "float", nullable: false),
                    PercentualAcumulado = table.Column<double>(type: "float", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivos", x => x.ObjetivoId);
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
                name: "Metas",
                columns: table => new
                {
                    MetaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaDespesaId = table.Column<int>(type: "int", nullable: false),
                    LimiteParaGastar = table.Column<double>(type: "float", nullable: false),
                    PercentualGasto = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metas", x => x.MetaId);
                    table.ForeignKey(
                        name: "FK_Metas_CategoriasDespesas_CategoriaDespesaId",
                        column: x => x.CategoriaDespesaId,
                        principalTable: "CategoriasDespesas",
                        principalColumn: "CategoriaDespesaId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "CartoesCredito",
                columns: table => new
                {
                    CartaoCreditoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BandeiraCartaoId = table.Column<int>(type: "int", nullable: false),
                    LimiteCartao = table.Column<double>(type: "float", nullable: false),
                    ContaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartoesCredito", x => x.CartaoCreditoId);
                    table.ForeignKey(
                        name: "FK_CartoesCredito_BandeirasCartoes_BandeiraCartaoId",
                        column: x => x.BandeiraCartaoId,
                        principalTable: "BandeirasCartoes",
                        principalColumn: "BandeiraCartaoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartoesCredito_Contas_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Contas",
                        principalColumn: "ContaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartoesCredito_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
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
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "Receitas",
                columns: table => new
                {
                    ReceitaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubcategoriaReceitaId = table.Column<int>(type: "int", nullable: false),
                    ContaId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    CartaoCreditoId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubcategoriaDespesaId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesasCartao", x => x.DespesaCartaoId);
                    table.ForeignKey(
                        name: "FK_DespesasCartao_CartoesCredito_CartaoCreditoId",
                        column: x => x.CartaoCreditoId,
                        principalTable: "CartoesCredito",
                        principalColumn: "CartaoCreditoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DespesasCartao_SubcategoriasDespesas_SubcategoriaDespesaId",
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
                    CartaoCreditoId = table.Column<int>(type: "int", nullable: false),
                    DataFechamentoFatura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimentoFatura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorFatura = table.Column<double>(type: "float", nullable: false),
                    isFaturaVencida = table.Column<bool>(type: "bit", nullable: false),
                    isFaturaPaga = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturas", x => x.FaturaId);
                    table.ForeignKey(
                        name: "FK_Faturas_CartoesCredito_CartaoCreditoId",
                        column: x => x.CartaoCreditoId,
                        principalTable: "CartoesCredito",
                        principalColumn: "CartaoCreditoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartoesCredito_BandeiraCartaoId",
                table: "CartoesCredito",
                column: "BandeiraCartaoId");

            migrationBuilder.CreateIndex(
                name: "IX_CartoesCredito_ContaId",
                table: "CartoesCredito",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_CartoesCredito_UsuarioId",
                table: "CartoesCredito",
                column: "UsuarioId");

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
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasCartao_CartaoCreditoId",
                table: "DespesasCartao",
                column: "CartaoCreditoId");

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
                name: "IX_Faturas_CartaoCreditoId",
                table: "Faturas",
                column: "CartaoCreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_Metas_CategoriaDespesaId",
                table: "Metas",
                column: "CategoriaDespesaId");

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
                name: "Faturas");

            migrationBuilder.DropTable(
                name: "Metas");

            migrationBuilder.DropTable(
                name: "Objetivos");

            migrationBuilder.DropTable(
                name: "Receitas");

            migrationBuilder.DropTable(
                name: "SubcategoriasDespesas");

            migrationBuilder.DropTable(
                name: "CartoesCredito");

            migrationBuilder.DropTable(
                name: "SubcategoriasReceitas");

            migrationBuilder.DropTable(
                name: "CategoriasDespesas");

            migrationBuilder.DropTable(
                name: "BandeirasCartoes");

            migrationBuilder.DropTable(
                name: "Contas");

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
