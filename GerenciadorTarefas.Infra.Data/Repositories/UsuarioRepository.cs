using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Infra.Data.Contextos;

namespace GerenciadorTarefas.Infra.Data.Repositories;

public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(RepositoryContext context) : base(context)
    {
        
    }

    public async Task<Usuario> ObterUsuarioPorId(Guid id)
    {
        return await GetByCondition(x => x.Id == id);
    }
}
