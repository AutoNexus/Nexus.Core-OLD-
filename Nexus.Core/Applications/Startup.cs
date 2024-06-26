﻿using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Configuration;
using Nexus.Core.Elements;
using Nexus.Core.Elements.Interfaces;
using Nexus.Core.Localization;
using Nexus.Core.Logging;
using Nexus.Core.Utilities;
using Nexus.Core.Visualization;
using Nexus.Core.Waitings;
using System.Reflection;

namespace Nexus.Core.Applications
{
    /// <summary>
    /// Allows to resolve dependencies for all services in the Nexus.Core library
    /// </summary>
    public class Startup
    {
        private ISettingsFile settingsFile;

        /// <summary>
        /// Used to configure dependencies for services of the current library
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="applicationProvider">function that provides an instance of <see cref="IApplication"/></param>
        /// <param name="settings">File with settings for configuration of dependencies.
        /// Pass the result of <see cref="GetSettings"/> if you need to get settings from the embedded resource of your project.</param>
        public virtual IServiceCollection ConfigureServices(IServiceCollection services, Func<IServiceProvider, IApplication> applicationProvider, ISettingsFile settings = null)
        {
            settingsFile = settings ?? GetSettings();
            services.AddScoped(applicationProvider);

            services.AddSingleton(settingsFile);
            services.AddSingleton(Logger.Instance);
            services.AddSingleton<IElementCacheConfiguration, ElementCacheConfiguration>();
            services.AddSingleton<ILoggerConfiguration, LoggerConfiguration>();
            services.AddSingleton<ITimeoutConfiguration, TimeoutConfiguration>();
            services.AddSingleton<IRetryConfiguration, RetryConfiguration>();
            services.AddSingleton<IVisualizationConfiguration, VisualizationConfiguration>();
            services.AddSingleton<IImageComparator, ImageComparator>();
            services.AddSingleton<ILocalizationManager, LocalizationManager>();
            services.AddSingleton<ILocalizedLogger, LocalizedLogger>();
            services.AddSingleton<IActionRetrier, ActionRetrier>();
            services.AddSingleton<IElementActionRetrier, ElementActionRetrier>();

            services.AddTransient<IConditionalWait, ConditionalWait>();
            services.AddTransient<IElementFinder, ElementFinder>();
            services.AddTransient<IElementFactory, ElementFactory>();
            return services;
        }

        /// <summary>
        /// Provides a <see cref="ISettingsFile"/> with settings.
        /// Value is set in <see cref="ConfigureServices"/>
        /// Otherwise, will use default JSON settings file with name: "settings.{profile}.json".
        /// Default settings will look for the resource file (copied to binaries/Resources/ folder);
        /// If not found, will look for embedded resource in the calling assembly of this method
        /// </summary>
        /// <returns>An instance of settings</returns>
        public ISettingsFile GetSettings()
        {
            if (settingsFile == null)
            {
                var profileNameFromEnvironment = EnvironmentConfiguration.GetVariable("profile");
                var settingsProfile = profileNameFromEnvironment == null ? "settings.json" : $"settings.{profileNameFromEnvironment}.json";
                Logger.Instance.Debug($"Get settings from: {settingsProfile}");

                var jsonFile = FileReader.IsResourceFileExist(settingsProfile)
                    ? new JsonSettingsFile(settingsProfile)
                    : new JsonSettingsFile($"Resources.{settingsProfile}", Assembly.GetCallingAssembly());
                return jsonFile;
            }

            return settingsFile;
        }
    }
}
