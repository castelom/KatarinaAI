using KatarinaAI.Agent.Application.Requests;
using KatarinaAI.Agent.Application.Responses;

namespace KatarinaAI.Agent.Application.Interfaces
{
    public interface ILLMProvider
    {
        Task<LLMResponse> GenerateAsync(LLMRequest request);
    }
}
