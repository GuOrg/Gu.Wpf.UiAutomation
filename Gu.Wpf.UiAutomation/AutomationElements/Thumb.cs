namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class Thumb : UiElement
    {
        public Thumb(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// Moves the slider horizontally
        /// </summary>
        /// <param name="distance">+ for right, - for left</param>
        public void SlideHorizontally(int distance)
        {
            Mouse.DragHorizontally(MouseButton.Left, this.Bounds.Center(), distance);
        }

        /// <summary>
        /// Moves the slider vertically
        /// </summary>
        /// <param name="distance">+ for down, - for up</param>
        public void SlideVertically(int distance)
        {
            Mouse.DragVertically(MouseButton.Left, this.Bounds.Center(), distance);
        }
    }
}
