namespace MinhaPrimeiraApi.Models;

// Representa um material usinável, com a dureza e a faixa de
// velocidade de corte (Vc) recomendada pelos fabricantes de ferramenta.
public record Material(
    int Id,
    string Nome,           // Ex: "Aço 1045"
    string Norma,          // Ex: "SAE 1045" (opcional, útil pra exibir)
    double DurezaHB,        // Dureza Brinell (referência mais comum em catálogo)
    double VcMin,           // m/min - limite inferior recomendado
    double VcMax,           // m/min - limite superior recomendado
    string Observacao = ""  // Ex: "Usar refrigeração abundante"
)
{
    // Vc "sugerida" = meio da faixa. Serve de ponto de partida pro usuário.
    public double VcRecomendado => Math.Round((VcMin + VcMax) / 2, 1);
}
