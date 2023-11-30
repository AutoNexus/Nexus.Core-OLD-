using Nexus.Selenium.Browsers;
using Nexus.Selenium.Configurations;
using Nexus.Core.Localization;
using Nexus.Selenium.Elements.Interfaces;

namespace Nexus.Selenium.Elements.Actions
{
    /// <summary>
    /// Allows to perform actions on elements via JavaScript specific for CheckBoxes.
    /// </summary>
    public class CheckBoxJsActions : JsActions
    {
        public CheckBoxJsActions(IElement element, string elementType, ILocalizedLogger logger, IBrowserProfile browserProfile)
            : base(element, elementType, logger, browserProfile)
        {
        }

        /// <summary>
        /// Gets CheckBox state.
        /// </summary>
        /// <returns>True if checked and false otherwise</returns>
        public bool IsChecked()
        {
            LogElementAction("loc.checkable.get.state");
            var state = GetState();
            LogElementAction("loc.checkable.state", state);
            return state;
        }

        /// <summary>
        /// Performs check action on the element.
        /// </summary>
        public void Check()
        {
            SetState(true);
        }

        /// <summary>
        /// Performs uncheck action on the element.
        /// </summary>
        public void Uncheck()
        {
            SetState(false);
        }

        /// <summary>
        /// Performs toggle action on the element.
        /// </summary>
        public void Toggle()
        {
            SetState(!GetState());
        }

        private void SetState(bool state)
        {
            LogElementAction("loc.setting.value", state);
            if (state != GetState())
            {
                Click();
            }
        }

        private bool GetState()
        {
            return ExecuteScript<bool>(JavaScript.GetCheckBoxState);
        }
    }
}
