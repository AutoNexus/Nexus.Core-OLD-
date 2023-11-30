using Nexus.Selenium.Configurations;

namespace Nexus.Template.Configurations
{
    public interface ICustomTimeoutConfiguration : ITimeoutConfiguration
    {
        public TimeSpan ElementAppear { get; }
    }
}
