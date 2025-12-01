namespace ValidorCartaoCredito.Core.Validators.RegrasCartao;

public static class RegrasDeCartoesRegex
{
    public static readonly List<RegraCartao> Lista = new()
    {
        // ======== BANDEIRAS INTERNACIONAIS PRINCIPAIS ========

        new RegraCartao
        {
            Nome = "American Express",
            PadraoRegex = @"^3[47]\d{13}$" // 15 dígitos
        },
        new RegraCartao
        {
            Nome = "Diners Club",
            PadraoRegex = @"^3(0[0-5]|[68]\d)\d{11}$" // 14 dígitos
        },
        new RegraCartao
        {
            Nome = "JCB",
            PadraoRegex = @"^35(2[89]|[3-8]\d)\d{12}$" // 16 dígitos
        },
        new RegraCartao
        {
            Nome = "Discover",
            PadraoRegex = @"^(6011\d{12}|65\d{14}|64[4-9]\d{13}|622(12[6-9]|1[3-9]\d|[2-8]\d{2}|9[0-1]\d|92[0-5])\d{10})$"
        },
        new RegraCartao
        {
            Nome = "Visa",
            PadraoRegex = @"^4\d{12}(\d{3})?(\d{3})?$" // 13,16,19 dígitos
        },
        new RegraCartao
        {
            Nome = "Mastercard",
            PadraoRegex = @"^(5[1-5]\d{14}|2(2[2-9]\d{12}|[3-6]\d{13}|7[01]\d{12}|720\d{12}))$"
        },

        // ======== BANDEIRAS BRASILEIRAS ========

        new RegraCartao
        {
            Nome = "Elo",
            PadraoRegex = @"^(40117[89]\d{10}|431274\d{10}|438935\d{10}|451416\d{10}|457393\d{10}|45763?\d{10}|504175\d{10}|5067\d{12}|5068\d{12}|5069\d{12}|509\d{13}|627780\d{10}|636297\d{10}|636368\d{10})$"
        },
        new RegraCartao
        {
            Nome = "Hipercard",
            PadraoRegex = @"^(606282\d{10,13}|3841[046]\d{11})$" // 16 ou 19 dígitos
        },
        new RegraCartao
        {
            Nome = "Hiper",
            PadraoRegex = @"^(637095|637568|637599|637609|637612)\d{10,13}$"
        },
        new RegraCartao
        {
            Nome = "Aura",
            PadraoRegex = @"^50\d{14,17}$"
        },

        // ======== BANDEIRAS ADICIONAIS INTERNACIONAIS ========

        new RegraCartao
        {
            Nome = "Maestro",
            PadraoRegex = @"^(5[06789]\d{14,17}|6\d{15,18})$"
        },
        new RegraCartao
        {
            Nome = "Visa Electron",
            PadraoRegex = @"^(4026\d{12}|417500\d{10}|4508\d{12}|4844\d{12}|4913\d{12}|4917\d{12})$"
        },
        new RegraCartao
        {
            Nome = "UnionPay",
            PadraoRegex = @"^62\d{14,17}$"
        },

        // ======== BINS ESPECIAIS ========

        new RegraCartao
        {
            Nome = "Nubank",
            PadraoRegex = @"^(515590|516230|536338|554176|554175)\d{10}$"
        },

        // ======== BANDEIRA PARA AMBIENTE DE TESTE ========

        new RegraCartao
        {
            Nome = "Teste",
            PadraoRegex = @"^9999\d{12}$"
        }
    };
}
