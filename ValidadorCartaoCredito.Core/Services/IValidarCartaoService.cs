using ValidorCartaoCredito.Core.Models;

namespace ValidarCartaoCredito.Core.Services;

public interface IValidarCartaoService
{
    CartaoResponse Validar(string numero);
}