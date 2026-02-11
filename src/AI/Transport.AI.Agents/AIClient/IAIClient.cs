namespace Transport.AI.Agents.AIClient;

public interface IAIClient
{
    public Task<string> CompleteAsync(string system, string user);
}
