using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Applications;
using Nexus.Core.Localization;
using Nexus.Core.Logging;
using Nexus.Core.Utilities;
using Nexus.Selenium.Configurations;
using Nexus.Selenium.Elements;
using Nexus.Selenium.Elements.Interfaces;
using System.Reflection;
using ICoreElementFactory = Nexus.Core.Elements.Interfaces.IElementFactory;
using ICoreTimeoutConfiguration = Nexus.Core.Configuration.ITimeoutConfiguration;
using ILoggerConfiguration = Nexus.Core.Configuration.ILoggerConfiguration;
using ITimeoutConfiguration = Nexus.Selenium.Configurations.ITimeoutConfiguration;
using TimeoutConfiguration = Nexus.Selenium.Configurations.TimeoutConfiguration;
namespace Nexus.Selenium.Browsers
{
    public class BrowserStartup : Startup
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services, Func<IServiceProvider, IApplication> applicationProvider,
    ISettingsFile settings = null)
        {
            settings = settings ?? GetSettings();
            base.ConfigureServices(services, applicationProvider, settings);
            services.AddTransient<IElementFactory, ElementFactory>();
            services.AddTransient<ICoreElementFactory, ElementFactory>();
            services.AddSingleton<ITimeoutConfiguration>(serviceProvider => new TimeoutConfiguration(settings));
            services.AddSingleton<ICoreTimeoutConfiguration>(serviceProvider => new TimeoutConfiguration(settings));
            services.AddSingleton<IBrowserProfile>(serviceProvider => new BrowserProfile(settings));
            services.AddSingleton<ILocalizationManager>(serviceProvider => new LocalizationManager(serviceProvider.GetRequiredService<ILoggerConfiguration>(), serviceProvider.GetRequiredService<Logger>(), Assembly.GetExecutingAssembly()));
            services.AddTransient(serviceProvider => NexusServices.BrowserFactory);
            return services;
        }
    }
}
