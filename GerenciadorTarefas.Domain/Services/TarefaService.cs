using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Enum;
using GerenciadorTarefas.Domain.Exceptions;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Domain.Interfaces.Services;

namespace GerenciadorTarefas.Domain.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository tarefaRepository;

    public TarefaService(ITarefaRepository tarefaRepository)
    {
        this.tarefaRepository = tarefaRepository;
    }

    public void ConfigurarPrioriedade(Tarefa tarefa)
    {
        tarefa.Prioridade ??= PrioridadeTarefaEnum.Baixa;
    }

    public void ValidarPrioriedade(Tarefa tarefa)
    {
        if (tarefa.Prioridade is not null)             
            throw new OperacaoNaoPermitidaException("Não é permitido alterar a prioridade de uma tarefa Para que ela foi criada");
    }

    public async Task<Dictionary<Guid, double>> ObterMediaTarefasConcluidasPorUsuario(Guid usuarioId)
    {
        return await this.tarefaRepository.ObterMediaTarefasConcluidasPorUsuario(usuarioId);
    }
}
