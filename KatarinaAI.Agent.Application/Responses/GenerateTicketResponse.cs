using System.Text.Json.Serialization;

namespace KatarinaAI.Agent.Application.Responses
{
    public class GenerateTicketResponse
    {
        [JsonPropertyName("title")]
        public required string Title { get; init; }

        [JsonPropertyName("objective")]
        public required string Objective { get; init; }

        [JsonPropertyName("description")]
        public required string Description { get; init; }

        [JsonPropertyName("acceptanceCriteria")]
        public required List<string> AcceptanceCriteria { get; init; }

        [JsonPropertyName("impactedComponents")]
        public required List<string> ImpactedComponents { get; init; }

        [JsonPropertyName("suggestedMethods")]
        public required List<string> SuggestedMethods { get; init; }
    }
}
