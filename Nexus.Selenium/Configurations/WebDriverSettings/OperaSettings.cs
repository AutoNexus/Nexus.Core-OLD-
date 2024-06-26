﻿using Nexus.Core.Utilities;
using Nexus.Selenium.Browsers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using System.Reflection;

namespace Nexus.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for Opera web driver.
    /// </summary>
    public class OperaSettings : ChromeSettings
    {
        private const string DefaultBinaryLocation = "%USERPROFILE%\\AppData\\Local\\Programs\\Opera\\launcher.exe";
        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public OperaSettings(ISettingsFile settingsFile) : base(settingsFile)
        {
        }

        public virtual string BinaryLocation
        {
            get
            {
                var pathInConfiguration = SettingsFile.GetValueOrDefault($"{DriverSettingsPath}.binaryLocation", DefaultBinaryLocation);
                return pathInConfiguration.StartsWith("%") ? Environment.ExpandEnvironmentVariables(pathInConfiguration) : Path.GetFullPath(pathInConfiguration);
            }
        }

        protected override BrowserName BrowserName => BrowserName.Opera;

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = (ChromeOptions)base.DriverOptions;
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
                var field = typeof(ChromiumOptions).GetField("additionalChromeOptions", BindingFlags.NonPublic | BindingFlags.Instance);
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
                if (field.GetValue(options) is Dictionary<string, object> optionsDictionary)
                {
                    optionsDictionary["w3c"] = true;
                    field.SetValue(options, optionsDictionary);
                }

                options.BinaryLocation = BinaryLocation;
                return options;
            }
        }
    }
}
