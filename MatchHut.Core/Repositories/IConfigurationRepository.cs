using MatchHut.Core.Models;

namespace MatchHut.Core.Repositories
{
    public interface IConfigurationRepository : IRepository<Configuration>
    {
        string GetConfigByKey(string key);

        string GetConfigByKey(string key, string defaultValue);
    }
}