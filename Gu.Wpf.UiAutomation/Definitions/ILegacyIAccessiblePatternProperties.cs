namespace Gu.Wpf.UiAutomation
{
    public interface ILegacyIAccessiblePatternProperties
    {
        PropertyId ChildId { get; }

        PropertyId DefaultAction { get; }

        PropertyId Description { get; }

        PropertyId Help { get; }

        PropertyId KeyboardShortcut { get; }

        PropertyId Name { get; }

        PropertyId Role { get; }

        PropertyId Selection { get; }

        PropertyId State { get; }

        PropertyId Value { get; }
    }
}