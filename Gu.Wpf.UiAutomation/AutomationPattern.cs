namespace Gu.Wpf.UiAutomation
{
    using System;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

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

        protected BasicAutomationElementBase BasicAutomationElement { get; }

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

        /// <inheritdoc/>
        public bool IsSupported
        {
            get
            {
                return this.TryGetPattern(out T pattern);
            }
        }
    }
}
