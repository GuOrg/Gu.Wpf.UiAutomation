namespace Gu.Wpf.UiAutomation.UIA3.Converters
{
    using System;
    using System.Collections.Generic;

    public static class AutomationElementConverter
    {
        public static IReadOnlyList<UiElement> NativeArrayToManaged(AutomationBase automation, object nativeElements)
        {
            if (nativeElements == null)
            {
                return new UiElement[0];
            }

            var uia3Automation = (UIA3Automation)automation;
            var nativeElementsCasted = (Interop.UIAutomationClient.IUIAutomationElementArray)nativeElements;
            var retArray = new UiElement[nativeElementsCasted.Length];
            for (var i = 0; i < nativeElementsCasted.Length; i++)
            {
                var nativeElement = nativeElementsCasted.GetElement(i);
                var automationElement = uia3Automation.WrapNativeElement(nativeElement);
                retArray[i] = automationElement;
            }

            return retArray;
        }

        public static UiElement NativeToManaged(AutomationBase automation, object nativeElement)
        {
            var uia3Automation = (UIA3Automation)automation;
            return uia3Automation.WrapNativeElement((Interop.UIAutomationClient.IUIAutomationElement)nativeElement);
        }

        public static Interop.UIAutomationClient.IUIAutomationElement ToNative(this UiElement uiElement)
        {
            if (uiElement == null)
            {
                return null;
            }

            var basicElement = uiElement.AutomationElement as UIA3BasicAutomationElement;
            if (basicElement == null)
            {
                throw new Exception("Element is not an UIA3 element");
            }

            return basicElement.NativeElement;
        }
    }
}
