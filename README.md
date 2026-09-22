# MinhaPrimeiraApi

API em C#/.NET (ASP.NET Core) para cálculos de usinagem — rotação (RPM) e avanço — com uma base de materiais que já considera dureza e faixa de velocidade de corte recomendada.

Projeto criado para aplicar no dia a dia como programador CNC o que venho estudando em Análise e Desenvolvimento de Sistemas (ADS).

## O que a API faz

- Calcula **RPM** a partir da velocidade de corte (Vc) e do diâmetro da ferramenta.
- Calcula o **avanço** (mm/min) a partir do número de dentes, Fz (avanço por dente) e RPM.
- Traz uma base de **materiais** (aço 1020, 1045, 4140, inox 304, alumínio 6061, ferro fundido cinzento) com dureza (HB) e faixa de Vc recomendada.
- Se você informar o material e não informar a Vc manualmente, a API já usa a Vc recomendada daquele material.

## Tecnologias

- C# / .NET (ASP.NET Core, Minimal APIs)
- OpenAPI (documentação automática dos endpoints)

## Endpoints

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/ola` | Testa se a API está no ar |
| GET | `/api/materiais` | Lista os materiais disponíveis, com dureza e Vc recomendada |
| POST | `/api/usinagem/calcular` | Calcula RPM e avanço a partir dos parâmetros informados |

### Exemplo de requisição — `POST /api/usinagem/calcular`

```json
{
  "diametro": 10,
  "numeroDentes": 2,
  "fz": 0.05,
  "materialId": 2
}
```

Se `vc` não for informado, a API usa a Vc recomendada do material escolhido. Também é possível informar `vc` manualmente, sem passar `materialId`.

## Como rodar localmente

```bash
git clone https://github.com/ruangomesgg/MinhaPrimeiraApi.git
cd MinhaPrimeiraApi
dotnet run
```

A API sobe em `http://localhost:5046` (a porta pode variar — confira no terminal).

## Calculadora web

Também criei uma interface web responsiva (PC e celular) para usar os cálculos sem precisar chamar a API diretamente:

🔗 [Calculadora de usinagem](https://claude.ai/artifact/39uPeCXUAjwmdEd84qCPoE)

## Próximos passos

- Adicionar parâmetros de profundidade/largura de corte (Ap/Ae) e material/revestimento da ferramenta.
- Persistir os materiais em banco de dados em vez de lista fixa em memória.
- Conectar a calculadora web diretamente na API (hoje ela roda com os cálculos replicados em JavaScript).
- Deploy da API em um serviço com URL pública.

## Autor

Ruan Gomes — programador CNC, em formação em Análise e Desenvolvimento de Sistemas.
