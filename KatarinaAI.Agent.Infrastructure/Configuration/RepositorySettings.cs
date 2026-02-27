namespace KatarinaAI.Agent.Infrastructure.Configuration
{
    public class RepositorySettings
    {
        public string Name { get; set; }
        public string RepositoryUrl { get; set; }
        public string Branch { get; set; } = "main";
        public string Type { get; set; } // backend | frontend | shared
    }
}
