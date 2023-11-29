using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Describes web driver settings.
    /// </summary>
    public interface IDriverSettings
    {
        /// <summary>
        /// Gets version of web driver for WebDriverManager.
        /// </summary>
        string WebDriverVersion { get; }

        /// <summary>
        /// Gets target system architecture for WebDriverManager.
        /// </summary>
        Architecture SystemArchitecture { get; }

        /// <summary>
        /// Gets desired options for web driver.
        /// </summary>
        DriverOptions DriverOptions { get; }

        /// <summary>
        /// WebDriver page load strategy.
        /// </summary>
        PageLoadStrategy PageLoadStrategy { get; }

        /// <summary>
        /// Gets download directory for web driver.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when browser settings do not contain capability key.</exception>
        string DownloadDir { get; }

        /// <summary>
        /// Gets web driver capability key for download directory.
        /// </summary>
        string DownloadDirCapabilityKey { get; }
    }
}
