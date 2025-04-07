using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Enum;

namespace GerenciadorTarefas.Infra.Data.Configurations;

public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.HasData(
            new
            {
                Id = new Guid("763203B8-CE0F-489A-867B-89CDFD48D77D"),
                DataVencimento = new DateTime(2025, 05, 16, 09, 4, 00, 753, DateTimeKind.Local).AddTicks(3568),
                Descricao = "cadastrar o usuário do José no dominio.",
                Prioridade = PrioridadeTarefaEnum.Baixa,
                ProjetoId = new Guid("EA2E398F-DCC9-4BE2-B30A-0F238EB998DD"),
                Status = TarefaStatusEnum.Pendente,
                Titulo = "Cadastrar usuário na rede",
                UsuarioId = new Guid("26771815-19A3-4876-B934-F5696A9A58F2")
            },
            new
            {
                Id = new Guid("EE73321D-4CB2-4A82-B8B5-82F4926D78F7"),
                DataVencimento = new DateTime(2025, 05, 21, 09, 4, 00, 753, DateTimeKind.Local).AddTicks(3586),
                Descricao = "Precisamos criar email para a maria participar do projeto",
                Prioridade = PrioridadeTarefaEnum.Baixa,
                ProjetoId = new Guid("EA2E398F-DCC9-4BE2-B30A-0F238EB998DD"),
                Status = TarefaStatusEnum.Pendente,
                Titulo = "Criar email para a PO Maria",
                UsuarioId = new Guid("26771815-19A3-4876-B934-F5696A9A58F2")
            },
            new
            {
                Id = new Guid("B187B582-F80D-404F-91FC-6FAE9D1B65C4"),
                DataVencimento = new DateTime(2025, 05, 21, 09, 4, 00, 753, DateTimeKind.Local).AddTicks(3591),
                Descricao = "Essa é uma tarefa para versionar o código do projeto no GIT",
                Prioridade = PrioridadeTarefaEnum.Baixa,
                ProjetoId = new Guid("EA2E398F-DCC9-4BE2-B30A-0F238EB998DD"),
                Status = TarefaStatusEnum.Pendente,
                Titulo = "Criar novo repositório para o gerenciador de tarefas",
                UsuarioId = new Guid("26771815-19A3-4876-B934-F5696A9A58F2")
            },
            new
            {
                Id = new Guid("AFBE0916-BA9C-4AB4-A9B3-D4C407971F5D"),
                DataVencimento = new DateTime(2025, 05, 21, 09, 4, 00, 753, DateTimeKind.Local).AddTicks(3594),
                Descricao = "Essa é uma tarefa para cadastrar usuários no banco de dados",
                Prioridade = PrioridadeTarefaEnum.Baixa,
                ProjetoId = new Guid("EA2E398F-DCC9-4BE2-B30A-0F238EB998DD"),
                Status = TarefaStatusEnum.Pendente,
                Titulo = "Cadastrar Usuários no banco de dados para utilização na aplicação",
                UsuarioId = new Guid("26771815-19A3-4876-B934-F5696A9A58F2")
            },
            new
            {
                Id = new Guid("A294175D-6F9B-450D-8306-1E1F863D000E"),
                DataVencimento = new DateTime(2025, 05, 26, 09, 4, 00, 753, DateTimeKind.Local).AddTicks(3597),
                Descricao = "Precisamos fazer teste da aplicação para sabermos a carga de usuarios que a aplicação comporta",
                Prioridade = PrioridadeTarefaEnum.Alta,
                ProjetoId = new Guid("B428A25B-28DB-4190-8033-EEDC63EFC129"),
                Status = TarefaStatusEnum.Pendente,
                Titulo = "Criação de teste utilizando k6",
                UsuarioId = new Guid("2A948373-AC6B-4EDD-BED0-C26741BC627A")
            },
            new
            {
                Id = new Guid("EBB552F8-E8BD-4CA4-9D02-E7F089256DE8"),
                DataVencimento = new DateTime(2025, 05, 26, 09, 4, 00, 753, DateTimeKind.Local).AddTicks(3600),
                Descricao = "Precisamos criar a pipeline, e nela precisamos também, executar testes e gerar relatorio de cobertura.",
                Prioridade = PrioridadeTarefaEnum.Baixa,
                ProjetoId = new Guid("B428A25B-28DB-4190-8033-EEDC63EFC129"),
                Status = TarefaStatusEnum.Pendente,
                Titulo = "Criação pipeline Gerenciador de Tarefas",
                UsuarioId = new Guid("2A948373-AC6B-4EDD-BED0-C26741BC627A")
            }
        );
    }
}
