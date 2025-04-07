using GerenciadorTarefas.Application.Interfaces;
using GerenciadorTarefas.Domain.Exceptions;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Domain.Interfaces.Services;
using GerenciadorTarefas.Infra.Data.Helpers;

namespace GerenciadorTarefas.Application.Services;

public class ProjetoAppService : IProjetoAppService
{
    private readonly IUnitOfWork repositoryManager;
    private readonly IProjetoService projetoService;

    public ProjetoAppService(IUnitOfWork repositoryManager, IProjetoService projetoService)
    {
        this.repositoryManager = repositoryManager;
        this.projetoService = projetoService;
    }
    
    public async Task<MessageHelper> RemoverProjeto(Guid projetoId)
    {
        var message = new MessageHelper();

        try
        {
            await this.projetoService.ValidarSeProjetoTemTarefasPendentes(projetoId);

            var projeto = await this.repositoryManager.Projeto.ObterProjetoPorId(projetoId, false);

            this.repositoryManager.Projeto.RemoverProjeto(projeto);

            await this.repositoryManager.Commit();

            message.Ok();
        }
        catch(OperacaoNaoPermitidaException ex)
        {
            message.BadRequest(ex);
        }
        catch(Exception ex)
        {
            message.Error(ex);
        }

        return message;
    }
}
