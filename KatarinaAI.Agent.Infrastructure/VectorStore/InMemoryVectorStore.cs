using KatarinaAI.Agent.Application.Interfaces;
using KatarinaAI.Agent.Domain.Models;

namespace KatarinaAI.Agent.Infrastructure.VectorStore
{
    public class InMemoryVectorStore : IVectorStore
    {
        private readonly List<IndexedComponent> _components = new();

        public Task AddAsync(IndexedComponent component)
        {
            _components.Add(component);
            return Task.CompletedTask;
        }

        public Task<List<IndexedComponent>> SearchAsync(
            List<float> embedding,
            int topK)
        {
            var scored = _components
                .Select(c => new
                {
                    Component = c,
                    Score = CosineSimilarity(embedding, c.Embedding)
                })
                .OrderByDescending(x => x.Score)
                .Take(topK)
                .Select(x => x.Component)
                .ToList();

            return Task.FromResult(scored);
        }

        private double CosineSimilarity(List<float> v1, List<float> v2)
        {
            double dot = 0;
            double normA = 0;
            double normB = 0;

            for (int i = 0; i < v1.Count; i++)
            {
                dot += v1[i] * v2[i];
                normA += v1[i] * v1[i];
                normB += v2[i] * v2[i];
            }

            return dot / (Math.Sqrt(normA) * Math.Sqrt(normB));
        }
    }
}
