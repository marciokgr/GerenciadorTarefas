using System.Reflection;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Domain.Interfaces.Services;

namespace GerenciadorTarefas.Domain.Services;

public class HistoricoAlteracaoService : IHistoricoAlteracaoService
{
    private readonly ITarefaRepository tarefaRepository;


    public HistoricoAlteracaoService(ITarefaRepository tarefaRepository)
    {
        this.tarefaRepository = tarefaRepository;
    }

    public async Task<List<HistoricoAlteracao>> ObterHistoricoAlteracao(Guid projetoId, Tarefa tarefa)
    {
        object? valorAntigo;
        object? valorNovo;

        var alteracoes = new List<HistoricoAlteracao>();

        var tarefaCadastrada = await this.tarefaRepository.ObterTarefaPorId(projetoId, tarefa.Id, false);

        MemberInfo[] fields = tarefaCadastrada.GetType().GetMembers();

        foreach (var field in fields)
        {
            if (field is PropertyInfo property)
            {
                if (property.Name.Equals("Prioridade"))
                    continue;

                valorAntigo = property.GetValue(tarefaCadastrada);
                valorNovo = property.GetValue(tarefa);                    

                if (!Equals(valorAntigo, valorNovo))
                    alteracoes.Add(new HistoricoAlteracao
                    {
                        Campo = field.Name,
                        De = valorAntigo != null ? valorAntigo.ToString() : string.Empty,
                        Para = valorNovo != null ? valorNovo.ToString() : string.Empty,
                        DataAlteracao = DateTime.Now
                    });
            }

        }

        return alteracoes;
    }
}
