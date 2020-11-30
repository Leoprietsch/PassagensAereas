using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PassagensAereasAPI.Infra.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClasseDeVoo",
                columns: table => new
                {
                    Descricao = table.Column<string>(maxLength: 30, nullable: false),
                    ValorFixoDoVoo = table.Column<double>(nullable: false),
                    ValorPorMilha = table.Column<double>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasseDeVoo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Local",
                columns: table => new
                {
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Local", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opcional",
                columns: table => new
                {
                    Nome = table.Column<string>(maxLength: 30, nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opcional", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    PrimeiroNome = table.Column<string>(maxLength: 30, nullable: false),
                    UltimoNome = table.Column<string>(maxLength: 30, nullable: false),
                    Cpf = table.Column<string>(maxLength: 11, nullable: false),
                    DataDeNascimento = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    Senha = table.Column<string>(maxLength: 150, nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Admin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trecho",
                columns: table => new
                {
                    LocalOrigemId = table.Column<int>(nullable: false),
                    LocalDestinoId = table.Column<int>(nullable: false),
                    Distancia = table.Column<double>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trecho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trecho_Local_LocalDestinoId",
                        column: x => x.LocalDestinoId,
                        principalTable: "Local",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trecho_Local_LocalOrigemId",
                        column: x => x.LocalOrigemId,
                        principalTable: "Local",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    Valor = table.Column<double>(nullable: false),
                    TrechoId = table.Column<int>(nullable: false),
                    ClasseDeVooId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserva_ClasseDeVoo_ClasseDeVooId",
                        column: x => x.ClasseDeVooId,
                        principalTable: "ClasseDeVoo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Trecho_TrechoId",
                        column: x => x.TrechoId,
                        principalTable: "Trecho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservaOpcional",
                columns: table => new
                {
                    OpcionalId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReservaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaOpcional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservaOpcional_Opcional_OpcionalId",
                        column: x => x.OpcionalId,
                        principalTable: "Opcional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservaOpcional_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reserva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_ClasseDeVooId",
                table: "Reserva",
                column: "ClasseDeVooId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_TrechoId",
                table: "Reserva",
                column: "TrechoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_UsuarioId",
                table: "Reserva",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaOpcional_OpcionalId",
                table: "ReservaOpcional",
                column: "OpcionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaOpcional_ReservaId",
                table: "ReservaOpcional",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Trecho_LocalDestinoId",
                table: "Trecho",
                column: "LocalDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Trecho_LocalOrigemId",
                table: "Trecho",
                column: "LocalOrigemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservaOpcional");

            migrationBuilder.DropTable(
                name: "Opcional");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "ClasseDeVoo");

            migrationBuilder.DropTable(
                name: "Trecho");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Local");
        }
    }
}
