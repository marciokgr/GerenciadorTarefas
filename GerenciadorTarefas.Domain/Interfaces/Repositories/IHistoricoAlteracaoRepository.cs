using GerenciadorTarefas.Domain.Entities;

namespace GerenciadorTarefas.Domain.Interfaces.Repositories;

public interface IHistoricoAlteracaoRepository
{
    void CriarHistorico(Guid tarefaId, HistoricoAlteracao historico);
}
