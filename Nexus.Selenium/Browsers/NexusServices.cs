﻿using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Applications;
using Nexus.Core.Localization;
using Nexus.Core.Logging;
using Nexus.Core.Utilities;
using Nexus.Core.Waitings;
using Nexus.Selenium.Configurations;

namespace Nexus.Selenium.Browsers
{
    /// <summary>
    /// Controls browser instance creation.
    /// </summary>
    public class NexusServices : NexusServices<Browser>
    {
        private static readonly ThreadLocal<BrowserStartup> BrowserStartupContainer = new ThreadLocal<BrowserStartup>(() => new BrowserStartup());
        private static readonly ThreadLocal<IBrowserFactory> BrowserFactoryContainer = new ThreadLocal<IBrowserFactory>();

        /// <summary>
        /// Check if browser already started.
        /// </summary>
        /// <value>true if browser started and false otherwise.</value>
        public static bool IsBrowserStarted => IsApplicationStarted();

        /// <summary>
        /// Gets registered instance of logger
        /// </summary>
        public static Logger Logger => Get<Logger>();

        /// <summary>
        /// Gets registered instance of localized logger
        /// </summary>
        public static ILocalizedLogger LocalizedLogger => Get<ILocalizedLogger>();

        /// <summary>
        /// Gets ConditionalWait object
        /// </summary>
        public static IConditionalWait ConditionalWait => Get<IConditionalWait>();

        /// <summary>
        /// Gets and sets thread-safe instance of browser.
        /// </summary>
        /// <value>Instance of desired browser.</value>
        public static Browser Browser
        {
            get => GetApplication(StartBrowserFunction, ConfigureServices);
            set => SetApplication(value);
        }

        private static Func<IServiceProvider, Browser> StartBrowserFunction => services => BrowserFactory.Browser;

        /// <summary>
        /// Method which allow user to override or add custom services.
        /// </summary>
        /// <param name="startup"><see cref="BrowserStartup"/>> object with custom or overriden services.</param>
        public static void SetStartup(BrowserStartup startup)
        {
            if (startup != null)
            {
                BrowserStartupContainer.Value = startup;
                SetServiceProvider(ConfigureServices().BuildServiceProvider());
            }
        }

        /// <summary>
        /// Factory for application creation.
        /// </summary>
        public static IBrowserFactory BrowserFactory
        {
            get
            {
                if (!BrowserFactoryContainer.IsValueCreated)
                {
                    SetDefaultFactory();
                }

                return BrowserFactoryContainer.Value;
            }
            set => BrowserFactoryContainer.Value = value;
        }

        /// <summary>
        /// Sets default factory responsible for browser creation.
        /// RemoteBrowserFactory if value set in configuration and LocalBrowserFactory otherwise.
        /// </summary>
        public static void SetDefaultFactory()
        {
            var appProfile = Get<IBrowserProfile>();
            IBrowserFactory applicationFactory;
            if (appProfile.IsRemote)
            {
                applicationFactory = new RemoteBrowserFactory(Get<IActionRetrier>(), Get<IBrowserProfile>(), Get<ITimeoutConfiguration>(), LocalizedLogger);
            }
            else
            {
                applicationFactory = new LocalBrowserFactory(Get<IActionRetrier>(), Get<IBrowserProfile>(), Get<ITimeoutConfiguration>(), LocalizedLogger);
            }

            BrowserFactory = applicationFactory;
        }

        /// <summary>
        /// Resolves required service from <see cref="ServiceProvider"/>
        /// </summary>
        /// <typeparam name="T">type of required service.</typeparam>
        /// <exception cref="InvalidOperationException">Thrown if there is no service of the required type.</exception> 
        /// <returns></returns>
        public static T Get<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        private static IServiceProvider ServiceProvider => GetServiceProvider(services => Browser, ConfigureServices);

        private static IServiceCollection ConfigureServices()
        {
            return BrowserStartupContainer.Value.ConfigureServices(new ServiceCollection(), services => Browser);
        }
    }
}
