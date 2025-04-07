
namespace GerenciadorTarefas.Domain.Exceptions;

public class OperacaoNaoPermitidaException : Exception
{
    public OperacaoNaoPermitidaException()
    {
        
    }

    public OperacaoNaoPermitidaException(string mensagem) : base(mensagem)
    {
        
    }

    public OperacaoNaoPermitidaException(string mensagem, Exception excecaoInterna) : base(mensagem, excecaoInterna)
    {
        
    }
}
