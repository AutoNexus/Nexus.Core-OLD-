using System;
using CoreTimeoutConfiguration = Nexus.Core.Configuration.ITimeoutConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Selenium.Configurations
{
    /// <summary>
    /// Describes timeouts configuration.
    /// </summary>
    public interface ITimeoutConfiguration : CoreTimeoutConfiguration
    {
        /// <summary>
        /// Gets WedDriver AsynchronousJavaScript timeout.
        /// </summary>
        TimeSpan Script { get; }

        /// <summary>
        /// Gets WedDriver PageLoad timeout.
        /// </summary>
        TimeSpan PageLoad { get; }
    }
}
