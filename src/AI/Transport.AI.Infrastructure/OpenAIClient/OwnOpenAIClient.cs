using Transport.AI.Agents.AIClient;
using OpenAI.Chat;

namespace Transport.AI.Infrastructure.OpenAIClient;

public class OwnOpenAIClient : IAIClient
{
    private readonly ChatClient _chatClient;

    public OwnOpenAIClient(ChatClient chatClient)
    {
        _chatClient = chatClient;
    }

    public async Task<string> CompleteAsync(string system, string user)
    {
        var messages = new List<ChatMessage>
        {
            ChatMessage.CreateSystemMessage(system),
            ChatMessage.CreateUserMessage(user)
        };

        ChatCompletion completion = await _chatClient.CompleteChatAsync(messages);
        return completion.Content[0].Text;
    }
}
