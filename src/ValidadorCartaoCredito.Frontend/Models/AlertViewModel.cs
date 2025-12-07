namespace ValidadorCartaoCredito.Frontend.Models
{
    public class AlertViewModel
    {
        public string Title { get; set; } = string.Empty;      // Ex.: "Cartão VÁLIDO" ou "Cartão INVÁLIDO"
        public string Message { get; set; } = string.Empty;     // Ex.: "Cartão validado com sucesso!"
        public string CssClass { get; set; } = "alert-info";    // Ex.: "alert-success" ou "alert-danger"
        public int Timeout { get; set; } = 4000;                // Millisegundos para auto-fechamento
        public bool Dismissible { get; set; } = true;
    }
}
