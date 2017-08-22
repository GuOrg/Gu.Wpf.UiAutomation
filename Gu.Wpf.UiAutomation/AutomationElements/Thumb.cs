namespace Gu.Wpf.UiAutomation.AutomationElements
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Input;

    public class Thumb : AutomationElement
    {
        public Thumb(BasicAutomationElementBase basicAutomationElement) : base(basicAutomationElement)
        {
        }

        /// <summary>
        /// Moves the slider horizontally
        /// </summary>
        /// <param name="distance">+ for right, - for left</param>
        public void SlideHorizontally(int distance)
        {
            Mouse.DragHorizontally(MouseButton.Left, Properties.BoundingRectangle.Value.Center, distance);
        }

        /// <summary>
        /// Moves the slider vertically
        /// </summary>
        /// <param name="distance">+ for down, - for up</param>
        public void SlideVertically(int distance)
        {
            Mouse.DragVertically(MouseButton.Left, Properties.BoundingRectangle.Value.Center, distance);
        }
    }
}
