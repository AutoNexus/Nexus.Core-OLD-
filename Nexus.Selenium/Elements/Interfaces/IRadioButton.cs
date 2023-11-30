using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Describes behavior of RadioButton UI element.
    /// </summary>
    public interface IRadioButton : IElement
    {
        /// <summary>
        /// Gets RadioButton state.
        /// </summary>
        /// <value>True if checked and false otherwise.</value>
        bool IsChecked { get; }
    }
}
