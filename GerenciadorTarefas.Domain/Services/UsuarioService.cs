using GerenciadorTarefas.Domain.Exceptions;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Domain.Interfaces.Services;

namespace GerenciadorTarefas.Domain.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        this.usuarioRepository = usuarioRepository;
    }

    public async Task<bool> ValidarSeUsuarioExiste(Guid id)
    {
        var usuario = await this.usuarioRepository.ObterUsuarioPorId(id);

        if (usuario is not null)
            return true;
        else
            throw new OperacaoNaoPermitidaException($"O usuário informado não está cadastrado no sistema: {id}");
    }
}
