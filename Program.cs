using MinhaPrimeiraApi.Data;
using MinhaPrimeiraApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Libera o front (mobile/PC) rodando em outro endereço/porta a consumir a API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors();

app.MapGet("/api/ola", () =>
{
    return "Minha primeira API em C# está funcionando!";
});

// Lista os materiais disponíveis, com dureza e Vc recomendada.
// O front usa isso pra montar o seletor de material.
app.MapGet("/api/materiais", () =>
{
    return MateriaisBase.Lista.Select(m => new
    {
        m.Id,
        m.Nome,
        m.Norma,
        m.DurezaHB,
        m.VcMin,
        m.VcMax,
        m.VcRecomendado,
        m.Observacao
    });
})
.WithName("ListarMateriais");

// Cálculo de usinagem. Se MaterialId for informado e Vc não for
// informado (ou vier 0), a Vc recomendada do material é usada automaticamente.
app.MapPost("/api/usinagem/calcular", (UsinagemRequest dados) =>
{
    Material? material = dados.MaterialId.HasValue
        ? MateriaisBase.BuscarPorId(dados.MaterialId.Value)
        : null;

    if (dados.MaterialId.HasValue && material is null)
    {
        return Results.NotFound(new { erro = $"Material {dados.MaterialId} não encontrado." });
    }

    var vcUtilizada = dados.Vc > 0
        ? dados.Vc
        : material?.VcRecomendado
            ?? throw new ArgumentException("Informe Vc manualmente ou um MaterialId válido.");

    var rpm = (vcUtilizada * 1000) / (Math.PI * dados.Diametro);
    var avanco = dados.NumeroDentes * dados.Fz * rpm;

    return Results.Ok(new
    {
        dados.Diametro,
        dados.NumeroDentes,
        dados.Fz,
        VcUtilizada = vcUtilizada,
        Material = material?.Nome,
        MaterialObservacao = material?.Observacao,
        Rpm = Math.Round(rpm),
        Avanco = Math.Round(avanco)
    });
})
.WithName("CalcularUsinagem");

app.Run();

// MaterialId é opcional: se vier, e Vc não vier, a Vc do material é usada.
record UsinagemRequest(
    double Diametro,
    int NumeroDentes,
    double Fz,
    double Vc = 0,
    int? MaterialId = null
);
