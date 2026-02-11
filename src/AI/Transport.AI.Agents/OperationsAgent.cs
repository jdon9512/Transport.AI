using System.Text.Json;
using Transport.AI.Agents.AIClient;
using Transport.AI.Agents.Saga;
using Transport.Shared.Events;

namespace Transport.AI.Agents;

public class OperationsAgent
{
    private readonly IAIClient _ai;

    public OperationsAgent(IAIClient ai)
    {
        _ai = ai;
    }

    public async Task<AgentDecision> AssignTruck(OrderSagaState state)
    {
        var json =  await _ai.CompleteAsync(
                    """
            You manage vehicle fleets.
            Return ONLY valid JSON.
            Schema:
            {
              "success": boolean,
              "summary": string,
              "data": {
                "truckId": GUID,
                "driverId": GUID
              },
              "warnings": string[]
            }
            """,
            $"Assign truck for weight {state.WeightKg}. Within the summary describe the brand, model and year truck");

        return JsonSerializer.Deserialize<AgentDecision>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        )!;
    }
}
