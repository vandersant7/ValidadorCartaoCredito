using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ValidadorCartaoCredito.Core.Models;

namespace ValidadorCartaoCredito.Frontend.Pages;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public IndexModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    // ESSAS DUAS LINHAS SÃO OBRIGATÓRIAS – com SupportsGet = true
    [BindProperty(SupportsGet = true)]
    public string NumeroCartao { get; set; } = string.Empty;
    public CartaoResponse? Resultado { get; set; }
    public string? AlertClass { get; set; }
    public bool IsProcessing { get; set; } = false;
    public string? StatusMessage { get; set; }
    public int AlertTimeout { get; set; } = 4000;


    public async Task OnPostAsync()
    {
        IsProcessing = true;
        StatusMessage = "Validando cartão, aguarde...";

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

        var successTimeout = _configuration.GetValue<int>("AlertSettings:SuccessTimeout");
        var errorTimeout = _configuration.GetValue<int>("AlertSettings:ErrorTimeout");

        AlertClass = Resultado.Valido ? "alert-success" : "alert-danger";
        StatusMessage = Resultado.Valido ? "Cartão validado com sucesso!" : 
            Resultado.Mensagem ?? "Erro ao validar cartão";
        AlertTimeout = Resultado.Valido ? successTimeout : errorTimeout;
        IsProcessing = false;
    }
}