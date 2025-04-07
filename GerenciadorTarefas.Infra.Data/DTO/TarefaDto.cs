namespace GerenciadorTarefas.Infra.Data.DTO;

public class TarefaDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataVencimento { get; set; }
    public string Status { get; set; }
    public string Prioridade { get; set; }
    public string? Comentario { get; set; }

    public Guid ProjetoId { get; set; }
}
