namespace ValidadorCartaoCredito.Core.Models;

public class CartaoResponse
{
    public string Tipo { get; set; } = "Desconhecido";
    public bool Valido { get; set; }
    public bool ValidoLuhn { get; set; }
    public string Mensagem { get; set; } = string.Empty;
}
