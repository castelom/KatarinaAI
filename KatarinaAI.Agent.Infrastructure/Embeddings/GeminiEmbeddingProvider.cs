using Google.GenAI;
using KatarinaAI.Agent.Application.Interfaces;

namespace KatarinaAI.Agent.Infrastructure.Embeddings
{
    public class GeminiEmbeddingProvider : IEmbeddingProvider
    {
        private readonly Client _client;

        public GeminiEmbeddingProvider(Client client)
        {
            _client = client;
        }

        public async Task<List<float>> GenerateEmbeddingAsync(string text)
        {
            var response = await _client.Models.EmbedContentAsync(
                model: "text-embedding-004",
                contents: text);

            var embeddings = response?.Embeddings;
            if (embeddings == null || embeddings.Count == 0)
            {
                return new List<float>();
            }

            var first = embeddings[0];
            var values = first?.Values;
            if (values == null || values.Count == 0)
            {
                return new List<float>();
            }

            return values.Select(v => (float)v).ToList();
        }
    }
}
