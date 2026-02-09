using System.Text.Json;
using Transport.AI.Agents.common;
using Transport.Shared.Events;

namespace Transport.AI.Agents;

public class LogisticsAgent
{
    private readonly OpenAIClient _ai;

    public LogisticsAgent(OpenAIClient ai)
    {
        _ai = ai;
    }

    public async Task<AgentDecision> CalculateRoute(OrderSagaState state)
    {
        var json = await _ai.CompleteAsync(
        """
            You are a logistics expert.
            Return ONLY valid JSON.
            Schema:
            {
              "success": boolean,
              "summary": string,
              "data": {
                "route": string,
                "distance_km": string
              },
              "warnings": string[]
            }
            """,
                    $"""
            Origin: {state.Origin}
            Destination: {state.Destination}
            """
        );

        return JsonSerializer.Deserialize<AgentDecision>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        )!;
    }
}
