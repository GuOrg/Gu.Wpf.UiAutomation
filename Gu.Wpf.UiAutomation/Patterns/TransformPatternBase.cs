namespace Gu.Wpf.UiAutomation
{
    public abstract class TransformPatternBase<TNativePattern> : PatternBase<TNativePattern>, ITransformPattern
        where TNativePattern : class
    {
        private AutomationProperty<bool> canMove;
        private AutomationProperty<bool> canResize;
        private AutomationProperty<bool> canRotate;

        protected TransformPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public ITransformPatternProperties Properties => this.Automation.PropertyLibrary.Transform;

        /// <inheritdoc/>
        public AutomationProperty<bool> CanMove => this.GetOrCreate(ref this.canMove, this.Properties.CanMove);

        /// <inheritdoc/>
        public AutomationProperty<bool> CanResize => this.GetOrCreate(ref this.canResize, this.Properties.CanResize);

        /// <inheritdoc/>
        public AutomationProperty<bool> CanRotate => this.GetOrCreate(ref this.canRotate, this.Properties.CanRotate);

        /// <inheritdoc/>
        public abstract void Move(double x, double y);

        /// <inheritdoc/>
        public abstract void Resize(double width, double height);

        /// <inheritdoc/>
        public abstract void Rotate(double degrees);
    }
}
