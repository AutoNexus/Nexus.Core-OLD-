﻿using OpenQA.Selenium;

namespace Nexus.Core.Applications
{
    /// <summary>
    /// Interface of any application controlled by Selenium WebDriver API
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// Current instance of driver
        /// </summary>
        WebDriver Driver { get; }

        /// <summary>
        /// Defines if the application is already started or not.
        /// </summary>
        bool IsStarted { get; }

        /// <summary>
        /// Sets implicit wait timeout to browser.
        /// Method was extracted with purpose not to pass it to Driver if it is similar to previous value.
        /// Simpliest implementation is: Driver.Manage().Timeouts().ImplicitlyWait = timeout
        /// </summary>
        /// <param name="timeout">timeout to set</param>
        void SetImplicitWaitTimeout(TimeSpan timeout);
    }
}
