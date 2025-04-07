using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciadorTarefas.Domain.Entities;

public class HistoricoAlteracao
{
    [Column("HistoricoAlteracaoId")]
    public Guid Id { get; set; }
    public string Campo { get; set; }
    public string De { get; set; }
    public string Para { get; set; }
    public DateTime DataAlteracao { get; set; }

    public Guid TarefaId { get; set; }

    public Tarefa Tarefa { get; set; }
}
