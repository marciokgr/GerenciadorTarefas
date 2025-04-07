using GerenciadorTarefas.Infra.Data.Contextos;
using GerenciadorTarefas.Infra.Data.Enums;
using GerenciadorTarefas.Infra.Data.Repositories;

namespace GerenciadorTarefas.Infra.Data.Factories;

public class RepositoryFactory
{
    public static dynamic? Create(RepositoryTypeEnum tipo, RepositoryContext context)
    {
        return tipo switch
        {
            RepositoryTypeEnum.Tarefa => new TarefaRepository(context),
            RepositoryTypeEnum.Projeto => new ProjetoRepository(context),
            RepositoryTypeEnum.HistoricoAlteracao => new HistoricoAlteracaoRepository(context),
            _=> null
        };
    }
}
