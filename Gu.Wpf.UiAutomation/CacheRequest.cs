namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;

    public class CacheRequest
    {
        [ThreadStatic]
        private static Stack<CacheRequest> cacheStack;
        [ThreadStatic]
        private static Stack<bool> forceNoCacheStack;

        public static bool IsCachingActive => (forceNoCacheStack == null || forceNoCacheStack.Count == 0) && Current != null;

        public static CacheRequest Current
        {
            get
            {
                if ((cacheStack != null) && (cacheStack.Count != 0))
                {
                    return cacheStack.Peek();
                }

                return null;
            }
        }

        public AutomationElementMode AutomationElementMode { get; set; }

        public ConditionBase TreeFilter { get; set; } = new TrueCondition();

        public TreeScope TreeScope { get; set; }

        public HashSet<PatternId> Patterns { get; } = new HashSet<PatternId>();

        public HashSet<PropertyId> Properties { get; } = new HashSet<PropertyId>();

        public void Add(PatternId pattern)
        {
            this.Patterns.Add(pattern);
        }

        public void Add(PropertyId property)
        {
            this.Properties.Add(property);
        }

        public IDisposable Activate()
        {
            Push(this);
            return new CacheRequestActivation();
        }

        public static void Push(CacheRequest cacheRequest)
        {
            if (cacheStack == null)
            {
                cacheStack = new Stack<CacheRequest>();
            }

            cacheStack.Push(cacheRequest);
        }

        public static void Pop()
        {
            if ((cacheStack == null) || (cacheStack.Count == 0))
            {
                throw new InvalidOperationException("No cache request available to pop");
            }

            cacheStack.Pop();
        }

        public static IDisposable ForceNoCache()
        {
            return new ForceNoCacheActivation();
        }

        private class CacheRequestActivation : IDisposable
        {
            public void Dispose()
            {
                Pop();
            }
        }

        private class ForceNoCacheActivation : IDisposable
        {
            public ForceNoCacheActivation()
            {
                if (forceNoCacheStack == null)
                {
                    forceNoCacheStack = new Stack<bool>();
                }

                forceNoCacheStack.Push(true);
            }

            public void Dispose()
            {
                forceNoCacheStack.Pop();
            }
        }
    }
}
