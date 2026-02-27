using KatarinaAI.Agent.Application.Interfaces;
using KatarinaAI.Agent.Application.Requests;
using KatarinaAI.Agent.Application.Responses;
using System.Net.Sockets;
using System.Text.Json;

namespace KatarinaAI.Agent.Application.UserCases
{
    public class GenerateTicketUseCase
    {
        private readonly ILLMProvider _llmProvider;

        public GenerateTicketUseCase(ILLMProvider llmProvider)
        {
            _llmProvider = llmProvider;
        }

        public async Task<GenerateTicketResponse> ExecuteAsync(
            GenerateTicketRequest request)
        {
            var prompt = BuildPrompt(request.Requirement);

            var llmRequest = new LLMRequest(prompt);

            var llmResponse = await _llmProvider.GenerateAsync(llmRequest);

            return await TryDeserializeWithRetry(llmResponse.RawContent, prompt);
        }

        private async Task<GenerateTicketResponse> TryDeserializeWithRetry(
            string content,
            string originalPrompt)
        {
            try
            {
                var ticket = JsonSerializer.Deserialize<GenerateTicketResponse>(
                    content,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (ticket is null)
                    throw new Exception("Deserialized object is null.");

                return ticket;
            }
            catch (Exception ex)
            {
                // Retry 1x com instrução de correção

                var correctionPrompt = $$"""
                    The previous response was invalid JSON and could not be parsed.

                    Error:
                    {{ex.Message}}

                    Previous response:
                    {{content}}

                    Return ONLY a valid JSON object that strictly follows the required schema.
                    Do not include explanations.
                    Do not include markdown.
                    Do not include additional fields.
                    Ensure it starts with '{' and ends with '}'.
                    """;

                var retryResponse = await _llmProvider.GenerateAsync(
                    new LLMRequest(originalPrompt + "\n\n" + correctionPrompt));

                try
                {
                    var correctedTicket = JsonSerializer.Deserialize<GenerateTicketResponse>(
                        retryResponse.RawContent,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                    if (correctedTicket is null)
                        throw new Exception("Retry returned null object.");

                    return correctedTicket;
                }
                catch
                {
                    throw new InvalidOperationException(
                        "LLM returned invalid JSON after retry.");
                }
            }
        }

        private string BuildPrompt(string requirement)
        {
            return $$"""
                You are a Technical Product Owner AI.

                Your task is to convert a short requirement into a structured technical ticket.

                IMPORTANT RULES:
                - Return ONLY a valid JSON object.
                - The response MUST start with '{' and end with '}'.
                - Do NOT include markdown.
                - Do NOT include explanations.
                - Do NOT include text before or after the JSON.
                - Do NOT include additional fields.
                - Ensure the JSON is syntactically valid and can be parsed.

                The JSON format MUST be exactly:

                {
                  "title": "string",
                  "objective": "string",
                  "description": "string",
                  "acceptanceCriteria": ["string"],
                  "impactedComponents": ["string"],
                  "suggestedMethods": ["string"]
                }

                Rules for content:
                - acceptanceCriteria must contain specific, testable statements.
                - impactedComponents must reference backend components, services, repositories, or controllers.
                - suggestedMethods must reference implementation-level guidance.

                Requirement:
                {{requirement}}
            """;
        }
    }
}
