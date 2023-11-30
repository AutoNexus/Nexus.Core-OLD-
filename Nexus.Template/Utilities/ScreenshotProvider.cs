using Nexus.Core.Visualization;
using Nexus.Selenium.Browsers;
using SkiaSharp;

namespace Nexus.Template.Utilities
{
    public class ScreenshotProvider
    {
        public string TakeScreenshot()
        {
            var image = GetImage();
            var directory = Path.Combine(Environment.CurrentDirectory, "screenshots");
            EnsureDirectoryExists(directory);
            var screenshotName = $"{GetType().Name}_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid().ToString("n").Substring(0, 5)}.png";
            var path = Path.Combine(directory, screenshotName);
            image.Save(path, SKEncodedImageFormat.Png);
            return path;
        }

        private static SKImage GetImage()
        {
            using var stream = new MemoryStream(NexusServices.Browser.GetScreenshot());
            return SKImage.FromEncodedData(stream);
        }

        private static void EnsureDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
