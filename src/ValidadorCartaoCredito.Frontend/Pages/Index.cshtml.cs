using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ValidadorCartaoCredito.Core.Models;

namespace ValidadorCartaoCredito.Frontend.Pages;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // ESSAS DUAS LINHAS SÃO OBRIGATÓRIAS – com SupportsGet = true
    [BindProperty(SupportsGet = true)]
    public string NumeroCartao { get; set; } = string.Empty;

    public CartaoResponse? Resultado { get; set; }
    public string? AlertClass { get; set; }

    public async Task OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(NumeroCartao))
        {
            Resultado = new CartaoResponse { Valido = false, Mensagem = "Digite o número do cartão." };
            AlertClass = "alert-danger";
            return;
        }

        var client = _httpClientFactory.CreateClient("ApiClient");
        var request = new CartaoRequest { NumeroCartao = NumeroCartao.Trim() };

        var response = await client.PostAsJsonAsync("/api/validar-cartao", request);

        Resultado = response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<CartaoResponse>()
            : new CartaoResponse { Valido = false, Mensagem = "Erro na API" };

        AlertClass = Resultado.Valido ? "alert-success" : "alert-danger";
    }
}