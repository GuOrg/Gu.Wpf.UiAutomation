namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class SelectionItemPattern : SelectionItemPatternBase<UIA.IUIAutomationSelectionItemPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_SelectionItemPatternId, "SelectionItem", AutomationObjectIds.IsSelectionItemPatternAvailableProperty);
        public static readonly PropertyId IsSelectedProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_SelectionItemIsSelectedPropertyId, "IsSelected");
        public static readonly PropertyId SelectionContainerProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_SelectionItemSelectionContainerPropertyId, "SelectionContainer").SetConverter(AutomationElementConverter.NativeToManaged);
        public static readonly EventId ElementAddedToSelectionEvent = EventId.Register(UIA.UIA_EventIds.UIA_SelectionItem_ElementAddedToSelectionEventId, "ElementAddedToSelection");
        public static readonly EventId ElementRemovedFromSelectionEvent = EventId.Register(UIA.UIA_EventIds.UIA_SelectionItem_ElementRemovedFromSelectionEventId, "ElementRemovedFromSelection");
        public static readonly EventId ElementSelectedEvent = EventId.Register(UIA.UIA_EventIds.UIA_SelectionItem_ElementSelectedEventId, "ElementSelected");

        public SelectionItemPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationSelectionItemPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void AddToSelection()
        {
            ComCallWrapper.Call(() => this.NativePattern.AddToSelection());
        }

        public override void RemoveFromSelection()
        {
            ComCallWrapper.Call(() => this.NativePattern.RemoveFromSelection());
        }

        public override void Select()
        {
            ComCallWrapper.Call(() => this.NativePattern.Select());
        }
    }
}
