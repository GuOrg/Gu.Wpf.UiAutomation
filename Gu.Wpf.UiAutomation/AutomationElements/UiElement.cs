namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Gu.Wpf.UiAutomation.Converters;
    using Gu.Wpf.UiAutomation.Overlay;
    using GdiColor = System.Drawing.Color;
    using WpfColor = System.Windows.Media.Color;

    /// <summary>
    /// Wrapper object for each ui element which is automatable.
    /// </summary>
    public partial class UiElement : IEquatable<UiElement>
    {
        public UiElement(AutomationElement automationElement)
        {
            this.AutomationElement = automationElement ?? throw new ArgumentNullException(nameof(automationElement));
        }

        /// <summary>
        /// The wrapped <see cref="System.Windows.Automation.AutomationElement"/>
        /// </summary>
        public AutomationElement AutomationElement { get; }

        /// <summary>
        /// Get the parent <see cref="UiElement"/>
        /// </summary>
        public UiElement Parent => new UiElement(TreeWalker.RawViewWalker.GetParent(this.AutomationElement));

        /// <summary>
        /// Get the parent <see cref="UiElement"/>
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
        /// The bounding rectangle relative to the screen.
        /// </summary>
        public Rect Bounds => this.AutomationElement.BoundingRectangle();

        /// <summary>
        /// The bounding rectangle relative to the window.
        /// </summary>
        public Rect RenderBounds
        {
            get
            {
                var rect = this.AutomationElement.BoundingRectangle();
                var windowBounds = this.Window?.Bounds ?? Rect.Empty;
                rect.Offset(-windowBounds.X, -windowBounds.Y);
                return rect;
            }
        }

        public string ItemStatus => this.AutomationElement.ItemStatus();

        public string HelpText => this.AutomationElement.HelpText();

        /// <summary>
        /// The direct framework type of the element.
        /// Results in "FrameworkType.Unknown" if it couldn't be resolved.
        /// </summary>
        public FrameworkType FrameworkType => FrameworkIds.ConvertToFrameworkType(this.AutomationElement.FrameworkId());

        /// <summary>
        /// The automation id of the element.
        /// </summary>
        public string AutomationId => this.AutomationElement.AutomationId();

        /// <summary>
        /// The name of the element.
        /// </summary>
        public string Name => this.AutomationElement.Name();

        /// <summary>
        /// The class name of the element.
        /// </summary>
        public string ClassName => this.AutomationElement.ClassName();

        /// <summary>
        /// The control type of the element.
        /// </summary>
        public ControlType ControlType => this.AutomationElement.ControlType();

        /// <summary>
        /// Flag if the element off-screen or on-screen(visible).
        /// </summary>
        public bool IsOffscreen => this.AutomationElement.IsOffscreen();

        public int ProcessId => this.AutomationElement.ProcessId();

        public string LocalizedControlType => this.AutomationElement.LocalizedControlType();

        /// <summary>
        /// Gets the cached children for this element.
        /// </summary>
        public IReadOnlyList<UiElement> CachedChildren => this.AutomationElement
                                                              .CachedChildren
                                                              .OfType<AutomationElement>()
                                                              .Select(x => new UiElement(x))
                                                              .ToArray();

        /// <summary>
        /// Gets the cached parent for this element.
        /// </summary>
        public UiElement CachedParent => new UiElement(this.AutomationElement.CachedParent);

        public bool TryGetWindow(out Window window)
        {
            if (this is Window w)
            {
                window = w;
                return true;
            }

            if (this.AutomationElement.TryFindFirst(TreeScope.Ancestors, Condition.Window, out var element))
            {
                window = new Window(element);
                return true;
            }

            window = null;
            return false;
        }

        /// <summary>
        /// Draws a red highlight around the element.
        /// </summary>
        public UiElement DrawHighlight()
        {
            return this.DrawHighlight(Colors.Red);
        }

        /// <summary>
        /// Draws a manually colored highlight around the element.
        /// </summary>
        public UiElement DrawHighlight(WpfColor color)
        {
            return this.DrawHighlight(blocking: true, color: color, duration: TimeSpan.FromMilliseconds(2000));
        }

        /// <summary>
        /// Draws a manually colored highlight around the element.
        /// </summary>
        public UiElement DrawHighlight(GdiColor color)
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
        public UiElement DrawHighlight(bool blocking, GdiColor color, TimeSpan duration)
        {
            return this.DrawHighlight(blocking, WpfColor.FromArgb(color.A, color.R, color.G, color.B), duration);
        }

        /// <summary>
        /// Draw a highlight around the element with the given settings.
        /// </summary>
        /// <param name="blocking">Flag to indicate if further execution waits until the highlight is removed.</param>
        /// <param name="color">The color to draw the highlight.</param>
        /// <param name="duration">The duration (im ms) how long the highlight is shown.</param>
        public UiElement DrawHighlight(bool blocking, WpfColor color, TimeSpan duration)
        {
            var rectangle = this.Bounds;
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
            return this.AutomationElement.GetClickablePoint();
        }

        /// <summary>
        /// Tries to get a clickable point of the element.
        /// </summary>
        /// <param name="point">The clickable point or null, if no point was found</param>
        /// <returns>True if a point was found, false otherwise</returns>
        public bool TryGetClickablePoint(out Point point)
        {
            return this.AutomationElement.TryGetClickablePoint(out point);
        }

        /// <summary>
        /// Registers the given event
        /// </summary>
        public IDisposable SubscribeToEvent(AutomationEvent automationEvent, TreeScope treeScope, Action<UiElement, AutomationEventArgs> action)
        {
            return this.AutomationElement.SubscribeToEvent(
                automationEvent,
                treeScope,
                (sender, args) => action(FromAutomationElement((AutomationElement)sender), args));
        }

        /// <summary>
        /// Registers a property changed event with the given property
        /// </summary>
        public IDisposable SubscribeToPropertyChangedEvent(TreeScope treeScope, AutomationProperty property, Action<UiElement, AutomationPropertyChangedEventArgs> action)
        {
            return this.AutomationElement.SubscribeToPropertyChangedEvent(
                treeScope,
                property,
                (sender, args) => action(FromAutomationElement((AutomationElement)sender), args));
        }

        /// <summary>
        /// Registers a property changed event with the given property
        /// </summary>
        public IDisposable SubscribeToPropertyChangedEvent(TreeScope treeScope, Action<UiElement, AutomationPropertyChangedEventArgs> action, params AutomationProperty[] properties)
        {
            return this.AutomationElement.SubscribeToPropertyChangedEvent(
                treeScope,
                (sender, args) => action(FromAutomationElement((AutomationElement)sender), args),
                properties);
        }

        /// <summary>
        /// Registers a structure changed event
        /// </summary>
        public IDisposable SubscribeToStructureChangedEvent(TreeScope treeScope, Action<UiElement, StructureChangedEventArgs> action)
        {
            return this.AutomationElement.SubscribeToStructureChangedEvent(
                treeScope,
                (sender, args) => action(FromAutomationElement((AutomationElement)sender), args));
        }

        /// <summary>
        /// Gets the available patterns for an element via properties
        /// </summary>
        public IReadOnlyList<AutomationPattern> GetSupportedPatterns()
        {
            return this.AutomationElement.GetSupportedPatterns();
        }

        /// <summary>
        /// Checks if the given pattern is available for the element via properties
        /// </summary>
        public bool IsPatternSupported(AutomationPattern patternId)
        {
            return this.AutomationElement.TryGetCurrentPattern(patternId, out _);
        }

        /// <summary>
        /// Gets the available patterns for an element via UIA method.
        /// Does not work with cached elements and might be unreliable.
        /// </summary>
        public IReadOnlyList<AutomationPattern> GetSupportedPatternsDirect()
        {
            return this.AutomationElement.GetSupportedPatterns();
        }

        /// <summary>
        /// Checks if the given pattern is available for the element via UIA method.
        /// Does not work with cached elements and might be unreliable.
        /// </summary>
        public bool IsPatternSupportedDirect(AutomationPattern pattern)
        {
            return this.GetSupportedPatternsDirect().Contains(pattern);
        }

        /// <summary>
        /// Gets the available properties for an element via UIA method.
        /// Does not work with cached elements and might be unreliable.
        /// </summary>
        public IReadOnlyList<AutomationProperty> GetSupportedPropertiesDirect()
        {
            return this.AutomationElement.GetSupportedProperties();
        }

        /// <summary>
        /// Method to check if the element supports the given property via UIA method.
        /// Does not work with cached elements and might be unreliable.
        /// </summary>
        public bool IsPropertySupportedDirect(AutomationProperty property)
        {
            return this.GetSupportedPropertiesDirect().Contains(property);
        }

        /// <summary>
        /// Compares two elements.
        /// </summary>
        public bool Equals(UiElement other)
        {
            return other != null && Equals(this.AutomationElement, other.AutomationElement);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as UiElement);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.AutomationElement?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// Overrides the string representation of the element with something useful
        /// </summary>
        public override string ToString()
        {
            return $"AutomationId: {this.AutomationId}, Name: {this.Name}, ControlType: {this.LocalizedControlType}, FrameworkId: {this.AutomationElement.FrameworkId()}";
        }

        public Button AsButton() => new Button(this.AutomationElement);

        public CheckBox AsCheckBox() => new CheckBox(this.AutomationElement);

        public ToggleButton AsToggleButton() => new ToggleButton(this.AutomationElement);

        public ComboBox AsComboBox() => new ComboBox(this.AutomationElement);

        public Label AsLabel() => new Label(this.AutomationElement);

        public TextBlock AsTextBlock() => new TextBlock(this.AutomationElement);

        public GroupBox AsGroupBox() => new GroupBox(this.AutomationElement);

        public Expander AsExpander() => new Expander(this.AutomationElement);

        public GridRow AsGridRow() => new GridRow(this.AutomationElement);

        public ListBox AsListBox() => new ListBox(this.AutomationElement);

        public ListView AsListView() => new ListView(this.AutomationElement);

        public DataGrid AsDataGrid() => new DataGrid(this.AutomationElement);

        public ListBoxItem AsListBoxItem() => new ListBoxItem(this.AutomationElement);

        public GridCell AsGridCell() => new GridCell(this.AutomationElement);

        public Menu AsMenu() => new Menu(this.AutomationElement);

        public ContextMenu AsContextMenu() => new ContextMenu(this.AutomationElement);

        public MenuItem AsMenuItem() => new MenuItem(this.AutomationElement);

        public ProgressBar AsProgressBar() => new ProgressBar(this.AutomationElement);

        public RadioButton AsRadioButton() => new RadioButton(this.AutomationElement);

        public Slider AsSlider() => new Slider(this.AutomationElement);

        public TabControl AsTabControl() => new TabControl(this.AutomationElement);

        public TabItem AsTabItem() => new TabItem(this.AutomationElement);

        public TextBox AsTextBox() => new TextBox(this.AutomationElement);

        public Thumb AsThumb() => new Thumb(this.AutomationElement);

        public TitleBar AsTitleBar() => new TitleBar(this.AutomationElement);

        public TreeView AsTreeView() => new TreeView(this.AutomationElement);

        public TreeViewItem AsTreeViewItem() => new TreeViewItem(this.AutomationElement);

        public HorizontalScrollBar AsHorizontalScrollBar() => new HorizontalScrollBar(this.AutomationElement);

        public VerticalScrollBar AsVerticalScrollBar() => new VerticalScrollBar(this.AutomationElement);

        public Window AsWindow(bool isMainWindow) => new Window(this.AutomationElement, isMainWindow);

        public MessageBox AsMessageBox() => new MessageBox(this.AutomationElement);

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
