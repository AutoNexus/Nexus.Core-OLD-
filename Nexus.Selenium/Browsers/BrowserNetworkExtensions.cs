using Nexus.Selenium.Logging;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Selenium.Browsers
{
    public static class BrowserNetworkExtensions
    {
        /// <summary>
        /// Registers authentication handler and awaits to start network monitoring.
        /// </summary>
        /// <param name="browser">Browser instance.</param>
        /// <param name="hostPart">Part of the host name for URI matcher.</param>
        /// <param name="username">Username for authentication.</param>
        /// <param name="password">Password for authentication.</param>
        /// <returns>A task to be awaited.</returns>
        public static async Task RegisterBasicAuthenticationAndStartMonitoring(this Browser browser, string hostPart, string username, string password)
        {
            var handler = new NetworkAuthenticationHandler
            {
                UriMatcher = (uri) => uri.Host.Contains(hostPart),
                Credentials = new PasswordCredentials(username, password)
            };
            await RegisterBasicAuthenticationAndStartMonitoring(browser, handler);
        }

        /// <summary>
        /// Registers authentication handler and awaits to start network monitoring.
        /// </summary>
        /// <param name="browser">Browser instance.</param>
        /// <param name="handler">Authentication parameters, such as URI matcher and credentials.</param>
        /// <returns>A task to be awaited.</returns>
        public static async Task RegisterBasicAuthenticationAndStartMonitoring(this Browser browser, NetworkAuthenticationHandler handler)
        {
            var networkHandling = browser.Network;
            networkHandling.AddAuthenticationHandler(handler);
            await networkHandling.StartMonitoring();
        }

        /// <summary>
        /// Enables HTTP Request/Response logging, and starts network monitoring.
        /// </summary>
        /// <param name="browser">Browser instance.</param>
        /// <param name="loggingOptions">Logging preferences.</param>
        /// <returns>A task to be awaited.</returns>
        public static async Task EnableHttpExchangeLoggingAndStartMonitoring(this Browser browser, HttpExchangeLoggingOptions loggingOptions = null)
        {
            browser.EnableHttpExchangeLogging(loggingOptions);
            await browser.Network.StartMonitoring();
        }

        /// <summary>
        /// Enables HTTP Request/Response logging.
        /// </summary>
        /// <param name="browser">Browser instance.</param>
        /// <param name="loggingOptions">Logging preferences.</param>
        public static void EnableHttpExchangeLogging(this Browser browser, HttpExchangeLoggingOptions loggingOptions = null)
        {
            if (loggingOptions == null)
            {
                loggingOptions = new HttpExchangeLoggingOptions();
            }
            browser.Network.NetworkRequestSent += GetRequestEventHandler(loggingOptions);
            browser.Network.NetworkResponseReceived += GetResponseEventHandler(loggingOptions);
        }

        private static EventHandler<NetworkResponseReceivedEventArgs> GetResponseEventHandler(HttpExchangeLoggingOptions loggingOptions)
        {
            return (object sender, NetworkResponseReceivedEventArgs args) =>
            {
                if (loggingOptions.ResponseInfo.Enabled)
                {
                    NexusServices.LocalizedLogger.LogByLevel(
                        loggingOptions.ResponseInfo.LogLevel,
                        "loc.browser.network.event.responsereceived.log.info",
                        args.ResponseStatusCode, args.ResponseUrl, args.ResponseResourceType, args.RequestId);
                }
                if (loggingOptions.ResponseHeaders.Enabled && args.ResponseHeaders.Any())
                {
                    NexusServices.LocalizedLogger.LogByLevel(
                        loggingOptions.ResponseHeaders.LogLevel,
                        "loc.browser.network.event.responsereceived.log.headers",
                        string.Join(",", args.ResponseHeaders.Select(header => $"{Environment.NewLine}\t{header.Key}: {header.Value}")));
                }
                if (loggingOptions.ResponseBody.Enabled && !string.IsNullOrEmpty(args.ResponseBody))
                {
                    NexusServices.LocalizedLogger.LogByLevel(
                        loggingOptions.ResponseBody.LogLevel,
                        "loc.browser.network.event.responsereceived.log.body",
                        args.ResponseBody);
                }
            };
        }

        private static EventHandler<NetworkRequestSentEventArgs> GetRequestEventHandler(HttpExchangeLoggingOptions loggingOptions)
        {
            return (object sender, NetworkRequestSentEventArgs args) =>
            {
                if (loggingOptions.RequestInfo.Enabled)
                {
                    NexusServices.LocalizedLogger.LogByLevel(
                        loggingOptions.RequestInfo.LogLevel,
                        "loc.browser.network.event.requestsent.log.info",
                        args.RequestMethod, args.RequestUrl, args.RequestId);

                }
                if (loggingOptions.RequestHeaders.Enabled && args.RequestHeaders.Any())
                {
                    NexusServices.LocalizedLogger.LogByLevel(
                        loggingOptions.RequestHeaders.LogLevel,
                        "loc.browser.network.event.requestsent.log.headers",
                        string.Join(",", args.RequestHeaders.Select(header => $"{Environment.NewLine}\t{header.Key}: {header.Value}")));
                }
                if (loggingOptions.RequestPostData.Enabled && !string.IsNullOrEmpty(args.RequestPostData))
                {
                    NexusServices.LocalizedLogger.LogByLevel(
                        loggingOptions.RequestPostData.LogLevel,
                        "loc.browser.network.event.requestsent.log.data",
                        args.RequestPostData);
                }
            };
        }
    }
}
