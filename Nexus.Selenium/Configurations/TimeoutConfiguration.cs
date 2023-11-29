using Nexus.Core.Utilities;
using System;
using CoreTimeoutConfiguration = Nexus.Core.Configuration.TimeoutConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Selenium.Configurations
{
    /// <summary>
    /// Provides timeouts configuration.
    /// </summary>
    public class TimeoutConfiguration : CoreTimeoutConfiguration, ITimeoutConfiguration
    {
        private readonly ISettingsFile settingsFile;

        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public TimeoutConfiguration(ISettingsFile settingsFile) : base(settingsFile)
        {
            this.settingsFile = settingsFile;
            Script = GetTimeoutFromSeconds(nameof(Script));
            PageLoad = GetTimeoutFromSeconds(nameof(PageLoad));
        }

        private TimeSpan GetTimeoutFromSeconds(string name)
        {
            return TimeSpan.FromSeconds(settingsFile.GetValue<int>($".timeouts.timeout{name}"));
        }

        public TimeSpan Script { get; }

        public TimeSpan PageLoad { get; }
    }
}
