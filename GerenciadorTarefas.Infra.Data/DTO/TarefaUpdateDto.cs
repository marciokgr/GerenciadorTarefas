using GerenciadorTarefas.Domain.Enum;

namespace GerenciadorTarefas.Infra.Data.DTO;

public class TarefaUpdateDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public PrioridadeTarefaEnum? Prioridade { get; set; }
    public TarefaStatusEnum Status { get; set; }
    public string? Comentario { get; set; }
    public DateTime DataVencimento { get; set; }
}
