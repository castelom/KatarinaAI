namespace KatarinaAI.Agent.Domain.Models
{
    public class IndexedComponent
    {
        public string RepositoryName { get; set; }
        public string ComponentName { get; set; }
        public string FilePath { get; set; }
        public string Type { get; set; }
        public List<float> Embedding { get; set; }
    }
}
