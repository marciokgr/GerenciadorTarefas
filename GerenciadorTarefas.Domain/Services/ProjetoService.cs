using GerenciadorTarefas.Domain.Enum;
using GerenciadorTarefas.Domain.Exceptions;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Domain.Interfaces.Services;

namespace GerenciadorTarefas.Domain.Services;

public class ProjetoService : IProjetoService
{
    private readonly IProjetoRepository projetoRepository;

    public ProjetoService(IProjetoRepository projetoRepository)
    {
        this.projetoRepository = projetoRepository;
    }

    public async Task ValidarSeProjetoTemTarefasPendentes(Guid projetoId)
    {
        var projeto = await this.projetoRepository.ObterProjetoComTarefa(projetoId);

        if (projeto is null)
            throw new OperacaoNaoPermitidaException("O projeto informado não existe");

        if (projeto.Tarefa is not null && projeto.Tarefa.Where(x => x.Status == TarefaStatusEnum.Pendente).Any())
            throw new OperacaoNaoPermitidaException("É necessário concluir ou remover as tarefas pendentes associadas ao projeto primeiro De de fazer sua remoção");
    }

    public async Task ValidarLimiteMaximoTarefasPorProjeto(Guid projetoId)
    {
        var projeto = await this.projetoRepository.ObterProjetoComTarefa(projetoId);

        if (projeto is null)
            throw new OperacaoNaoPermitidaException("O projeto informado não existe");

        if (projeto.Tarefa is not null && projeto.Tarefa.Count == 20)
            throw new OperacaoNaoPermitidaException("O limite de tarefas já foi atingida para este projeto");
    }
}
