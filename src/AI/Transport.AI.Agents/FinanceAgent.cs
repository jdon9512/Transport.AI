using System.Text.Json;
using Transport.AI.Agents.AIClient;
using Transport.AI.Agents.Saga;

namespace Transport.AI.Agents;

public class FinanceAgent
{
    private readonly IAIClient _ai;

    public FinanceAgent(IAIClient ai)
    {
        _ai = ai;
    }

    public async Task<AgentDecision> CalculateCost(OrderSagaState state)
    {;

        var json = await _ai.CompleteAsync(
                                """
            You calculate logistics costs.
            Return ONLY valid JSON.
            Schema:
            {
              "success": boolean,
              "summary": string,
              "data": {
                "costRout": string,
                "costTolls": string,
                "price": string
              },
              "warnings": string[]
            }
            """,
            $"Estimate cost for route {state.Origin} to {state.Destination}. Take in account the tolls, gas, driver's pay, truck's wear and tear from the trip");

        return JsonSerializer.Deserialize<AgentDecision>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        )!;
    }
}
