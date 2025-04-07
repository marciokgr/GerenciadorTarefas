using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Infra.Data.Contextos;

namespace GerenciadorTarefas.Infra.Data.Repositories;

public class HistoricoAlteracaoRepository : RepositoryBase<HistoricoAlteracao>, IHistoricoAlteracaoRepository
{
    public HistoricoAlteracaoRepository(RepositoryContext context) : base(context)
    {
        
    }
    public void CriarHistorico(Guid tarefaId, HistoricoAlteracao historico)
    {
        historico.TarefaId = tarefaId;
        Create(historico);
    }
}
