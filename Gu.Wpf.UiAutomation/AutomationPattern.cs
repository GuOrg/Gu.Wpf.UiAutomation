namespace Gu.Wpf.UiAutomation
{
    using System;

    public class AutomationPattern<T, TNative> : IAutomationPattern<T>
        where T : IPattern
    {
        private readonly Func<BasicAutomationElementBase, TNative, T> patternCreateFunc;
        private readonly PatternId patternId;

        public AutomationPattern(PatternId patternId, BasicAutomationElementBase basicAutomationElement, Func<BasicAutomationElementBase, TNative, T> patternCreateFunc)
        {
            this.patternId = patternId;
            this.BasicAutomationElement = basicAutomationElement;
            this.patternCreateFunc = patternCreateFunc;
        }

        /// <inheritdoc/>
        public T Pattern
        {
            get
            {
                var nativePattern = this.BasicAutomationElement.GetNativePattern<TNative>(this.patternId);
                return this.patternCreateFunc(this.BasicAutomationElement, nativePattern);
            }
        }

        /// <inheritdoc/>
        public T PatternOrDefault
        {
            get
            {
                this.TryGetPattern(out T pattern);
                return pattern;
            }
        }

        /// <inheritdoc/>
        public bool IsSupported => this.TryGetPattern(out T _);

        protected BasicAutomationElementBase BasicAutomationElement { get; }

        /// <inheritdoc/>
        public bool TryGetPattern(out T pattern)
        {
            if (this.BasicAutomationElement.TryGetNativePattern(this.patternId, out TNative nativePattern))
            {
                pattern = this.patternCreateFunc(this.BasicAutomationElement, nativePattern);
                return true;
            }

            pattern = default(T);
            return false;
        }
    }
}
