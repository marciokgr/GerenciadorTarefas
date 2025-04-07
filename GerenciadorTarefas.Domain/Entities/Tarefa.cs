using System.ComponentModel.DataAnnotations.Schema;
using GerenciadorTarefas.Domain.Enum;

namespace GerenciadorTarefas.Domain.Entities;

public class Tarefa
{
    [Column("TarefaId")]
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataVencimento { get; set; }
    public TarefaStatusEnum Status { get; set; }
    public PrioridadeTarefaEnum? Prioridade { get; set; }
    public string? Comentario { get; set; }

    public Guid ProjetoId { get; set; }
    public Projeto Projeto { get; set; }

    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public ICollection<HistoricoAlteracao> HistoricoAlteracoes { get; set; }
}

