namespace KatarinaAI.Agent.Application.Responses
{
    public class LLMResponse
    {
        public LLMResponse(string content) 
        {
            RawContent = content;
        }
        /// <summary>
        /// Raw text returned by the provider.
        /// </summary>
        public string RawContent { get; init; } = string.Empty;

        /// <summary>
        /// If structured output was requested, parsed JSON.
        /// </summary>
        public string? JsonContent { get; init; }

        /// <summary>
        /// Indicates whether the provider successfully returned structured data.
        /// </summary>
        public bool IsStructured { get; init; }

        /// <summary>
        /// Token usage metadata (optional, provider dependent).
        /// </summary>
        public int? PromptTokens { get; init; }

        public int? CompletionTokens { get; init; }

        public string? ModelUsed { get; init; }
    }
}
