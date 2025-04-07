using GerenciadorTarefas.Domain.Entities;

namespace GerenciadorTarefas.Domain.Interfaces.Services;

public interface IHistoricoAlteracaoService
{
    Task<List<HistoricoAlteracao>> ObterHistoricoAlteracao(Guid projetoId, Tarefa tarefa);
}
