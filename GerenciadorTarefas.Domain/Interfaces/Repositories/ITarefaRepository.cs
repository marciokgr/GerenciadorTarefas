using GerenciadorTarefas.Domain.Entities;

namespace GerenciadorTarefas.Domain.Interfaces.Repositories;

public interface ITarefaRepository
{
    void CriarTarefaPorProjeto(Guid projetoId, Guid usuarioId, Tarefa tarefa);
    Task<Tarefa> ObterTarefaPorId(Guid projetoId, Guid id, bool trackChanges);
    void RemoverTarefa(Tarefa tarefa);
    Task<Dictionary<Guid, double>> ObterMediaTarefasConcluidasPorUsuario(Guid usuarioId);
}
