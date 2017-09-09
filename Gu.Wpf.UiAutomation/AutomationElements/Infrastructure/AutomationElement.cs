namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Gu.Wpf.UiAutomation.Overlay;
    using GdiColor = System.Drawing.Color;
    using WpfColor = System.Windows.Media.Color;

    /// <summary>
    /// Wrapper object for each ui element which is automatable.
    /// </summary>
    public partial class AutomationElement : IEquatable<AutomationElement>
    {
        public AutomationElement(BasicAutomationElementBase basicAutomationElement)
        {
            this.BasicAutomationElement = basicAutomationElement ?? throw new ArgumentNullException(nameof(basicAutomationElement));
        }

        /// <summary>
        /// Object which contains the native wrapper element (UIA2 or UIA3) for this element.
        /// </summary>
        public BasicAutomationElementBase BasicAutomationElement { get; }

        /// <summary>
        /// Get the parent <see cref="AutomationElement"/>
        /// </summary>
        public AutomationElement Parent => this.Automation
                                               .TreeWalkerFactory
                                               .GetRawViewWalker()
                                               .GetParent(this);

        /// <summary>
        /// Get the parent <see cref="AutomationElement"/>
        /// </summary>
        public Window Window
        {
            get
            {
                if (this.TryGetWindow(out var window))
                {
                    return window;
                }

                throw new InvalidOperationException("Did not find a parent window.");
            }
        }

        /// <summary>
        /// The current used automation object.
        /// </summary>
        public double ActualWidth => this.Bounds.Width;

        /// <summary>
        /// The current used automation object.
        /// </summary>
        public double ActualHeight => this.Bounds.Height;

        /// <summary>
        /// The current used automation object.
        /// </summary>
        public Rect Bounds
        {
            get
            {
                if (this.Properties.BoundingRectangle.TryGetValue(out var rect))
                {
                    return rect;
                }

                return Rect.Empty;
            }
        }

        /// <summary>
        /// The current used automation object.
        /// </summary>
        public AutomationBase Automation => this.BasicAutomationElement.Automation;

        /// <summary>
        /// Shortcut to the condition factory for the current automation.
        /// </summary>
        public ConditionFactory ConditionFactory => this.BasicAutomationElement.Automation.ConditionFactory;

        /// <summary>
        /// Standard UIA patterns of this element.
        /// </summary>
        public AutomationElementPatternValuesBase Patterns => this.BasicAutomationElement.Patterns;

        /// <summary>
        /// Standard UIA properties of this element.
        /// </summary>
        public AutomationElementPropertyValues Properties => this.BasicAutomationElement.Properties;

        public string ItemStatus => this.Properties.ItemStatus.Value;

        public string HelpText => this.Properties.HelpText.Value;

        /// <summary>
        /// Gets the cached children for this element.
        /// </summary>
        public IReadOnlyList<AutomationElement> CachedChildren => this.BasicAutomationElement.GetCachedChildren();

        /// <summary>
        /// Gets the cached parent for this element.
        /// </summary>
        public AutomationElement CachedParent => this.BasicAutomationElement.GetCachedParent();

        /// <summary>
        /// The direct framework type of the element.
        /// Results in "FrameworkType.Unknown" if it couldn't be resolved.
        /// </summary>
        public FrameworkType FrameworkType
        {
            get
            {
                var hasProperty = this.Properties.FrameworkId.TryGetValue(out string currentFrameworkId);
                return hasProperty ? FrameworkIds.ConvertToFrameworkType(currentFrameworkId) : FrameworkType.Unknown;
            }
        }

        /// <summary>
        /// The automation id of the element.
        /// </summary>
        public string AutomationId => this.Properties.AutomationId.Value;

        /// <summary>
        /// The name of the element.
        /// </summary>
        public string Name => this.Properties.Name.Value;

        /// <summary>
        /// The class name of the element.
        /// </summary>
        public string ClassName => this.Properties.ClassName.Value;

        /// <summary>
        /// The control type of the element.
        /// </summary>
        public ControlType ControlType => this.Properties.ControlType.Value;

        /// <summary>
        /// Flag if the element off-screen or on-screen(visible).
        /// </summary>
        public bool IsOffscreen => this.Properties.IsOffscreen.Value;

        public bool TryGetWindow(out Window window)
        {
            if (this is Window w)
            {
                window = w;
                return true;
            }

            var parent = this.Parent;
            while (parent != null)
            {
                if (parent.Properties.ControlType.TryGetValue(out var controlType) &&
                    controlType == ControlType.Window)
                {
                    window = parent.AsWindow(parent.Parent?.Name?.StartsWith("Desktop") == true);
                    return true;
                }

                parent = parent.Parent;
            }

            window = null;
            return false;
        }

        /// <summary>
        /// Draws a red highlight around the element.
        /// </summary>
        public AutomationElement DrawHighlight()
        {
            return this.DrawHighlight(Colors.Red);
        }

        /// <summary>
        /// Draws a manually colored highlight around the element.
        /// </summary>
        public AutomationElement DrawHighlight(WpfColor color)
        {
            return this.DrawHighlight(blocking: true, color: color, duration: TimeSpan.FromMilliseconds(2000));
        }

        /// <summary>
        /// Draws a manually colored highlight around the element.
        /// </summary>
        public AutomationElement DrawHighlight(GdiColor color)
        {
            return this.DrawHighlight(blocking: true, color: color, duration: TimeSpan.FromMilliseconds(2000));
        }

        /// <summary>
        /// Draw a highlight around the element with the given settings.
        /// </summary>
        /// <param name="blocking">Flag to indicate if further execution waits until the highlight is removed.</param>
        /// <param name="color">The color to draw the highlight.</param>
        /// <param name="duration">The duration (im ms) how long the highlight is shown.</param>
        /// <remarks>Override for winforms color.</remarks>
        public AutomationElement DrawHighlight(bool blocking, GdiColor color, TimeSpan duration)
        {
            return this.DrawHighlight(blocking, WpfColor.FromArgb(color.A, color.R, color.G, color.B), duration);
        }

        /// <summary>
        /// Draw a highlight around the element with the given settings.
        /// </summary>
        /// <param name="blocking">Flag to indicate if further execution waits until the highlight is removed.</param>
        /// <param name="color">The color to draw the highlight.</param>
        /// <param name="duration">The duration (im ms) how long the highlight is shown.</param>
        public AutomationElement DrawHighlight(bool blocking, WpfColor color, TimeSpan duration)
        {
            var rectangle = this.Properties.BoundingRectangle.Value;
            if (!rectangle.IsZeroes())
            {
                if (blocking)
                {
                    OverlayRectangleWindow.ShowBlocking(rectangle, color, duration);
                }
                else
                {
                    OverlayRectangleWindow.Show(rectangle, color, duration);
                }
            }

            return this;
        }

        /// <summary>
        /// Captures the object as screenshot in WinForms Bitmap format.
        /// </summary>
        public System.Drawing.Bitmap Capture()
        {
            return UiAutomation.Capture.Rectangle(this.Bounds);
        }

        /// <summary>
        /// Captures the object as screenshot in WPF BitmapImage format.
        /// </summary>
        public BitmapImage CaptureWpf()
        {
            return UiAutomation.Capture.RectangleWpf(this.Bounds);
        }

        /// <summary>
        /// Captures the object as screenshot directly into the given file.
        /// </summary>
        /// <param name="filePath">The filepath where the screenshot should be saved.</param>
        public void CaptureToFile(string filePath)
        {
            UiAutomation.Capture.RectangleToFile(this.Bounds, filePath);
        }

        /// <summary>
        /// Gets a clickable point of the element.
        /// </summary>
        /// <exception cref="NoClickablePointException">Thrown when no clickable point was found</exception>
        public Point GetClickablePoint()
        {
            return this.BasicAutomationElement.GetClickablePoint();
        }

        /// <summary>
        /// Tries to get a clickable point of the element.
        /// </summary>
        /// <param name="point">The clickable point or null, if no point was found</param>
        /// <returns>True if a point was found, false otherwise</returns>
        public bool TryGetClickablePoint(out Point point)
        {
            return this.BasicAutomationElement.TryGetClickablePoint(out point);
        }

        /// <summary>
        /// Registers the given event
        /// </summary>
        public IDisposable SubscribeToEvent(EventId @event, TreeScope treeScope, Action<AutomationElement, EventId> action)
        {
            if (Equals(@event, EventId.NotSupportedByFramework))
            {
                throw new NotSupportedByFrameworkException();
            }

            return this.BasicAutomationElement.SubscribeToEvent(@event, treeScope, action);
        }

        /// <summary>
        /// Registers a property changed event with the given property
        /// </summary>
        public IDisposable SubscribeToPropertyChangedEvent(TreeScope treeScope, Action<AutomationElement, PropertyId, object> action, params PropertyId[] properties)
        {
            return this.BasicAutomationElement.SubscribeToPropertyChangedEvent(treeScope, action, properties);
        }

        /// <summary>
        /// Registers a structure changed event
        /// </summary>
        public IDisposable SubscribeToStructureChangedEvent(TreeScope treeScope, Action<AutomationElement, StructureChangeType, int[]> action)
        {
            return this.BasicAutomationElement.SubscribeToStructureChangedEvent(treeScope, action);
        }

        /// <summary>
        /// Gets the available patterns for an element via properties
        /// </summary>
        public IReadOnlyList<PatternId> GetSupportedPatterns()
        {
            return this.Automation.PatternLibrary.AllForCurrentFramework.Where(this.IsPatternSupported).ToArray();
        }

        /// <summary>
        /// Checks if the given pattern is available for the element via properties
        /// </summary>
        public bool IsPatternSupported(PatternId pattern)
        {
            if (Equals(pattern, PatternId.NotSupportedByFramework))
            {
                return false;
            }

            if (pattern.AvailabilityProperty == null)
            {
                throw new ArgumentException("Pattern doesn't have an AvailabilityProperty");
            }

            var success = this.BasicAutomationElement.TryGetPropertyValue(pattern.AvailabilityProperty, out bool isPatternAvailable);
            return success && isPatternAvailable;
        }

        /// <summary>
        /// Gets the available patterns for an element via UIA method.
        /// Does not work with cached elements and might be unreliable.
        /// </summary>
        public IReadOnlyList<PatternId> GetSupportedPatternsDirect()
        {
            return this.BasicAutomationElement.GetSupportedPatterns();
        }

        /// <summary>
        /// Checks if the given pattern is available for the element via UIA method.
        /// Does not work with cached elements and might be unreliable.
        /// </summary>
        public bool IsPatternSupportedDirect(PatternId pattern)
        {
            return this.GetSupportedPatternsDirect().Contains(pattern);
        }

        /// <summary>
        /// Gets the available properties for an element via UIA method.
        /// Does not work with cached elements and might be unreliable.
        /// </summary>
        public IReadOnlyList<PropertyId> GetSupportedPropertiesDirect()
        {
            return this.BasicAutomationElement.GetSupportedProperties();
        }

        /// <summary>
        /// Method to check if the element supports the given property via UIA method.
        /// Does not work with cached elements and might be unreliable.
        /// </summary>
        public bool IsPropertySupportedDirect(PropertyId property)
        {
            return this.GetSupportedPropertiesDirect().Contains(property);
        }

        /// <summary>
        /// Compares two elements.
        /// </summary>
        public bool Equals(AutomationElement other)
        {
            return other != null && this.Automation.Compare(this, other);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as AutomationElement);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.BasicAutomationElement?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// Overrides the string representation of the element with something usefull
        /// </summary>
        public override string ToString()
        {
            return $"AutomationId:{this.Properties.AutomationId.ValueOrDefault("MISSING")}, Name:{this.Properties.Name.ValueOrDefault("MISSING")}, ControlType:{this.Properties.LocalizedControlType.ValueOrDefault("MISSING")}, FrameworkId:{this.Properties.FrameworkId.ValueOrDefault("MISSING")}";
        }

        public Button AsButton() => new Button(this.BasicAutomationElement);

        public CheckBox AsCheckBox() => new CheckBox(this.BasicAutomationElement);

        public ToggleButton AsToggleButton() => new ToggleButton(this.BasicAutomationElement);

        public ComboBox AsComboBox() => new ComboBox(this.BasicAutomationElement);

        public Label AsLabel() => new Label(this.BasicAutomationElement);

        public TextBlock AsTextBlock() => new TextBlock(this.BasicAutomationElement);

        public GroupBox AsGroupBox() => new GroupBox(this.BasicAutomationElement);

        public Expander AsExpander() => new Expander(this.BasicAutomationElement);

        public GridRow AsGridRow() => new GridRow(this.BasicAutomationElement);

        public ListBox AsListBox() => new ListBox(this.BasicAutomationElement);

        public ListView AsListView() => new ListView(this.BasicAutomationElement);

        public DataGrid AsDataGrid() => new DataGrid(this.BasicAutomationElement);

        public ListBoxItem AsListBoxItem() => new ListBoxItem(this.BasicAutomationElement);

        public GridCell AsGridCell() => new GridCell(this.BasicAutomationElement);

        public Menu AsMenu() => new Menu(this.BasicAutomationElement);

        public ContextMenu AsContextMenu() => new ContextMenu(this.BasicAutomationElement);

        public MenuItem AsMenuItem() => new MenuItem(this.BasicAutomationElement);

        public ProgressBar AsProgressBar() => new ProgressBar(this.BasicAutomationElement);

        public RadioButton AsRadioButton() => new RadioButton(this.BasicAutomationElement);

        public Slider AsSlider() => new Slider(this.BasicAutomationElement);

        public TabControl AsTabControl() => new TabControl(this.BasicAutomationElement);

        public TabItem AsTabItem() => new TabItem(this.BasicAutomationElement);

        public TextBox AsTextBox() => new TextBox(this.BasicAutomationElement);

        public Thumb AsThumb() => new Thumb(this.BasicAutomationElement);

        public TitleBar AsTitleBar() => new TitleBar(this.BasicAutomationElement);

        public TreeView AsTreeView() => new TreeView(this.BasicAutomationElement);

        public TreeViewItem AsTreeViewItem() => new TreeViewItem(this.BasicAutomationElement);

        public HorizontalScrollBar AsHorizontalScrollBar() => new HorizontalScrollBar(this.BasicAutomationElement);

        public VerticalScrollBar AsVerticalScrollBar() => new VerticalScrollBar(this.BasicAutomationElement);

        public Window AsWindow(bool isMainWindow) => new Window(this.BasicAutomationElement, isMainWindow);

        public MessageBox AsMessageBox() => new MessageBox(this.BasicAutomationElement);

        protected internal void ExecuteInPattern<TPattern>(TPattern pattern, bool throwIfNotSupported, Action<TPattern> action)
        {
            if (pattern != null)
            {
                action(pattern);
            }
            else if (throwIfNotSupported)
            {
                throw new System.NotSupportedException();
            }
        }

        protected internal TRet ExecuteInPattern<TPattern, TRet>(TPattern pattern, bool throwIfNotSupported, Func<TPattern, TRet> func)
        {
            if (pattern != null)
            {
                return func(pattern);
            }

            if (throwIfNotSupported)
            {
                throw new System.NotSupportedException();
            }

            return default(TRet);
        }

        protected void PerformMouseAction(bool moveMouse, Action action)
        {
            var clickablePoint = this.GetClickablePoint();
            if (moveMouse)
            {
                Mouse.MoveTo(clickablePoint);
            }
            else
            {
                Mouse.Position = clickablePoint;
            }

            action();
            Wait.UntilInputIsProcessed();
        }
    }
}
