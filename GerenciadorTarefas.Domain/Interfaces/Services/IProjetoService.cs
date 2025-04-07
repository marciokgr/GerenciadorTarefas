namespace GerenciadorTarefas.Domain.Interfaces.Services;

public interface IProjetoService
{
    Task ValidarSeProjetoTemTarefasPendentes(Guid projetoId);
    Task ValidarLimiteMaximoTarefasPorProjeto(Guid projetoId);
}
