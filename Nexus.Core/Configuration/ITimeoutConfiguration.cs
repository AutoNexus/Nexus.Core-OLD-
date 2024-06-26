﻿namespace Nexus.Core.Configuration
{
    /// <summary>
    /// Describes timeouts configuration.
    /// </summary>
    public interface ITimeoutConfiguration
    {
        /// <summary>
        /// Gets WedDriver ImplicitWait timeout.
        /// </summary>
        TimeSpan Implicit { get; }

        /// <summary>
        /// Gets default ConditionalWait timeout.
        /// </summary>
        TimeSpan Condition { get; }

        /// <summary>
        /// Gets ConditionalWait polling interfal.
        /// </summary>
        TimeSpan PollingInterval { get; }

        /// <summary>
        /// Gets Command timeout.
        /// </summary>
        TimeSpan Command { get; }
    }
}
