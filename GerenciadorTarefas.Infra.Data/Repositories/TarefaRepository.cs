using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Enum;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Infra.Data.Contextos;

namespace GerenciadorTarefas.Infra.Data.Repositories;

public class TarefaRepository : RepositoryBase<Tarefa>, ITarefaRepository
{
    public TarefaRepository(RepositoryContext context) : base(context)
    {
        
    }

    public void CriarTarefaPorProjeto(Guid projetoId, Guid usuarioId, Tarefa tarefa)
    {
        tarefa.ProjetoId = projetoId;
        tarefa.UsuarioId = usuarioId;
        Create(tarefa);
    }

    public async Task<Tarefa> ObterTarefaPorId(Guid projetoId, Guid id, bool trackChanges)
    {
        return await GetByCondition(x => x.ProjetoId == projetoId && x.Id == id, trackChanges);
    }

    public void RemoverTarefa(Tarefa tarefa)
    {
        Remove(tarefa);
    }

    public async Task<Dictionary<Guid, double>> ObterMediaTarefasConcluidasPorUsuario(Guid usuarioId)
    {
        var dataLimite = DateTime.UtcNow.AddDays(-30);

        var mediaPorUsuario = this.context.Tarefas
            .Where(t => t.DataVencimento >= dataLimite && t.Status == TarefaStatusEnum.Concluido)
            .GroupBy(t => usuarioId)
            .ToDictionary(g => g.Key, g => g.Count() / (double)30);

        return mediaPorUsuario;
    }
}
