namespace Transport.AI.Agents;

public class AgentDecision
{
    public bool Success { get; set; }
    public string Summary { get; set; }
    public Dictionary<string, string> Data { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
}