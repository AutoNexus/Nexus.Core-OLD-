﻿using Nexus.Core.Utilities;
using Nexus.Selenium.Browsers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for Chrome web driver.
    /// </summary>
    public class ChromeSettings : DriverSettings
    {
        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public ChromeSettings(ISettingsFile settingsFile) : base(settingsFile)
        {
        }

        protected override BrowserName BrowserName => BrowserName.Chrome;

        public override string DownloadDirCapabilityKey => "download.default_directory";

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = new ChromeOptions();
                SetChromePrefs(options);
                SetCapabilities(options, (name, value) => options.AddAdditionalOption(name, value));
                SetChromeArguments(options);
                SetChromeExcludedArguments(options);
                SetPageLoadStrategy(options);
                SetLoggingPreferences(options);
                return options;
            }
        }

        private void SetChromeExcludedArguments(ChromeOptions options)
        {
            options.AddExcludedArguments(BrowserExcludedArguments);
        }

        private void SetChromePrefs(ChromeOptions options)
        {
            foreach (var option in BrowserOptions)
            {
                var value = option.Key == DownloadDirCapabilityKey ? DownloadDir : option.Value;
                options.AddUserProfilePreference(option.Key, value);
            }
        }

        private void SetChromeArguments(ChromeOptions options)
        {
            options.AddArguments(BrowserStartArguments);
        }
    }
}
