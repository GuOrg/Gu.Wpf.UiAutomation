using Gu.Wpf.UiAutomation.Identifiers;
using Gu.Wpf.UiAutomation.Patterns;
using Gu.Wpf.UiAutomation.UIA2.Converters;
using Gu.Wpf.UiAutomation.UIA2.Identifiers;
using UIA = System.Windows.Automation;

namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    public class TableItemPattern : TableItemPatternBase<UIA.TableItemPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA2, UIA.TableItemPattern.Pattern.Id, "TableItem", AutomationObjectIds.IsTableItemPatternAvailableProperty);
        public static readonly PropertyId ColumnHeaderItemsProperty = PropertyId.Register(AutomationType.UIA2, UIA.TableItemPattern.ColumnHeaderItemsProperty.Id, "ColumnHeaderItems").SetConverter(AutomationElementConverter.NativeArrayToManaged);
        public static readonly PropertyId RowHeaderItemsProperty = PropertyId.Register(AutomationType.UIA2, UIA.TableItemPattern.RowHeaderItemsProperty.Id, "RowHeaderItems").SetConverter(AutomationElementConverter.NativeArrayToManaged);

        public TableItemPattern(BasicAutomationElementBase basicAutomationElement, UIA.TableItemPattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }
    }

    public class TableItemPatternProperties : ITableItemPatternProperties
    {
        public PropertyId ColumnHeaderItems => TableItemPattern.ColumnHeaderItemsProperty;
        public PropertyId RowHeaderItems => TableItemPattern.RowHeaderItemsProperty;
    }
}
