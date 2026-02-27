using Google.GenAI;
using KatarinaAI.Agent.Application.Interfaces;
using KatarinaAI.Agent.Infrastructure.Embeddings;
using KatarinaAI.Agent.Infrastructure.VectorStore;
using Microsoft.Extensions.Configuration;

namespace KatarinaAI.Agent.Tests.Integration.Fixtures
{
    public class IntegrationTestFixture
    {
        public IEmbeddingProvider EmbeddingProvider { get; }
        public IVectorStore VectorStore { get; }

        public IntegrationTestFixture()
        {
            // Carrega configuração incluindo User Secrets
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<IntegrationTestFixture>() // Usa o UserSecretsId do projeto de testes
                .Build();

            var apiKey = configuration["Gemini:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("Gemini:ApiKey user secret is not set.");

            Environment.SetEnvironmentVariable("GOOGLE_API_KEY", apiKey);

            var client = new Client();

            EmbeddingProvider = new GeminiEmbeddingProvider(client);
            VectorStore = new InMemoryVectorStore();
        }
    }
}