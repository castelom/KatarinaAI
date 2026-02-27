using KatarinaAI.Agent.Application.Interfaces;
using KatarinaAI.Agent.Application.Responses;

namespace KatarinaAI.Agent.Infrastructure.Agents
{
    public class TicketGenerationAgent : ILLMAgent
    {
        public Task<LLMTicketResponse> GenerateTicketAsync(string requirement)
        {
            throw new NotImplementedException();
        }
    }
}
