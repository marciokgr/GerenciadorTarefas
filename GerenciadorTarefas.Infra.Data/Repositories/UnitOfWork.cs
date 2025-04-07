using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Infra.Data.Contextos;
using GerenciadorTarefas.Infra.Data.Enums;
using GerenciadorTarefas.Infra.Data.Factories;

namespace GerenciadorTarefas.Infra.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private ITarefaRepository tarefa;
    private IProjetoRepository projeto;
    private IHistoricoAlteracaoRepository historicoAlteracao;

    private RepositoryContext contexto;

    public ITarefaRepository Tarefa
    {
        get
        {
            if (this.tarefa == null)
                return RepositoryFactory.Create(RepositoryTypeEnum.Tarefa, contexto);

            return this.tarefa;
        }
    }

    public IProjetoRepository Projeto
    {
        get
        {
            if (this.projeto == null)
                return RepositoryFactory.Create(RepositoryTypeEnum.Projeto, contexto);

            return this.projeto;
        }
    }

    public IHistoricoAlteracaoRepository HistoricoAlteracao
    {
        get
        {
            if (this.historicoAlteracao == null)
                return RepositoryFactory.Create(RepositoryTypeEnum.HistoricoAlteracao, contexto);

            return this.historicoAlteracao;
        }
    }           

    public UnitOfWork(RepositoryContext contexto)
    {
        this.contexto = contexto;
    }

    public async Task Commit()
    {
        await contexto.SaveChangesAsync();
    }
}
