using Nexus.Core.Utilities;
using Nexus.Selenium.Browsers;
using System.Reflection;

namespace Nexus.Template.Configurations
{
    internal static class Environment
    {
        public static ISettingsFile CurrentEnvironment
        {
            get
            {
                var envName = NexusServices.Get<ISettingsFile>().GetValue<string>("environment");
                var pathToConfigFile = $"Resources.Environment.{envName}.config.json";
                return new JsonSettingsFile(pathToConfigFile, Assembly.GetCallingAssembly());
            }
        }
    }
}
