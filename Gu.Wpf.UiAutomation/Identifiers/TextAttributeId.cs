namespace Gu.Wpf.UiAutomation
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

        public static TextAttributeId Register(int id, string name)
        {
            return RegisterTextAttribute(id, name);
        }

        public static TextAttributeId Find(int id)
        {
            return FindTextAttribute(id);
        }

        public TextAttributeId SetConverter(Func<AutomationBase, object, object> convertMethod)
        {
            return this.SetConverter<TextAttributeId>(convertMethod);
        }
    }
}
