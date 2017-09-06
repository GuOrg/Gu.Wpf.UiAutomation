namespace Gu.Wpf.UiAutomation.UIA3.Identifiers
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;

    public static class TextAttributes
    {
        public static readonly TextAttributeId AnimationStyle = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_AnimationStyleAttributeId, "AnimationStyle");
        public static readonly TextAttributeId AnnotationObjects = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_AnnotationObjectsAttributeId, "AnnotationObjects");
        public static readonly TextAttributeId AnnotationTypes = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_AnnotationTypesAttributeId, "AnnotationTypes");
        public static readonly TextAttributeId BackgroundColor = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_BackgroundColorAttributeId, "BackgroundColor");
        public static readonly TextAttributeId BulletStyle = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_BulletStyleAttributeId, "BulletStyle");
        public static readonly TextAttributeId CapStyle = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_CapStyleAttributeId, "CapStyle");
        public static readonly TextAttributeId CaretBidiMode = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_CaretBidiModeAttributeId, "CaretBidiMode");
        public static readonly TextAttributeId CaretPosition = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_CaretPositionAttributeId, "CaretPosition");
        public static readonly TextAttributeId Culture = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_CultureAttributeId, "Culture", ValueConverter.ToCulture);
        public static readonly TextAttributeId FontName = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_FontNameAttributeId, "FontName");
        public static readonly TextAttributeId FontSize = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_FontSizeAttributeId, "FontSize");
        public static readonly TextAttributeId FontWeight = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_FontWeightAttributeId, "FontWeight");
        public static readonly TextAttributeId ForegroundColor = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_ForegroundColorAttributeId, "ForegroundColor");
        public static readonly TextAttributeId HorizontalTextAlignment = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_HorizontalTextAlignmentAttributeId, "HorizontalTextAlignment");
        public static readonly TextAttributeId IndentationFirstLine = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_IndentationFirstLineAttributeId, "IndentationFirstLine");
        public static readonly TextAttributeId IndentationLeading = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_IndentationLeadingAttributeId, "IndentationLeading");
        public static readonly TextAttributeId IndentationTrailing = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_IndentationTrailingAttributeId, "IndentationTrailing");
        public static readonly TextAttributeId IsActive = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_IsActiveAttributeId, "IsActive");
        public static readonly TextAttributeId IsHidden = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_IsHiddenAttributeId, "IsHidden");
        public static readonly TextAttributeId IsItalic = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_IsItalicAttributeId, "IsItalic");
        public static readonly TextAttributeId IsReadOnly = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_IsReadOnlyAttributeId, "IsReadOnly");
        public static readonly TextAttributeId IsSubscript = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_IsSubscriptAttributeId, "IsSubscript");
        public static readonly TextAttributeId IsSuperscript = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_IsSuperscriptAttributeId, "IsSuperscript");
        public static readonly TextAttributeId Link = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_LinkAttributeId, "Link");
        public static readonly TextAttributeId MarginBottom = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_MarginBottomAttributeId, "MarginBottom");
        public static readonly TextAttributeId MarginLeading = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_MarginLeadingAttributeId, "MarginLeading");
        public static readonly TextAttributeId MarginTop = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_MarginTopAttributeId, "MarginTop");
        public static readonly TextAttributeId MarginTrailing = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_MarginTrailingAttributeId, "MarginTrailing");
        public static readonly TextAttributeId OutlineStyles = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_OutlineStylesAttributeId, "OutlineStyles");
        public static readonly TextAttributeId OverlineColor = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_OverlineColorAttributeId, "OverlineColor");
        public static readonly TextAttributeId OverlineStyle = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_OverlineStyleAttributeId, "OverlineStyle");
        public static readonly TextAttributeId SelectionActiveEnd = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_SelectionActiveEndAttributeId, "SelectionActiveEnd");
        public static readonly TextAttributeId StrikethroughColor = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_StrikethroughColorAttributeId, "StrikethroughColor");
        public static readonly TextAttributeId StrikethroughStyle = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_StrikethroughStyleAttributeId, "StrikethroughStyle");
        public static readonly TextAttributeId StyleId = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_StyleIdAttributeId, "StyleId");
        public static readonly TextAttributeId StyleName = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_StyleNameAttributeId, "StyleName");
        public static readonly TextAttributeId Tabs = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_TabsAttributeId, "Tabs");
        public static readonly TextAttributeId TextFlowDirections = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_TextFlowDirectionsAttributeId, "TextFlowDirections");
        public static readonly TextAttributeId UnderlineColor = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_UnderlineColorAttributeId, "UnderlineColor");
        public static readonly TextAttributeId UnderlineStyle = TextAttributeId.GetOrCreate(Interop.UIAutomationClient.UIA_TextAttributeIds.UIA_UnderlineStyleAttributeId, "UnderlineStyle");
    }
}
