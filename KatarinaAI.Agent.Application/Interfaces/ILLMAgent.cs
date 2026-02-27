using KatarinaAI.Agent.Application.Responses;

namespace KatarinaAI.Agent.Application.Interfaces
{
    public interface ILLMAgent
    {
        Task<LLMTicketResponse> GenerateTicketAsync(string requirement);
    }
}
