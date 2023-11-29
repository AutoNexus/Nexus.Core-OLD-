using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Applications;
using Nexus.Core.Configuration;
using Nexus.Core.Elements.Interfaces;
using Nexus.Core.Elements;
using Nexus.Core.Localization;
using Nexus.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            services.AddTransient(serviceProvider => AqualityServices.BrowserFactory);
            return services;
        }
    }
}
