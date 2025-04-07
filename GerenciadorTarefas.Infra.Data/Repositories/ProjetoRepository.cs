using Microsoft.EntityFrameworkCore;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Infra.Data.Contextos;

namespace GerenciadorTarefas.Infra.Data.Repositories;

public class ProjetoRepository : RepositoryBase<Projeto>, IProjetoRepository
{
    public ProjetoRepository(RepositoryContext context) : base(context)
    {
        
    }
    public async Task<Projeto> ObterProjetoPorId(Guid projetoId, bool trackChanges)
    {
        return await GetByCondition(x => x.Id == projetoId);
    }

    public async Task<Projeto> ObterProjetoComTarefa(Guid projetoId)
    {
        var projeto = await this.context.Projetos
            .Where(x => x.Id == projetoId)
            .AsNoTracking()
            .Include(x => x.Tarefa)
            .FirstOrDefaultAsync();

        return projeto;
    }

    public void RemoverProjeto(Projeto projeto)
    {
        Remove(projeto);
    }
}
