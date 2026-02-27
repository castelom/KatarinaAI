using KatarinaAI.Agent.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KatarinaAI.Agent.Infrastructure.Repositories
{
    public class GitHubRepositoryIndexer
    {
        private readonly ProjectSettings _projectSettings;

        public GitHubRepositoryIndexer(
            IOptions<ProjectSettings> projectOptions)
        {
            _projectSettings = projectOptions.Value;
        }

        public async Task IndexAllRepositoriesAsync()
        {
            foreach (var repo in _projectSettings.Repositories)
            {
               // await IndexRepositoryAsync(repo);
            }
        }
    }
}
