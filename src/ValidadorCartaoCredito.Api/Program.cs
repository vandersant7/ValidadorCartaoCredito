using Microsoft.OpenApi.Models;
using ValidarCartaoCredito.Core.Services;
using ValidadorCartaoCredito.Core.Models;
using ValidadorCartaoCredito.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidarCartaoService, ValidarCartaoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Documentação da API";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.MapGet("/", () => "API rodando com Swagger!");

app.MapPost("/api/validar-cartao",
    (CartaoRequest request, IValidarCartaoService service) =>
    {
        var resultado = service.Validar(request.NumeroCartao);
        return Results.Ok(resultado);
    })
    .WithName("ValidarCartao").WithOpenApi(operation => 
    {
        operation.Summary = "Valida um número de cartão de crédito.";
        operation.Description = "Recebe um número de cartão de crédito e retorna informações sobre sua validade e tipo.";
        operation.Tags = new List<OpenApiTag>
        {
            new OpenApiTag { Name = "Validação de Cartão" }
        };
        return operation;
    });
   


app.Run();

