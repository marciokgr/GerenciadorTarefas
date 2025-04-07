
namespace GerenciadorTarefas.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    ITarefaRepository Tarefa { get; }
    IProjetoRepository Projeto { get; }
    IHistoricoAlteracaoRepository HistoricoAlteracao { get; }
    Task Commit();
}
