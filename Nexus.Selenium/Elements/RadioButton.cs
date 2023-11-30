using Nexus.Core.Elements;
using Nexus.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Nexus.Selenium.Elements
{
    /// <summary>
    /// Defines RadioButton UI element.
    /// </summary>
    public class RadioButton : CheckableElement, IRadioButton
    {
        protected internal RadioButton(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.radio");
    }
}
