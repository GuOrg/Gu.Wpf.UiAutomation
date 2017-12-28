namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;

    public static partial class AutomationElementExt
    {
        [Obsolete("This can be removed.")]
        public static UiElement GetDesktop(this AutomationElement element)
        {
            return new UiElement(AutomationElement.RootElement);
        }

        public static T FindFirst<T>(this AutomationElement element, TreeScope treeScope, ConditionBase condition, Func<AutomationElement, T> wrap)
        {
            return wrap(element.FindFirst(treeScope, condition.ToNative()));
        }

        public static UiElement FindFirst(this AutomationElement element, TreeScope treeScope, ConditionBase condition)
        {
            return new UiElement(element.FindFirst(treeScope, condition.ToNative()));
        }

        public static IReadOnlyList<UiElement> FindAll(this AutomationElement element, TreeScope treeScope, ConditionBase condition)
        {
            throw new NotImplementedException();
            //var nativeFoundElements = CacheRequest.IsCachingActive
            //    ? this.NativeElement.FindAllBuildCache((Interop.UIAutomationClient.TreeScope)treeScope, condition.ToNative(this.Automation.NativeAutomation), CacheRequest.Current.ToNative(this.Automation))
            //    : this.NativeElement.FindAll((Interop.UIAutomationClient.TreeScope)treeScope, condition.ToNative(this.Automation.NativeAutomation));
            //return AutomationElementConverter.NativeArrayToManaged(this.Automation, nativeFoundElements);
        }

        public static IReadOnlyList<T> FindAll<T>(this AutomationElement element, TreeScope treeScope, ConditionBase condition, Func<AutomationElement, T> wrap)
        {
            throw new NotImplementedException();
            //var nativeFoundElements = CacheRequest.IsCachingActive
            //    ? this.NativeElement.FindAllBuildCache((Interop.UIAutomationClient.TreeScope)treeScope, condition.ToNative(this.Automation.NativeAutomation), CacheRequest.Current.ToNative(this.Automation))
            //    : this.NativeElement.FindAll((Interop.UIAutomationClient.TreeScope)treeScope, condition.ToNative(this.Automation.NativeAutomation));
            //var result = new T[nativeFoundElements.Length];
            //for (var i = 0; i < nativeFoundElements.Length; i++)
            //{
            //    var nativeElement = nativeFoundElements.GetElement(i);
            //    var basicElement = new UIA3BasicAutomationElement(this.Automation, nativeElement);
            //    result[i] = wrap(basicElement);
            //}

            //return result;
        }

        public static UiElement FindIndexed(this AutomationElement element, TreeScope treeScope, ConditionBase condition, int index)
        {
            throw new NotImplementedException();
            //var nativeFoundElements = CacheRequest.IsCachingActive
            //    ? this.NativeElement.FindAllBuildCache(treeScope, condition.ToNative(this.Automation.NativeAutomation), CacheRequest.Current.ToNative(this.Automation))
            //    : this.NativeElement.FindAll((Interop.UIAutomationClient.TreeScope)treeScope, condition.ToNative(this.Automation.NativeAutomation));
            //var nativeElement = nativeFoundElements.GetElement(index);
            //if (nativeElement == null)
            //{
            //    return null;
            //}

            //return new UiElement(nativeElement);
        }

        public static T FindIndexed<T>(this AutomationElement element, TreeScope treeScope, ConditionBase condition, int index, Func<AutomationElement, T> wrap)
        {
            throw new NotImplementedException();
            //var nativeFoundElements = CacheRequest.IsCachingActive
            //    ? this.NativeElement.FindAllBuildCache((Interop.UIAutomationClient.TreeScope)treeScope, condition.ToNative(this.Automation.NativeAutomation), CacheRequest.Current.ToNative(this.Automation))
            //    : this.NativeElement.FindAll((Interop.UIAutomationClient.TreeScope)treeScope, condition.ToNative(this.Automation.NativeAutomation));
            //if (nativeFoundElements == null ||
            //    index >= nativeFoundElements.Length)
            //{
            //    return null;
            //}

            //var nativeElement = nativeFoundElements.GetElement(index);
            //if (nativeElement == null)
            //{
            //    return null;
            //}

            //var basicElement = new UIA3BasicAutomationElement(this.Automation, nativeElement);
            //return wrap(basicElement);
        }
    }
}
