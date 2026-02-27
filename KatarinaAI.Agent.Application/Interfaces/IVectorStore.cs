using KatarinaAI.Agent.Domain.Models;

namespace KatarinaAI.Agent.Application.Interfaces
{
    public interface IVectorStore
    {
        Task AddAsync(IndexedComponent component);
        Task<List<IndexedComponent>> SearchAsync(List<float> embedding, int topK);
    }
}
