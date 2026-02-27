namespace KatarinaAI.Agent.Infrastructure.Configuration
{
    public class ProjectSettings
    {
        public string Name { get; set; } = string.Empty;
        public List<RepositorySettings> Repositories { get; set; } = new List<RepositorySettings>();
    }
}
