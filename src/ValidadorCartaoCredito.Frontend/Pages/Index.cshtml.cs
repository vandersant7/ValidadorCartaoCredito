using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ValidadorCartaoCredito.Core.Models;
using ValidadorCartaoCredito.Frontend.Models;

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

    [BindProperty(SupportsGet = true)]
    public string NumeroCartao { get; set; } = string.Empty;

    public CartaoResponse? Resultado { get; set; }
    public bool IsProcessing { get; set; } = false;
    public string? StatusMessage { get; set; }

    public AlertViewModel? AlertModel { get; set; }

    public async Task OnPostAsync()
    {
        IsProcessing = true;
        StatusMessage = "Validando cartão, aguarde...";

        if (string.IsNullOrWhiteSpace(NumeroCartao))
        {
            Resultado = new CartaoResponse { Valido = false, Mensagem = "Digite o número do cartão." };
            MontarAlerta(Resultado, defaultMessage: "Digite o número do cartão.");
            IsProcessing = false;
            return;
        }

        try
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var request = new CartaoRequest { NumeroCartao = NumeroCartao.Trim() };

            var response = await client.PostAsJsonAsync("/api/validar-cartao", request);

            if (response.IsSuccessStatusCode)
            {
                Resultado = await response.Content.ReadFromJsonAsync<CartaoResponse>();
            }
            else
            {
                Resultado = new CartaoResponse { Valido = false, Mensagem = "Não foi possível validar o cartão. Tente novamente mais tarde." };
            }

        }
        catch (Exception)
        {
            Resultado = new CartaoResponse 
            { 
                Valido = false, 
                Mensagem = "Erro de comunicação com o servidor. Verifique sua conexão." 
            };
        }
       

        MontarAlerta(Resultado);
        IsProcessing = false;
    }

    private void MontarAlerta(CartaoResponse resultado, string? defaultMessage = null)
    {
        var successTimeout = _configuration.GetValue<int>("AlertSettings:SuccessTimeout", 4000);
        var errorTimeout = _configuration.GetValue<int>("AlertSettings:ErrorTimeout", 4000);

        var valido = resultado.Valido;
        var titulo = valido ? "Cartão VÁLIDO" : "Cartão INVÁLIDO";
        var css = valido ? "alert-success" : "alert-danger";
        var msg = !string.IsNullOrWhiteSpace(resultado.Mensagem)
                     ? resultado.Mensagem
                     : (defaultMessage ?? (valido ? "Cartão validado com sucesso!" : "Erro ao validar cartão"));

        AlertModel = new AlertViewModel
        {
            Title = titulo,
            Message = msg,
            CssClass = css,
            Timeout = valido ? successTimeout : errorTimeout,
            Dismissible = true
        };

        StatusMessage = msg;
    }
}
