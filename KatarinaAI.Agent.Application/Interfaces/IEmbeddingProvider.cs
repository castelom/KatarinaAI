namespace KatarinaAI.Agent.Application.Interfaces
{
    public interface IEmbeddingProvider
    {
        Task<List<float>> GenerateEmbeddingAsync(string text);
    }
}
