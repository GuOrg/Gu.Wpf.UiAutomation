namespace Gu.Wpf.UiAutomation
{
    public interface ITransformPattern : IPattern
    {
        ITransformPatternProperties Properties { get; }

        AutomationProperty<bool> CanMove { get; }

        AutomationProperty<bool> CanResize { get; }

        AutomationProperty<bool> CanRotate { get; }

        void Move(double x, double y);

        void Resize(double width, double height);

        void Rotate(double degrees);
    }
}