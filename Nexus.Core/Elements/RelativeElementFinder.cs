﻿using Nexus.Core.Localization;
using Nexus.Core.Waitings;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Nexus.Core.Elements
{
    /// <summary>
    /// Implementation of <see cref="IElementFinder"/> for a relative <see cref="ISearchContext"/> supplier
    /// </summary>
    public class RelativeElementFinder : ElementFinder
    {
        public RelativeElementFinder(ILocalizedLogger logger, IConditionalWait conditionalWait, Func<ISearchContext> searchContextSupplier)
    : base(logger, conditionalWait)
        {
            ConditionalWait = conditionalWait;
            SearchContextSupplier = searchContextSupplier;
        }

        private IConditionalWait ConditionalWait { get; }

        private Func<ISearchContext> SearchContextSupplier { get; }

        public override ReadOnlyCollection<IWebElement> FindElements(By locator, DesiredState desiredState, TimeSpan? timeout = null, string name = null)
        {
            var foundElements = new List<IWebElement>();
            var resultElements = new List<IWebElement>();
            try
            {
                ConditionalWait.WaitForTrue(() =>
                {
                    foundElements = SearchContextSupplier().FindElements(locator).ToList();
                    resultElements = foundElements.Where(desiredState.ElementStateCondition).ToList();
                    return resultElements.Any();
                }, timeout);
            }
            catch (TimeoutException ex)
            {
                HandleTimeoutException(new WebDriverTimeoutException(ex.Message, ex), desiredState, locator, foundElements, name);
            }
            return resultElements.AsReadOnly();
        }
    }
}
