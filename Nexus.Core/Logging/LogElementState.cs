using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Core.Logging
{
    /// <summary>
    /// Logs element state.
    /// </summary>
    /// <param name="messageKey">Key of localized message to log.</param>
    /// <param name="stateKey">Key of localized state to log.</param>
    public delegate void LogElementState(string messageKey, string stateKey);

    /// <summary>
    /// Logs element visual state.
    /// </summary>
    /// <param name="messageKey">Key of localized message to log.</param>
    /// <param name="values">Values to put into localized message (if any).</param>
    public delegate void LogVisualState(string messageKey, params object[] values);
}
