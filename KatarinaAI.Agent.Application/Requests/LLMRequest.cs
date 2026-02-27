namespace KatarinaAI.Agent.Application.Requests
{
    public class LLMRequest
    {
        public LLMRequest(string userPrompt) 
        {
            UserPrompt = userPrompt;
        }

        public string SystemPrompt { get; init; } = string.Empty;

        public string UserPrompt { get; init; } = string.Empty;

        /// <summary>
        /// Optional additional context (future RAG support).
        /// </summary>
        public string? Context { get; init; }

        /// <summary>
        /// Controls randomness (0 = deterministic).
        /// </summary>
        public double Temperature { get; init; } = 0.2;

        /// <summary>
        /// Maximum tokens allowed in the response.
        /// </summary>
        public int MaxTokens { get; init; } = 1500;

        /// <summary>
        /// If true, provider should enforce structured JSON output.
        /// </summary>
        public bool RequireJsonResponse { get; init; } = true;

        /// <summary>
        /// Optional JSON schema description for structured output.
        /// </summary>
        public string? JsonSchema { get; init; }
    }
}
