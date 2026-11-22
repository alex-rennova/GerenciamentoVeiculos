namespace GerenciamentoVeiculos.Domain.Exceptions;

/// <summary>
/// Exceção base para erros de regras de negócio ou domínio.
/// </summary>
public abstract class DominioException : Exception
{
    protected DominioException(string mensagem) : base(mensagem)
    {
    }
}

/// <summary>
/// Lançada quando um recurso solicitado não é encontrado (mapeia para HTTP 404).
/// </summary>
public class RecursoNaoEncontradoException : DominioException
{
    public RecursoNaoEncontradoException(string mensagem) : base(mensagem)
    {
    }
}

/// <summary>
/// Lançada quando uma regra de negócio é violada (ex: tentar excluir proprietário com veículos - mapeia para HTTP 409 Conflito).
/// </summary>
public class ConflitoException : DominioException
{
    public ConflitoException(string mensagem) : base(mensagem)
    {
    }
}

/// <summary>
/// Lançada para violações gerais de validação de negócio (mapeia para HTTP 400 Bad Request).
/// </summary>
public class RegraNegocioException : DominioException
{
    public RegraNegocioException(string mensagem) : base(mensagem)
    {
    }
}
