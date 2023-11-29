using OpenQA.Selenium;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Core.Visualization
{
    /// <summary>
    /// Selenium screenshot extensions.
    /// </summary>
    public static class ScreenshotExtensions
    {
        /// <summary>
        /// Represents given screenshot as an image.
        /// </summary>
        /// <param name="screenshot">Given screenshot.</param>
        /// <returns>Image.</returns>
        public static SKImage AsImage(this Screenshot screenshot)
        {
            return SKImage.FromEncodedData(screenshot.AsByteArray);
        }
    }
}
