using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiGerenciadorDeViagens.Migrations
{
    /// <inheritdoc />
    public partial class BancodeDados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tabela_Usuario",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Endereco_Rua = table.Column<string>(type: "text", nullable: false),
                    Endereco_Bairro = table.Column<string>(type: "text", nullable: false),
                    Endereco_Numero = table.Column<string>(type: "text", nullable: false),
                    Telefone_Fixo = table.Column<string>(type: "text", nullable: false),
                    Telefone_Celular = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabela_Usuario", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "Tabela_Viagem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Origem = table.Column<string>(type: "text", nullable: false),
                    Destino = table.Column<string>(type: "text", nullable: false),
                    dataIda = table.Column<string>(type: "text", nullable: false),
                    dataVolta = table.Column<string>(type: "text", nullable: false),
                    horaIda = table.Column<string>(type: "text", nullable: false),
                    horaVolta = table.Column<string>(type: "text", nullable: false),
                    Companhia = table.Column<string>(type: "text", nullable: false),
                    Precos = table.Column<double>(type: "double precision", nullable: false),
                    Cadeiras = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabela_Viagem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tabela_Passagem",
                columns: table => new
                {
                    NumeroPassagem = table.Column<Guid>(type: "uuid", nullable: false),
                    Origem = table.Column<string>(type: "text", nullable: false),
                    Destino = table.Column<string>(type: "text", nullable: false),
                    DataIda = table.Column<string>(type: "text", nullable: false),
                    DataVolta = table.Column<string>(type: "text", nullable: false),
                    HoraIda = table.Column<string>(type: "text", nullable: false),
                    HoraVolta = table.Column<string>(type: "text", nullable: false),
                    Companhia = table.Column<string>(type: "text", nullable: false),
                    assentos = table.Column<int>(type: "integer", nullable: false),
                    FormaDePagamento = table.Column<string>(type: "text", nullable: false),
                    Cpf = table.Column<string>(type: "text", nullable: false),
                    UsuarioCPF = table.Column<string>(type: "text", nullable: false),
                    ViagensId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabela_Passagem", x => x.NumeroPassagem);
                    table.ForeignKey(
                        name: "FK_Tabela_Passagem_Tabela_Usuario_UsuarioCPF",
                        column: x => x.UsuarioCPF,
                        principalTable: "Tabela_Usuario",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tabela_Passagem_Tabela_Viagem_ViagensId",
                        column: x => x.ViagensId,
                        principalTable: "Tabela_Viagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tabela_Passagem_UsuarioCPF",
                table: "Tabela_Passagem",
                column: "UsuarioCPF");

            migrationBuilder.CreateIndex(
                name: "IX_Tabela_Passagem_ViagensId",
                table: "Tabela_Passagem",
                column: "ViagensId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tabela_Passagem");

            migrationBuilder.DropTable(
                name: "Tabela_Usuario");

            migrationBuilder.DropTable(
                name: "Tabela_Viagem");
        }
    }
}
