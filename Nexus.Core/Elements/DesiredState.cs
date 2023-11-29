using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Core.Elements
{
    /// <summary>
    /// Defines desired state for element with ability to handle exceptions
    /// </summary>
    public class DesiredState
    {
        public DesiredState(Func<IWebElement, bool> elementStateCondition, string stateName)
        {
            ElementStateCondition = elementStateCondition;
            StateName = stateName;
        }

        public Func<IWebElement, bool> ElementStateCondition { get; }

        public bool IsCatchingTimeoutException { get; set; }

        public bool IsThrowingNoSuchElementException { get; set; }

        public string StateName { get; }
    }
}
