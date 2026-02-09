using Transport.Shared.Events;

namespace Transport.AI.Agents;

public class OperationsAgent
{
    private readonly OpenAIClient _ai;

    public OperationsAgent(OpenAIClient ai)
    {
        _ai = ai;
    }

    public async Task<string> AssignTruck(OrderSagaState state)
    {
        return await _ai.CompleteAsync(
            "You manage vehicle fleets",
            $"Assign truck for weight {state.Weight}");
    }
}
