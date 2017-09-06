namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class SelectionItemPattern : SelectionItemPatternBase<Interop.UIAutomationClient.IUIAutomationSelectionItemPattern>
    {
        public static readonly PatternId Pattern = PatternId.GetOrCreate(Interop.UIAutomationClient.UIA_PatternIds.UIA_SelectionItemPatternId, "SelectionItem", AutomationObjectIds.IsSelectionItemPatternAvailableProperty);
        public static readonly PropertyId IsSelectedProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_SelectionItemIsSelectedPropertyId, "IsSelected");
        public static readonly PropertyId SelectionContainerProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_SelectionItemSelectionContainerPropertyId, "SelectionContainer", AutomationElementConverter.NativeToManaged);
        public static readonly EventId ElementAddedToSelectionEvent = EventId.GetOrCreate(Interop.UIAutomationClient.UIA_EventIds.UIA_SelectionItem_ElementAddedToSelectionEventId, "ElementAddedToSelection");
        public static readonly EventId ElementRemovedFromSelectionEvent = EventId.GetOrCreate(Interop.UIAutomationClient.UIA_EventIds.UIA_SelectionItem_ElementRemovedFromSelectionEventId, "ElementRemovedFromSelection");
        public static readonly EventId ElementSelectedEvent = EventId.GetOrCreate(Interop.UIAutomationClient.UIA_EventIds.UIA_SelectionItem_ElementSelectedEventId, "ElementSelected");

        public SelectionItemPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationSelectionItemPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void AddToSelection()
        {
            Com.Call(() => this.NativePattern.AddToSelection());
        }

        public override void RemoveFromSelection()
        {
            Com.Call(() => this.NativePattern.RemoveFromSelection());
        }

        public override void Select()
        {
            Com.Call(() => this.NativePattern.Select());
        }
    }
}
