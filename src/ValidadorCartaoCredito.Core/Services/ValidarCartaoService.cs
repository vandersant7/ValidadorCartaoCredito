using System.Text.RegularExpressions;
using ValidarCartaoCredito.Core.Services;
using ValidadorCartaoCredito.Core.Models;
using ValidadorCartaoCredito.Core.Validators.RegrasCartao;

namespace ValidadorCartaoCredito.Core.Services;

public class ValidarCartaoService : IValidarCartaoService
{
    public CartaoResponse Validar(string numeroCartao)
    {
        var numero = LimparNumero(numeroCartao);

        string tipo = InferirTipoCartao(numero);
        bool validoTipo = tipo != "Desconhecido";

        bool validoLuhn = ValidarLuhn(numero);

        return new CartaoResponse
        {
            Tipo = tipo,
            Valido = validoTipo,
            ValidoLuhn = validoLuhn,
            Mensagem =
                validoTipo && validoLuhn
                    ? "Cartão válido."
                    : !validoTipo
                        ? "Tipo de cartão não identificado."
                        : "Falha na validação Luhn."
        };
    }

    private static string LimparNumero(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            return string.Empty;

        return Regex.Replace(numero, @"\D", "");
    }

    private static string InferirTipoCartao(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            return "Desconhecido";

        foreach (var regra in RegrasDeCartoesRegex.Lista)
        {
            if (!string.IsNullOrEmpty(regra.PadraoRegex) && Regex.IsMatch(numero, regra.PadraoRegex))
                return regra.Nome ?? "Desconhecido";
        }

        return "Desconhecido";
    }

    private static bool ValidarLuhn(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            return false;

        numero = new string(numero.Where(char.IsDigit).ToArray());

        int soma = 0;
        bool dobrar = false;

        // Percorre da direita para esquerda
        for (int i = numero.Length - 1; i >= 0; i--)
        {
            int digito = numero[i] - '0';

            if (dobrar)
            {
                digito *= 2;
                if (digito > 9)
                    digito -= 9;
            }

            soma += digito;
            dobrar = !dobrar;
        }

        return soma % 10 == 0;
    }

}
