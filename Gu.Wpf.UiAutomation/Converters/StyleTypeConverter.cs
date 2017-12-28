namespace Gu.Wpf.UiAutomation.UIA3.Converters
{
    using System;

    public static class StyleTypeConverter
    {
        public static object ToStyleType(object nativeStyleType)
        {
            switch ((int)nativeStyleType)
            {
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_BulletedList:
                    return StyleType.BulletedList;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Custom:
                    return StyleType.Custom;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Emphasis:
                    return StyleType.Emphasis;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading1:
                    return StyleType.Heading1;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading2:
                    return StyleType.Heading2;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading3:
                    return StyleType.Heading3;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading4:
                    return StyleType.Heading4;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading5:
                    return StyleType.Heading5;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading6:
                    return StyleType.Heading6;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading7:
                    return StyleType.Heading7;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading8:
                    return StyleType.Heading8;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading9:
                    return StyleType.Heading9;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Normal:
                    return StyleType.Normal;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_NumberedList:
                    return StyleType.NumberedList;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Quote:
                    return StyleType.Quote;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Subtitle:
                    return StyleType.Subtitle;
                case Interop.UIAutomationClient.UIA_StyleIds.StyleId_Title:
                    return StyleType.Title;
                default:
                    throw new NotSupportedException();
            }
        }

        public static object ToStyleTypeNative(StyleType styleType)
        {
            switch (styleType)
            {
                case StyleType.BulletedList:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_BulletedList;
                case StyleType.Custom:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Custom;
                case StyleType.Emphasis:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Emphasis;
                case StyleType.Heading1:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading1;
                case StyleType.Heading2:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading2;
                case StyleType.Heading3:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading3;
                case StyleType.Heading4:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading4;
                case StyleType.Heading5:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading5;
                case StyleType.Heading6:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading6;
                case StyleType.Heading7:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading7;
                case StyleType.Heading8:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading8;
                case StyleType.Heading9:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Heading9;
                case StyleType.Normal:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Normal;
                case StyleType.NumberedList:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_NumberedList;
                case StyleType.Quote:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Quote;
                case StyleType.Subtitle:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Subtitle;
                case StyleType.Title:
                    return Interop.UIAutomationClient.UIA_StyleIds.StyleId_Title;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
