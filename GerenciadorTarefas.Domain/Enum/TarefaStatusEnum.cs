using System.ComponentModel;

namespace GerenciadorTarefas.Domain.Enum;

public enum TarefaStatusEnum
{
    [Description("Pendente")]
    Pendente = 1,

    [Description("Andamento")]
    Andamento = 2,

    [Description("Concluido")]
    Concluido = 3
}
