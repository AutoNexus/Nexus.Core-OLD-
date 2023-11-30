using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Describes behavior of Link UI element.
    /// </summary>
    public interface ILink : IElement
    {
        /// <summary>
        /// Gets value of href attribute.
        /// </summary>
        /// <value>String representation of element's href.</value>
        string Href { get; }
    }
}
