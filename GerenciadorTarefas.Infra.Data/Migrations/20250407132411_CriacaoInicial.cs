using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GerenciadorTarefas.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projetos",
                columns: table => new
                {
                    ProjetoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projetos", x => x.ProjetoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    TarefaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Prioridade = table.Column<int>(type: "int", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjetoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.TarefaId);
                    table.ForeignKey(
                        name: "FK_Tarefas_Projetos_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projetos",
                        principalColumn: "ProjetoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tarefas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoAlteracoes",
                columns: table => new
                {
                    HistoricoAlteracaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Campo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    De = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Para = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TarefaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoAlteracoes", x => x.HistoricoAlteracaoId);
                    table.ForeignKey(
                        name: "FK_HistoricoAlteracoes_Tarefas_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "Tarefas",
                        principalColumn: "TarefaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Projetos",
                columns: new[] { "ProjetoId", "Nome" },
                values: new object[,]
                {
                    { new Guid("b428a25b-28db-4190-8033-eedc63efc129"), "Projeto Tots" },
                    { new Guid("ea2e398f-dcc9-4be2-b30a-0f238eb998dd"), "Projeto Gupy " }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Nome" },
                values: new object[,]
                {
                    { new Guid("26771815-19a3-4876-b934-f5696a9a58f2"), "João da Silva" },
                    { new Guid("2a948373-ac6b-4edd-bed0-c26741bc627a"), "Márcio Krüger" }
                });

            migrationBuilder.InsertData(
                table: "Tarefas",
                columns: new[] { "TarefaId", "Comentario", "DataVencimento", "Descricao", "Prioridade", "ProjetoId", "Status", "Titulo", "UsuarioId" },
                values: new object[,]
                {
                    { new Guid("763203b8-ce0f-489a-867b-89cdfd48d77d"), null, new DateTime(2025, 5, 16, 9, 4, 0, 753, DateTimeKind.Local).AddTicks(3568), "cadastrar o usuário do José no dominio.", 1, new Guid("ea2e398f-dcc9-4be2-b30a-0f238eb998dd"), 1, "Cadastrar usuário na rede", new Guid("26771815-19a3-4876-b934-f5696a9a58f2") },
                    { new Guid("a294175d-6f9b-450d-8306-1e1f863d000e"), null, new DateTime(2025, 5, 26, 9, 4, 0, 753, DateTimeKind.Local).AddTicks(3597), "Precisamos fazer teste da aplicação para sabermos a carga de usuarios que a aplicação comporta", 3, new Guid("b428a25b-28db-4190-8033-eedc63efc129"), 1, "Criação de teste utilizando k6", new Guid("2a948373-ac6b-4edd-bed0-c26741bc627a") },
                    { new Guid("afbe0916-ba9c-4ab4-a9b3-d4c407971f5d"), null, new DateTime(2025, 5, 21, 9, 4, 0, 753, DateTimeKind.Local).AddTicks(3594), "Essa é uma tarefa para cadastrar usuários no banco de dados", 1, new Guid("ea2e398f-dcc9-4be2-b30a-0f238eb998dd"), 1, "Cadastrar Usuários no banco de dados para utilização na aplicação", new Guid("26771815-19a3-4876-b934-f5696a9a58f2") },
                    { new Guid("b187b582-f80d-404f-91fc-6fae9d1b65c4"), null, new DateTime(2025, 5, 21, 9, 4, 0, 753, DateTimeKind.Local).AddTicks(3591), "Essa é uma tarefa para versionar o código do projeto no GIT", 1, new Guid("ea2e398f-dcc9-4be2-b30a-0f238eb998dd"), 1, "Criar novo repositório para o gerenciador de tarefas", new Guid("26771815-19a3-4876-b934-f5696a9a58f2") },
                    { new Guid("ebb552f8-e8bd-4ca4-9d02-e7f089256de8"), null, new DateTime(2025, 5, 26, 9, 4, 0, 753, DateTimeKind.Local).AddTicks(3600), "Precisamos criar a pipeline, e nela precisamos também, executar testes e gerar relatorio de cobertura.", 1, new Guid("b428a25b-28db-4190-8033-eedc63efc129"), 1, "Criação pipeline Gerenciador de Tarefas", new Guid("2a948373-ac6b-4edd-bed0-c26741bc627a") },
                    { new Guid("ee73321d-4cb2-4a82-b8b5-82f4926d78f7"), null, new DateTime(2025, 5, 21, 9, 4, 0, 753, DateTimeKind.Local).AddTicks(3586), "Precisamos criar email para a maria participar do projeto", 1, new Guid("ea2e398f-dcc9-4be2-b30a-0f238eb998dd"), 1, "Criar email para a PO Maria", new Guid("26771815-19a3-4876-b934-f5696a9a58f2") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoAlteracoes_TarefaId",
                table: "HistoricoAlteracoes",
                column: "TarefaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_ProjetoId",
                table: "Tarefas",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_UsuarioId",
                table: "Tarefas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoAlteracoes");

            migrationBuilder.DropTable(
                name: "Tarefas");

            migrationBuilder.DropTable(
                name: "Projetos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
