using GerenciadorTarefas.Domain.Entities;

namespace GerenciadorTarefas.Domain.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario> ObterUsuarioPorId(Guid id);
}
