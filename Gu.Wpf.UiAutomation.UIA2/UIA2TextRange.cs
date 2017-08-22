namespace Gu.Wpf.UiAutomation.UIA2
{
    using System;
    using System.Linq;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Shapes;
    using Gu.Wpf.UiAutomation.UIA2.Converters;
    using UIA = System.Windows.Automation;

    public class UIA2TextRange : ITextRange
    {
        public UIA2Automation Automation { get; }

        public UIA.Text.TextPatternRange NativeRange { get; }

        public UIA2TextRange(UIA2Automation automation, UIA.Text.TextPatternRange nativeRange)
        {
            this.Automation = automation;
            this.NativeRange = nativeRange;
        }

        public void AddToSelection()
        {
            this.NativeRange.AddToSelection();
        }

        public ITextRange Clone()
        {
            var clonedTextRangeNative = this.NativeRange.Clone();
            return TextRangeConverter.NativeToManaged(this.Automation, clonedTextRangeNative);
        }

        public bool Compare(ITextRange range)
        {
            var nativeRange = this.ToNativeRange(range);
            return this.NativeRange.Compare(nativeRange);
        }

        public int CompareEndpoints(TextPatternRangeEndpoint srcEndPoint, ITextRange targetRange, TextPatternRangeEndpoint targetEndPoint)
        {
            var nativeRange = this.ToNativeRange(targetRange);
            return this.NativeRange.CompareEndpoints((UIA.Text.TextPatternRangeEndpoint)srcEndPoint, nativeRange, (UIA.Text.TextPatternRangeEndpoint)targetEndPoint);
        }

        public void ExpandToEnclosingUnit(TextUnit textUnit)
        {
            this.NativeRange.ExpandToEnclosingUnit((UIA.Text.TextUnit)textUnit);
        }

        public ITextRange FindAttribute(TextAttributeId attribute, object value, bool backward)
        {
            var nativeValue = ValueConverter.ToNative(value);
            var nativeAttribute = UIA.AutomationTextAttribute.LookupById(attribute.Id);
            var nativeTextRange = this.NativeRange.FindAttribute(nativeAttribute, nativeValue, backward);
            return TextRangeConverter.NativeToManaged(this.Automation, nativeTextRange);
        }

        public ITextRange FindText(string text, bool backward, bool ignoreCase)
        {
            var nativeTextRange = this.NativeRange.FindText(text, backward, ignoreCase);
            return TextRangeConverter.NativeToManaged(this.Automation, nativeTextRange);
        }

        public object GetAttributeValue(TextAttributeId attribute)
        {
            var nativeAttribute = UIA.AutomationTextAttribute.LookupById(attribute.Id);
            var nativeValue = this.NativeRange.GetAttributeValue(nativeAttribute);
            return attribute.Convert<object>(this.Automation, nativeValue);
        }

        public Rectangle[] GetBoundingRectangles()
        {
            var unrolledRects = this.NativeRange.GetBoundingRectangles();
            return unrolledRects?.Select(r => (Rectangle)ValueConverter.ToRectangle(r)).ToArray();
        }

        public AutomationElement[] GetChildren()
        {
            var nativeChildren = this.NativeRange.GetChildren();
            return AutomationElementConverter.NativeArrayToManaged(this.Automation, nativeChildren);
        }

        public AutomationElement GetEnclosingElement()
        {
            var nativeElement = this.NativeRange.GetEnclosingElement();
            return AutomationElementConverter.NativeToManaged(this.Automation, nativeElement);
        }

        public string GetText(int maxLength)
        {
            return this.NativeRange.GetText(maxLength);
        }

        public int Move(TextUnit unit, int count)
        {
            return this.NativeRange.Move((UIA.Text.TextUnit)unit, count);
        }

        public void MoveEndpointByRange(TextPatternRangeEndpoint srcEndPoint, ITextRange targetRange, TextPatternRangeEndpoint targetEndPoint)
        {
            var nativeRange = this.ToNativeRange(targetRange);
            this.NativeRange.MoveEndpointByRange((UIA.Text.TextPatternRangeEndpoint)srcEndPoint, nativeRange, (UIA.Text.TextPatternRangeEndpoint)targetEndPoint);
        }

        public int MoveEndpointByUnit(TextPatternRangeEndpoint endpoint, TextUnit unit, int count)
        {
            return this.NativeRange.MoveEndpointByUnit((UIA.Text.TextPatternRangeEndpoint)endpoint, (UIA.Text.TextUnit)unit, count);
        }

        public void RemoveFromSelection()
        {
            this.NativeRange.RemoveFromSelection();
        }

        public void ScrollIntoView(bool alignToTop)
        {
            this.NativeRange.ScrollIntoView(alignToTop);
        }

        public void Select()
        {
            this.NativeRange.Select();
        }

        protected UIA.Text.TextPatternRange ToNativeRange(ITextRange range)
        {
            var concreteTextRange = range as UIA2TextRange;
            if (concreteTextRange == null)
            {
                throw new Exception("TextRange is no UIA2 TextRange");
            }

            return concreteTextRange.NativeRange;
        }
    }
}
