namespace Gu.Wpf.UiAutomation.Identifiers
{
    using System;

    /// <summary>
    /// A wrapper around text attribute ids
    /// </summary>
    public class TextAttributeId : ConvertibleIdentifierBase
    {
        public TextAttributeId(int id, string name)
            : base(id, name)
        {
        }

        public TextAttributeId SetConverter(Func<AutomationBase, object, object> convertMethod)
        {
            return this.SetConverter<TextAttributeId>(convertMethod);
        }

        public static TextAttributeId Register(AutomationType automationType, int id, string name)
        {
            return RegisterTextAttribute(automationType, id, name);
        }

        public static TextAttributeId Find(AutomationType automationType, int id)
        {
            return FindTextAttribute(automationType, id);
        }
    }
}
