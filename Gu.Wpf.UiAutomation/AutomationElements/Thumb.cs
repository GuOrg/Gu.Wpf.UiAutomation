namespace Gu.Wpf.UiAutomation
{
    using System.Windows;
    using System.Windows.Automation;

    public class Thumb : UiElement
    {
        private const int DragSpeed = 200;

        public Thumb(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public TransformPattern TransformPattern => this.AutomationElement.TransformPattern();

        /// <summary>
        /// Moves the slider horizontally.
        /// </summary>
        /// <param name="distance">+ for right, - for left.</param>
        public void SlideHorizontally(int distance)
        {
            var cp = this.GetClickablePoint();
            Mouse.Drag(MouseButton.Left, cp, cp + new Vector(distance, 0), DragSpeed);
            Wait.UntilInputIsProcessed();
        }

        /// <summary>
        /// Moves the slider vertically.
        /// </summary>
        /// <param name="distance">+ for down, - for up.</param>
        public void SlideVertically(int distance)
        {
            var cp = this.GetClickablePoint();
            Mouse.Drag(MouseButton.Left, cp, cp + new Vector(0, distance), DragSpeed);
            Wait.UntilInputIsProcessed();
        }
    }
}
