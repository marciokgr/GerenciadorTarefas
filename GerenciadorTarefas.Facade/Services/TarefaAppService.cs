using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using GerenciadorTarefas.Application.Interfaces;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Exceptions;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Domain.Interfaces.Services;
using GerenciadorTarefas.Infra.Data.DTO;
using GerenciadorTarefas.Infra.Data.Helpers;

namespace GerenciadorTarefas.Application.Services;

public class TarefaAppService : ITarefaAppService
{
    private readonly IUnitOfWork repositoryManager;
    private readonly ITarefaService tarefaService;
    private readonly IProjetoService projetoService;
    private readonly IHistoricoAlteracaoService historicoAlteracaoService;
    private readonly IUsuarioService usuarioService;
    private readonly IMapper mapper;

    public TarefaAppService(IUnitOfWork repositoryManager, 
        ITarefaService tarefaService,
        IHistoricoAlteracaoService historicoAlteracaoService, 
        IProjetoService projetoService, IUsuarioService usuarioService, 
        IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.tarefaService = tarefaService;
        this.historicoAlteracaoService = historicoAlteracaoService;
        this.projetoService = projetoService;
        this.usuarioService = usuarioService;
        this.mapper = mapper;
    }

    public async Task<MessageHelper<TarefaDto>> CriarTarefa(Guid projetoId, Guid usuarioId, TarefaCreationDto tarefaDto)
    {
        var message = new MessageHelper<TarefaDto>();

        try
        {
            var tarefa = this.mapper.Map<Tarefa>(tarefaDto);

            this.tarefaService.ConfigurarPrioriedade(tarefa);

            await this.projetoService.ValidarLimiteMaximoTarefasPorProjeto(projetoId);
            
            var usuarioExiste = await this.usuarioService.ValidarSeUsuarioExiste(usuarioId);

            if (usuarioExiste)
            {
                this.repositoryManager.Tarefa.CriarTarefaPorProjeto(projetoId, usuarioId, tarefa);

                await this.repositoryManager.Commit();

                message.Ok(this.mapper.Map<TarefaDto>(tarefa));
            }
        }
        catch (OperacaoNaoPermitidaException ex)
        {
            message.BadRequest(ex);
        }
        catch (Exception ex)
        {
            message.Error(ex);
        }

        return message;
    }

    public async Task<MessageHelper> AtualizarCamposTarefa(Guid projetoId, Guid id, JsonPatchDocument<TarefaUpdateDto> patchDoc)
    {
        var message = new MessageHelper();

        try
        {
            var projeto = await this.repositoryManager.Projeto.ObterProjetoPorId(projetoId, false) ?? throw new Exception("O projeto não foi encontrado");
            var tarefa = await this.repositoryManager.Tarefa.ObterTarefaPorId(projetoId, id, true) ?? throw new Exception("A tarefa não foi encontrada");

            if (patchDoc.Operations.Where(x => x.path.Contains("prioridade")).Any())
                this.tarefaService.ValidarPrioriedade(tarefa);

            var tarefaToPatch = this.mapper.Map<TarefaUpdateDto>(tarefa);

            patchDoc.ApplyTo(tarefaToPatch);

            this.mapper.Map(tarefaToPatch, tarefa);

            var historicoAlteracoes = await this.historicoAlteracaoService.ObterHistoricoAlteracao(projetoId, tarefa);

            foreach (var historicoAlteracao in historicoAlteracoes)
                this.repositoryManager.HistoricoAlteracao.CriarHistorico(id, historicoAlteracao);

            await this.repositoryManager.Commit();

            message.Ok();
        }
        catch (OperacaoNaoPermitidaException ex)
        {
            message.BadRequest(ex);
        }
        catch (Exception ex)
        {
            message.Error(ex);
        }

        return message;
    }

    public async Task<MessageHelper> RemoverTarefaPorProjeto(Guid projetoId, Guid id)
    {
        var message = new MessageHelper();

        try
        {
            var projeto = await this.repositoryManager.Projeto.ObterProjetoPorId(projetoId, false);

            if (projeto == null)
                throw new Exception("O projeto não foi encontrado");

            var tarefa = await this.repositoryManager.Tarefa.ObterTarefaPorId(projetoId, id, false);

            if (tarefa == null)
                throw new Exception("A tarefa não foi encontrada");

            this.repositoryManager.Tarefa.RemoverTarefa(tarefa);

            await this.repositoryManager.Commit();

            message.Ok();
        }
        catch (Exception ex)
        {
            message.Error(ex);
        }

        return message;
    }

    public async Task<MessageHelper<Dictionary<Guid, double>>> ObterMediaTarefasConcluidasPorUsuario(Guid usuarioId)
    {
        var message = new MessageHelper<Dictionary<Guid, double>>();

        try
        {
            var dados = await this.tarefaService.ObterMediaTarefasConcluidasPorUsuario(usuarioId);

            if (!dados.Any())
            {
                message.NotFound("Nenhum registro encontrado.");

                return message;
            }

            message.Ok(dados);
        }
        catch(Exception ex)
        {
            message.Error(ex);
        }

        return message;
    }
}
