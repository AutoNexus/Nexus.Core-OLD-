﻿using Nexus.Core.Localization;
using Nexus.Core.Utilities;
using Nexus.Selenium.Browsers;
using Nexus.Selenium.Configurations;
using Nexus.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using SeleniumActions = OpenQA.Selenium.Interactions.Actions;

namespace Nexus.Selenium.Elements.Actions
{
    /// <summary>
    /// Allows to perform actions on elements via Selenium Actions class.
    /// </summary>
    public class MouseActions
    {
        private readonly IElement element;
        private readonly string elementType;
        private readonly ILocalizedLogger logger;
        private readonly IElementActionRetrier elementActionsRetrier;

        public MouseActions(IElement element, string elementType, ILocalizedLogger logger, IElementActionRetrier elementActionsRetrier)
        {
            this.element = element;
            this.elementType = elementType;
            this.logger = logger;
            this.elementActionsRetrier = elementActionsRetrier;
        }

        private JsActions JsActions => new JsActions(element, elementType, logger, NexusServices.Get<IBrowserProfile>());

        /// <summary>
        /// Performs click on element.
        /// </summary>
        public void Click()
        {
            LogElementAction("loc.clicking");
            JsActions.HighlightElement();
            elementActionsRetrier.DoWithRetry(() => PerformAction(element => MoveToElement(element).Click(element)));
        }

        /// <summary>
        /// Performs double click on element.
        /// </summary>
        public void DoubleClick()
        {
            LogElementAction("loc.clicking.double");
            elementActionsRetrier.DoWithRetry(() => PerformAction(element => MoveToElement(element).DoubleClick(element)));
        }

        /// <summary>
        /// Perfroms right click on element.
        /// </summary>
        public void RightClick()
        {
            LogElementAction("loc.clicking.right");
            elementActionsRetrier.DoWithRetry(() => PerformAction(element => MoveToElement(element).ContextClick(element)));
        }

        /// <summary>
        /// Moves mouse to the element.
        /// </summary>
        public void MoveToElement()
        {
            LogElementAction("loc.moving");
            JsActions.ScrollIntoView();
            elementActionsRetrier.DoWithRetry(() => PerformAction(MoveToElement));
        }

        /// <summary>
        /// Moves mouse from this element.
        /// </summary>
        public void MoveMouseFromElement()
        {
            LogElementAction("loc.movingFrom");
            elementActionsRetrier.DoWithRetry(
                () => PerformAction(
                    element => new SeleniumActions(NexusServices.Browser.Driver)
                    .MoveToElement(element, -element.Size.Width / 2, -element.Size.Height / 2)));
        }

        private SeleniumActions MoveToElement(IWebElement element)
        {
            return new SeleniumActions(NexusServices.Browser.Driver).MoveToElement(element);
        }

        private void PerformAction(Func<WebElement, SeleniumActions> action)
        {
            action(element.GetElement()).Build().Perform();
        }

        protected internal void LogElementAction(string messageKey, params object[] args)
        {
            logger.InfoElementAction(elementType, element.Name, messageKey, args);
        }
    }
}
