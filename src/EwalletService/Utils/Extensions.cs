using Microsoft.Extensions.Configuration;

namespace EwalletService.Utils
{
    public static class Extensions
    {
        public static IConfigurationSection GetBackgroundServiceSettings(this IConfiguration configuration, string name)
        {
            return configuration.GetSection("BackgroundService").GetSection(name);
        }
    }
}
