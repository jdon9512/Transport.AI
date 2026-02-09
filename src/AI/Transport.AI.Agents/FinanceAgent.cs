using Transport.Shared.Events;

namespace Transport.AI.Agents;

public class FinanceAgent
{
    private readonly OpenAIClient _ai;

    public FinanceAgent(OpenAIClient ai)
    {
        _ai = ai;
    }

    public async Task<string> CalculateCost(OrderSagaState state)
    {
        return await _ai.CompleteAsync(
            "You calculate logistics costs",
            $"Estimate cost for route {state.Origin} to {state.Destination}");
    }
}
