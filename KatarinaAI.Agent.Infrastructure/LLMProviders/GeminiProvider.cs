using Google.GenAI;
using KatarinaAI.Agent.Application.Interfaces;
using KatarinaAI.Agent.Application.Requests;
using KatarinaAI.Agent.Application.Responses;
using KatarinaAI.Agent.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KatarinaAI.Agent.Infrastructure.LLMProviders
{
    public class GeminiProvider : ILLMProvider
    {
        private readonly Client _client;
        private readonly GeminiOptions _options;

        public GeminiProvider(
            Client client,
            IOptions<GeminiOptions> options)
        {
            _client = client;
            _options = options.Value;
        }

        public async Task<LLMResponse> GenerateAsync(LLMRequest request)
        {
            var response = await _client.Models.GenerateContentAsync(
                model: _options.Model,
                contents: request.UserPrompt);



            return new LLMResponse(response.Candidates?[0].Content?.Parts?[0].Text!);

        }
    }
}
