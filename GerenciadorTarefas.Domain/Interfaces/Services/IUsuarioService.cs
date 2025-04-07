namespace GerenciadorTarefas.Domain.Interfaces.Services;

public interface IUsuarioService
{
    public Task<bool> ValidarSeUsuarioExiste(Guid id);
}
