using GerenciadorTarefas.Infra.Data.Helpers;

namespace GerenciadorTarefas.Application.Interfaces;

public interface IProjetoAppService
{
    public Task<MessageHelper> RemoverProjeto(Guid projetoId);
}
