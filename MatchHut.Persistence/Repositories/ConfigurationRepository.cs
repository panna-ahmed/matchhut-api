using Humanizer;
using Microsoft.EntityFrameworkCore;
using MatchHut.Core;
using MatchHut.Core.Models;
using MatchHut.Core.Repositories;
using System.Linq;

namespace MatchHut.Persistence.Repositories
{
    public class ConfigurationRepository : Repository<Configuration>
    , IConfigurationRepository
    {
        public ConfigurationRepository(ApplicationDbContext context)
        : base(context)
        {
        }

        public string GetConfigByKey(string key, string defaultValue)
        {
            var config = Context.Set<Configuration>().AsNoTracking().SingleOrDefault(c => c.ConfigCode == key && c.Status == Statuses.Active.Humanize());
            if (config == null)
                return defaultValue;

            return config.ConfigValue;
        }

        public string GetConfigByKey(string key)
        {
            return Context.Set<Configuration>().Where(c => c.ConfigCode == key && c.Status == Statuses.Active.Humanize()).SingleOrDefault().ConfigValue;
        }
    }
}