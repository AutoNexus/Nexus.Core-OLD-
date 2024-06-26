﻿namespace Nexus.Core.Utilities
{
    /// <summary>
    /// Environment variables reader.
    /// </summary>
    public static class EnvironmentConfiguration
    {
        /// <summary>
        /// Gets value of environment variable by key.
        /// </summary>
        /// <param name="key">Environment variable key.</param>
        /// <returns>Value of environment variable.</returns>
        public static string GetVariable(string key)
        {
            var variables = new List<string>
            {
                Environment.GetEnvironmentVariable(key),
                Environment.GetEnvironmentVariable(key.ToLower()),
                Environment.GetEnvironmentVariable(key.ToUpper()),
                //necessary for Azure
                Environment.GetEnvironmentVariable(key.ToUpper().Replace('.', '_'))
            };
            return variables.Find(variable => variable != null);
        }
    }
}
