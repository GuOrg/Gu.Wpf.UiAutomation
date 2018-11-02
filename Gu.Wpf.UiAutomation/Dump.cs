namespace Gu.Wpf.UiAutomation
{
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Automation;

    /// <summary>
    /// For dumping the automation tree of a control.
    /// </summary>
    public static class Dump
    {
        /// <summary>
        /// Dump the visual tree of <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The <see cref="UiElement"/>.</param>
        /// <param name="allPropertiesAndPatterns">If all automation properties and patterns should be written to the result.</param>
        /// <returns>A string representing the automation tree of <paramref name="element"/>.</returns>
        public static string Recursive(UiElement element, bool allPropertiesAndPatterns = false)
        {
            return Recursive(element.AutomationElement);
        }

        /// <summary>
        /// Dump the visual tree of <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The <see cref="UiElement"/>.</param>
        /// <param name="allPropertiesAndPatterns">If all automation properties and patterns should be written to the result.</param>
        /// <returns>A string representing the automation tree of <paramref name="element"/>.</returns>
        public static string Recursive(AutomationElement element, bool allPropertiesAndPatterns = false)
        {
            var builder = new StringBuilder();
            using (var writer = new IndentedTextWriter(new StringWriter(builder)))
            {
                Recursive(element, writer, allPropertiesAndPatterns);
            }

            return builder.ToString();
        }

        private static void Recursive(AutomationElement element, IndentedTextWriter writer, bool allPropertiesAndPatterns = false)
        {
            PropertiesAndPatterns(element, writer, allPropertiesAndPatterns);
            writer.Indent++;
            foreach (var child in element.Children())
            {
                Recursive(child, writer, allPropertiesAndPatterns);
            }

            writer.Indent--;
        }

        private static void PropertiesAndPatterns(AutomationElement element, IndentedTextWriter writer, bool all = true)
        {
            if (all)
            {
                foreach (var pattern in element.GetSupportedPatterns())
                {
                    writer.WriteLine(pattern.ProgrammaticName);
                    var currentPattern = element.GetCurrentPattern(pattern);
                    writer.WriteLine(currentPattern);
                    var currentProperty = currentPattern.GetType().GetProperty("Current");
                    if (currentProperty != null)
                    {
                        var value = currentProperty.GetValue(currentPattern);
                        foreach (var property in value.GetType().GetProperties())
                        {
                            writer.WriteLine($"{property.Name} {property.GetValue(value)}");
                        }
                    }
                }

                foreach (var property in element.GetSupportedProperties().OrderBy(x => x.ProgrammaticName))
                {
                    writer.WriteLine($"{property.ProgrammaticName.TrimStart("AutomationElementIdentifiers.").TrimEnd("Property")} {element.GetCurrentPropertyValue(property)}");
                }

                writer.WriteLine();
            }
            else
            {
                var info = element.Current;
                writer.WriteLine($"ControlType: {info.ControlType.ProgrammaticName} (LocalizedControlType: {info.LocalizedControlType})");
                writer.WriteLine($"ClassName: {info.ClassName}");
                writer.WriteLine($"Name: {info.Name}");
                writer.WriteLine($"AutomationId: {info.AutomationId}");
                writer.WriteLine($"IsContentElement: {info.IsContentElement} IsControlElement: {info.IsControlElement}");
                writer.WriteLine($"Properties: {string.Join(", ", element.GetSupportedProperties().Select(x => x.ProgrammaticName.TrimStart("AutomationElementIdentifiers.").TrimEnd("Property")).OrderBy(x => x))}");
                writer.WriteLine($"Patterns: {string.Join(", ", element.GetSupportedPatterns().Select(x => x.ProgrammaticName.TrimEnd("Identifiers.Pattern").TrimEnd("Pattern")).OrderBy(x => x))}");
                writer.WriteLine();
            }
        }
    }
}
