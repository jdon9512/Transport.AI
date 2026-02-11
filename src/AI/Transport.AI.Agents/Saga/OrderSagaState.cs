namespace Transport.AI.Agents.Saga
{
    public class OrderSagaState
    {
        public Guid OrderId { get; set; }

        public string Origin { get; set; } = "armenia, quindio";
        public string Destination { get; set; } = "pereira, risaralda";
        public string CargoDescription { get; set; } = "directo";
        public decimal WeightKg { get; set; } = 1000;

        public AgentDecision? Logistics { get; set; }
        public AgentDecision? Operations { get; set; }
        public AgentDecision? Finance { get; set; }

        public bool Approved { get; set; }
        public List<string> RejectionReasons { get; set; } = new();
    }

}
