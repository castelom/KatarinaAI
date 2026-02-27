namespace KatarinaAI.Agent.Infrastructure.Configuration
{
    public class GitHubSettings
    {
        public string PersonalAccessToken { get; set; }
        public string BaseApiUrl { get; set; } = "https://api.github.com";
    }
}
