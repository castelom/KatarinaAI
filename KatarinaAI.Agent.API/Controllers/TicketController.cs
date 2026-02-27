using KatarinaAI.Agent.Application.Requests;
using KatarinaAI.Agent.Application.Responses;
using KatarinaAI.Agent.Application.UserCases;
using Microsoft.AspNetCore.Mvc;

namespace KatarinaAI.Agent.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly GenerateTicketUseCase _generateTicketUseCase;

        public TicketController(GenerateTicketUseCase generateTicketUseCase)
        {
            _generateTicketUseCase = generateTicketUseCase;
        }

        [HttpPost(Name = "GenerateTicket")]
        public async Task<GenerateTicketResponse> GenerateTicket(GenerateTicketRequest request)
        {
            var response = await _generateTicketUseCase.ExecuteAsync(request);
            return response;
        }
    }
}
