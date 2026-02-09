using Transport.AI.Agents.common;

namespace Transport.AI.Orchestrator.Saga;

public class OrderSagaState
{
    public Guid OrderId { get; set; }

    public AgentDecision? Logistics { get; set; }
    public AgentDecision? Operations { get; set; }
    public AgentDecision? Finance { get; set; }

    public bool Approved { get; set; }
    public List<string> RejectionReasons { get; set; } = new();
}
