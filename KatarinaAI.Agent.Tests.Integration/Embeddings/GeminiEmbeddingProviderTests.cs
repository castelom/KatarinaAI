using FluentAssertions;
using KatarinaAI.Agent.Application.Interfaces;
using KatarinaAI.Agent.Domain.Models;
using KatarinaAI.Agent.Tests.Integration.Fixtures;
using Xunit;

namespace KatarinaAI.Agent.Tests.Integration.Embeddings
{
    public class GeminiEmbeddingProviderTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly IEmbeddingProvider _embeddingProvider;
        private readonly IVectorStore _vectorStore;

        public GeminiEmbeddingProviderTests(
            IntegrationTestFixture fixture)
        {
            _embeddingProvider = fixture.EmbeddingProvider;
            _vectorStore = fixture.VectorStore;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Should_Generate_Embeddings_And_Find_Similar_Component()
        {
            // Arrange
            var embedding1 = await _embeddingProvider
                .GenerateEmbeddingAsync("UserController");

            var embedding2 = await _embeddingProvider
                .GenerateEmbeddingAsync("Reset password");

            await _vectorStore.AddAsync(new IndexedComponent
            {
                ComponentName = "UserController",
                Embedding = embedding1
            });

            // Act
            var results = await _vectorStore
                .SearchAsync(embedding2, 5);

            // Assert
            results.Should().NotBeNull();
            results.Should().NotBeEmpty();
            results.First().ComponentName
                .Should().Be("UserController");
        }
    }
}
