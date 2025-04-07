using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciadorTarefas.Domain.Entities;

public class Usuario
{
    [Column("UsuarioId")]
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public ICollection<Tarefa> Tarefas { get; set; }
}
