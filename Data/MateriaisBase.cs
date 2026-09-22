using MinhaPrimeiraApi.Models;

namespace MinhaPrimeiraApi.Data;

// Base inicial de materiais. Os valores de Vc são faixas típicas
// de catálogo (ex: Sandvik, Mitsubishi) para fresamento com metal duro.
// Ajuste depois com valores reais do seu processo/ferramenta.
public static class MateriaisBase
{
    public static readonly List<Material> Lista = new()
    {
        new Material(1, "Aço 1020",  "SAE 1020",  DurezaHB: 120, VcMin: 120, VcMax: 200),
        new Material(2, "Aço 1045",  "SAE 1045",  DurezaHB: 170, VcMin: 100, VcMax: 160),
        new Material(3, "Aço 4140",  "SAE 4140",  DurezaHB: 280, VcMin: 60,  VcMax: 110,
            Observacao: "Material temperado, usar avanço reduzido"),
        new Material(4, "Inox 304",  "AISI 304",  DurezaHB: 200, VcMin: 60,  VcMax: 100,
            Observacao: "Alta tendência a empastamento, usar refrigeração"),
        new Material(5, "Alumínio 6061", "ABNT 6061", DurezaHB: 95, VcMin: 300, VcMax: 600),
        new Material(6, "Ferro Fundido Cinzento", "FC-25", DurezaHB: 210, VcMin: 80, VcMax: 150),
    };

    public static Material? BuscarPorId(int id) =>
        Lista.FirstOrDefault(m => m.Id == id);
}
