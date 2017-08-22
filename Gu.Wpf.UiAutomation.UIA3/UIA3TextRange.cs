namespace Gu.Wpf.UiAutomation.UIA3
{
    using System;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Shapes;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using UIA = Interop.UIAutomationClient;

    public class UIA3TextRange : ITextRange
    {
        public UIA3Automation Automation { get; }

        public UIA.IUIAutomationTextRange NativeRange { get; }

        internal UIA3TextRange(UIA3Automation automation, UIA.IUIAutomationTextRange nativeRange)
        {
            this.Automation = automation;
            this.NativeRange = nativeRange;
        }

        public void AddToSelection()
        {
            ComCallWrapper.Call(() => this.NativeRange.AddToSelection());
        }

        public ITextRange Clone()
        {
            var clonedTextRangeNative = ComCallWrapper.Call(() => this.NativeRange.Clone());
            return TextRangeConverter.NativeToManaged(this.Automation, clonedTextRangeNative);
        }

        public bool Compare(ITextRange range)
        {
            var nativeRange = this.ToNativeRange(range);
            return ComCallWrapper.Call(() => this.NativeRange.Compare(nativeRange)) != 0;
        }

        public int CompareEndpoints(TextPatternRangeEndpoint srcEndPoint, ITextRange targetRange, TextPatternRangeEndpoint targetEndPoint)
        {
            var nativeRange = this.ToNativeRange(targetRange);
            return ComCallWrapper.Call(() => this.NativeRange.CompareEndpoints((UIA.TextPatternRangeEndpoint)srcEndPoint, nativeRange, (UIA.TextPatternRangeEndpoint)targetEndPoint));
        }

        public void ExpandToEnclosingUnit(TextUnit textUnit)
        {
            ComCallWrapper.Call(() => this.NativeRange.ExpandToEnclosingUnit((UIA.TextUnit)textUnit));
        }

        public ITextRange FindAttribute(TextAttributeId attribute, object value, bool backward)
        {
            var nativeValue = ValueConverter.ToNative(value);
            var nativeTextRange = ComCallWrapper.Call(() => this.NativeRange.FindAttribute(attribute.Id, nativeValue, backward.ToInt()));
            return TextRangeConverter.NativeToManaged(this.Automation, nativeTextRange);
        }

        public ITextRange FindText(string text, bool backward, bool ignoreCase)
        {
            var nativeTextRange = ComCallWrapper.Call(() => this.NativeRange.FindText(text, backward.ToInt(), ignoreCase.ToInt()));
            return TextRangeConverter.NativeToManaged(this.Automation, nativeTextRange);
        }

        public object GetAttributeValue(TextAttributeId attribute)
        {
            var nativeValue = ComCallWrapper.Call(() => this.NativeRange.GetAttributeValue(attribute.Id));
            return attribute.Convert<object>(this.Automation, nativeValue);
        }

        public Rectangle[] GetBoundingRectangles()
        {
            var unrolledRects = ComCallWrapper.Call(() => this.NativeRange.GetBoundingRectangles());
            if (unrolledRects == null)
            {
                return null;
            }

            // If unrolledRects is somehow not a multiple of 4, we still will not 
            // overrun it, since (x / 4) * 4 <= x for C# integer math.
            var result = new Rectangle[unrolledRects.Length / 4];
            for (var i = 0; i < result.Length; i++)
            {
                var j = i * 4;
                result[i] = new Rectangle(unrolledRects[j], unrolledRects[j + 1], unrolledRects[j + 2], unrolledRects[j + 3]);
            }

            return result;
        }

        public AutomationElement[] GetChildren()
        {
            var nativeChildren = ComCallWrapper.Call(() => this.NativeRange.GetChildren());
            return AutomationElementConverter.NativeArrayToManaged(this.Automation, nativeChildren);
        }

        public AutomationElement GetEnclosingElement()
        {
            var nativeElement = ComCallWrapper.Call(() => this.NativeRange.GetEnclosingElement());
            return AutomationElementConverter.NativeToManaged(this.Automation, nativeElement);
        }

        public string GetText(int maxLength)
        {
            return ComCallWrapper.Call(() => this.NativeRange.GetText(maxLength));
        }

        public int Move(TextUnit unit, int count)
        {
            return ComCallWrapper.Call(() => this.NativeRange.Move((UIA.TextUnit)unit, count));
        }

        public void MoveEndpointByRange(TextPatternRangeEndpoint srcEndPoint, ITextRange targetRange, TextPatternRangeEndpoint targetEndPoint)
        {
            var nativeRange = this.ToNativeRange(targetRange);
            ComCallWrapper.Call(() => this.NativeRange.MoveEndpointByRange((UIA.TextPatternRangeEndpoint)srcEndPoint, nativeRange, (UIA.TextPatternRangeEndpoint)targetEndPoint));
        }

        public int MoveEndpointByUnit(TextPatternRangeEndpoint endpoint, TextUnit unit, int count)
        {
            return ComCallWrapper.Call(() => this.NativeRange.MoveEndpointByUnit((UIA.TextPatternRangeEndpoint)endpoint, (UIA.TextUnit)unit, count));
        }

        public void RemoveFromSelection()
        {
            ComCallWrapper.Call(() => this.NativeRange.RemoveFromSelection());
        }

        public void ScrollIntoView(bool alignToTop)
        {
            ComCallWrapper.Call(() => this.NativeRange.ScrollIntoView(alignToTop.ToInt()));
        }

        public void Select()
        {
            ComCallWrapper.Call(() => this.NativeRange.Select());
        }

        public UIA3TextRange2 AsTextRange2()
        {
            var nativeRange2 = (UIA.IUIAutomationTextRange2)this.NativeRange;
            return TextRangeConverter.NativeToManaged(this.Automation, nativeRange2);
        }

        protected UIA.IUIAutomationTextRange ToNativeRange(ITextRange range)
        {
            var concreteTextRange = range as UIA3TextRange;
            if (concreteTextRange == null)
            {
                throw new Exception("TextRange is no UIA3 TextRange");
            }

            return concreteTextRange.NativeRange;
        }
    }
}
