namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class TransformPatternBase<TNativePattern> : PatternBase<TNativePattern>, ITransformPattern
    {
        private AutomationProperty<bool> canMove;
        private AutomationProperty<bool> canResize;
        private AutomationProperty<bool> canRotate;

        protected TransformPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public ITransformPatternProperties Properties => this.Automation.PropertyLibrary.Transform;

        public AutomationProperty<bool> CanMove => this.GetOrCreate(ref this.canMove, this.Properties.CanMove);

        public AutomationProperty<bool> CanResize => this.GetOrCreate(ref this.canResize, this.Properties.CanResize);

        public AutomationProperty<bool> CanRotate => this.GetOrCreate(ref this.canRotate, this.Properties.CanRotate);

        public abstract void Move(double x, double y);

        public abstract void Resize(double width, double height);

        public abstract void Rotate(double degrees);
    }
}
