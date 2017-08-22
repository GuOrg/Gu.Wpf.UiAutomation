namespace Gu.Wpf.UiAutomation
{
    using System;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IAutomationPattern<T> where T : IPattern
    {
        T Pattern { get; }

        T PatternOrDefault { get; }

        bool TryGetPattern(out T pattern);

        bool IsSupported { get; }
    }

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

        public T Pattern
        {
            get
            {
                var nativePattern = this.BasicAutomationElement.GetNativePattern<TNative>(this.patternId);
                return this.patternCreateFunc(this.BasicAutomationElement, nativePattern);
            }
        }

        public T PatternOrDefault
        {
            get
            {
                T pattern;
                this.TryGetPattern(out pattern);
                return pattern;
            }
        }

        public bool TryGetPattern(out T pattern)
        {
            TNative nativePattern;
            if (this.BasicAutomationElement.TryGetNativePattern(this.patternId, out nativePattern))
            {
                pattern = this.patternCreateFunc(this.BasicAutomationElement, nativePattern);
                return true;
            }

            pattern = default(T);
            return false;
        }

        public bool IsSupported
        {
            get
            {
                T pattern;
                return this.TryGetPattern(out pattern);
            }
        }
    }
}
