namespace Gu.Wpf.UiAutomation.UIA2.Converters
{
    using System;
    using System.Linq;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using UIA = System.Windows.Automation;

    public static class AutomationElementConverter
    {
        public static AutomationElement[] NativeArrayToManaged(AutomationBase automation, object nativeElements)
        {
            if (nativeElements == null)
            {
                return new AutomationElement[0];
            }

            var uia2Automation = (UIA2Automation)automation;
            if (nativeElements is UIA.AutomationElementCollection nativeElementsCollection)
            {
                var retArray = new AutomationElement[nativeElementsCollection.Count];
                for (var i = 0; i < nativeElementsCollection.Count; i++)
                {
                    var nativeElement = nativeElementsCollection[i];
                    var automationElement = uia2Automation.WrapNativeElement(nativeElement);
                    retArray[i] = automationElement;
                }

                return retArray;
            }

            if (nativeElements is UIA.AutomationElement[] nativeElementsArray)
            {
                return nativeElementsArray.Select(uia2Automation.WrapNativeElement).ToArray();
            }

            throw new ArgumentException("Input is neither an AutomationElementCollection nor an AutomationElement[]", nameof(nativeElements));
        }

        public static AutomationElement NativeToManaged(AutomationBase automation, object nativeElement)
        {
            var uia2Automation = (UIA2Automation)automation;
            return uia2Automation.WrapNativeElement((UIA.AutomationElement)nativeElement);
        }

        public static UIA.AutomationElement ToNative(this AutomationElement automationElement)
        {
            if (automationElement == null)
            {
                return null;
            }

            var basicElement = automationElement.BasicAutomationElement as UIA2BasicAutomationElement;
            if (basicElement == null)
            {
                throw new Exception("Element is not an UI2 element");
            }

            return basicElement.NativeElement;
        }
    }
}
