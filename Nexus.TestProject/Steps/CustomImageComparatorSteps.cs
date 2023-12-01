using Nexus.Core.Visualization;
using Nexus.Template.CustomAttributes;
using Nexus.Template.Utilities;
using SkiaSharp;

namespace Nexus.TestProject.Steps
{
    public class CustomImageComparatorSteps
    {
        private readonly CustomImageComparator customImageComparator;
        private readonly SKImage modelOfImage;

        public CustomImageComparatorSteps(float customThresholdValue, string modelImageResourses)
        {
            modelOfImage = new FileInfo(modelImageResourses).ReadImage();
            var imageWidth = modelOfImage.Width;
            var imageHeight = modelOfImage.Height;
            customImageComparator = new CustomImageComparator(customThresholdValue, imageWidth, imageHeight);
        }

        [LogStep(StepType.Step)]
        public static SKImage GetExpectedImageFromResourse(string expectedImageResourse)
        {
            return new FileInfo(expectedImageResourse).ReadImage();
        }

        [LogStep(StepType.Assertion)]
        public void CheckThatActualAndExpectedImagesAreTheSame(SKImage expectedImage)
        {
            var differenceBetweenImages = customImageComparator.Compare(modelOfImage, expectedImage);
            Assert.AreEqual(0, differenceBetweenImages, "The images should be the same");
        }

        [LogStep(StepType.Assertion)]
        public void CheckThatActualAndExpectedImagesAreNotTheSame(SKImage expectedImage)
        {
            var differenceBetweenImages = customImageComparator.Compare(modelOfImage, expectedImage);
            Assert.AreNotEqual(0, differenceBetweenImages, "The images should not be the same");
        }
    }
}
