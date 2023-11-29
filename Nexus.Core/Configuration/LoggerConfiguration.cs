﻿using Nexus.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Core.Configuration
{
    /// <summary>
    /// Provides logger configuration
    /// </summary>
    public class LoggerConfiguration : ILoggerConfiguration
    {
        private const string DefaultLanguage = "en";

        /// <summary>
        /// Instantiates class using <see cref="ISettingsFile"/> with general settings.
        /// </summary>
        /// <param name="settingsFile">Settings file</param>
        public LoggerConfiguration(ISettingsFile settingsFile)
        {
            Language = settingsFile.GetValueOrDefault(".logger.language", DefaultLanguage);
            LogPageSource = settingsFile.GetValueOrDefault(".logger.logPageSource", true);
        }

        public string Language { get; }

        public bool LogPageSource { get; }
    }
}
