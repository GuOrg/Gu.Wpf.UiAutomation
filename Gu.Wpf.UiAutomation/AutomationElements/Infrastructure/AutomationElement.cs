namespace Gu.Wpf.UiAutomation.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Gu.Wpf.UiAutomation.Conditions;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.EventHandlers;
    using Gu.Wpf.UiAutomation.Exceptions;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Input;
    using Gu.Wpf.UiAutomation.Scrolling;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using GdiColor = System.Drawing.Color;
    using WpfColor = System.Windows.Media.Color;

    /// <summary>
    /// Wrapper object for each ui element which is automatable.
    /// </summary>
    public class AutomationElement : IEquatable<AutomationElement>
    {
        public AutomationElement(BasicAutomationElementBase basicAutomationElement)
        {
            this.BasicAutomationElement = basicAutomationElement ?? throw new ArgumentNullException(nameof(basicAutomationElement));
        }

        public AutomationElement(AutomationElement automationElement)
            : this(automationElement?.BasicAutomationElement)
        {
        }

        /// <summary>
        /// Object which contains the native wrapper element (UIA2 or UIA3) for this element.
        /// </summary>
        public BasicAutomationElementBase BasicAutomationElement { get; }

        /// <summary>
        /// The current used automationn object.
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

        /// <summary>
        /// Gets the cached children for this element.
        /// </summary>
        public AutomationElement[] CachedChildren => this.BasicAutomationElement.GetCachedChildren();

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
        /// Flag if the element is enabled or not.
        /// </summary>
        public bool IsEnabled => this.Properties.IsEnabled.Value;

        /// <summary>
        /// Flag if the element off-screen or on-screen(visible).
        /// </summary>
        public bool IsOffscreen => this.Properties.IsOffscreen.Value;

        /// <summary>
        /// Performs a left click on the element.
        /// </summary>
        /// <param name="moveMouse">Flag to indicate, if the mouse should move slowly (true) or instantly (false).</param>
        public void Click(bool moveMouse = false)
        {
            this.PerformMouseAction(moveMouse, Mouse.LeftClick);
        }

        /// <summary>
        /// Performs a double left click on the element.
        /// </summary>
        /// <param name="moveMouse">Flag to indicate, if the mouse should move slowly (true) or instantly (false).</param>
        public void DoubleClick(bool moveMouse = false)
        {
            this.PerformMouseAction(moveMouse, Mouse.LeftDoubleClick);
        }

        /// <summary>
        /// Performs a right click on the element.
        /// </summary>
        /// <param name="moveMouse">Flag to indicate, if the mouse should move slowly (true) or instantly (false).</param>
        public void RightClick(bool moveMouse = false)
        {
            this.PerformMouseAction(moveMouse, Mouse.RightClick);
        }

        /// <summary>
        /// Performs a double right click on the element.
        /// </summary>
        /// <param name="moveMouse">Flag to indicate, if the mouse should move slowly (true) or instantly (false).</param>
        public void RightDoubleClick(bool moveMouse = false)
        {
            this.PerformMouseAction(moveMouse, Mouse.RightDoubleClick);
        }

        private void PerformMouseAction(bool moveMouse, Action action)
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
            Helpers.WaitUntilInputIsProcessed();
        }

        /// <summary>
        /// Sets the focus to this element.
        /// Warning: This can be unreliable! <see cref="SetForeground" /> should be more reliable.
        /// </summary>
        public virtual void Focus()
        {
            this.BasicAutomationElement.SetFocus();
        }

        /// <summary>
        /// Sets the focus by using the Win32 SetFocus() method.
        /// </summary>
        public void FocusNative()
        {
            var windowHandle = this.Properties.NativeWindowHandle;
            if (windowHandle != new IntPtr(0))
            {
                User32.SetFocus(windowHandle);
                Helpers.WaitUntilResponsive(this);
            }
            else
            {
                // Fallback to the UIA Version
                this.Focus();
            }
        }

        /// <summary>
        /// Brings the element to the foreground.
        /// </summary>
        public void SetForeground()
        {
            var windowHandle = this.Properties.NativeWindowHandle;
            if (windowHandle != new IntPtr(0))
            {
                User32.SetForegroundWindow(windowHandle);
                Helpers.WaitUntilResponsive(this);
            }
            else
            {
                // Fallback to the UIA Version
                this.Focus();
            }
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
            return this.DrawHighlight(true, color, 2000);
        }

        /// <summary>
        /// Draws a manually colored highlight around the element.
        /// </summary>
        public AutomationElement DrawHighlight(GdiColor color)
        {
            return this.DrawHighlight(true, color, 2000);
        }

        /// <summary>
        /// Draw a highlight around the element with the given settings.
        /// </summary>
        /// <param name="blocking">Flag to indicate if further execution waits until the highlight is removed.</param>
        /// <param name="color">The color to draw the highlight.</param>
        /// <param name="durationInMs">The duration (im ms) how long the highlight is shown.</param>
        /// <remarks>Override for winforms color.</remarks>
        public AutomationElement DrawHighlight(bool blocking, GdiColor color, int durationInMs)
        {
            return this.DrawHighlight(blocking, WpfColor.FromArgb(color.A, color.R, color.G, color.B), durationInMs);
        }

        /// <summary>
        /// Draw a highlight around the element with the given settings.
        /// </summary>
        /// <param name="blocking">Flag to indicate if further execution waits until the highlight is removed.</param>
        /// <param name="color">The color to draw the highlight.</param>
        /// <param name="durationInMs">The duration (im ms) how long the highlight is shown.</param>
        public AutomationElement DrawHighlight(bool blocking, WpfColor color, int durationInMs)
        {
            var rectangle = this.Properties.BoundingRectangle.Value;
            if (!rectangle.IsEmpty)
            {
                if (blocking)
                {
                    this.BasicAutomationElement.Automation.OverlayManager.ShowBlocking(rectangle, color, durationInMs);
                }
                else
                {
                    this.BasicAutomationElement.Automation.OverlayManager.Show(rectangle, color, durationInMs);
                }
            }

            return this;
        }

        /// <summary>
        /// Captures the object as screenshot in WinForms Bitmap format.
        /// </summary>
        public Bitmap Capture()
        {
            return ScreenCapture.CaptureArea(this.Properties.BoundingRectangle);
        }

        /// <summary>
        /// Captures the object as screenshot in WPF BitmapImage format.
        /// </summary>
        public BitmapImage CaptureWpf()
        {
            return ScreenCapture.CaptureAreaWpf(this.Properties.BoundingRectangle);
        }

        /// <summary>
        /// Captures the object as screenshot directly into the given file.
        /// </summary>
        /// <param name="filePath">The filepath where the screenshot should be saved.</param>
        public void CaptureToFile(string filePath)
        {
            ScreenCapture.CaptureAreaToFile(this.Properties.BoundingRectangle, filePath);
        }

        /// <summary>
        /// Finds all elements in the given treescope and with the given condition.
        /// </summary>
        public AutomationElement[] FindAll(TreeScope treeScope, ConditionBase condition)
        {
            return this.FindAll(treeScope, condition, Retry.DefaultRetryFor);
        }

        /// <summary>
        /// Finds all elements in the given treescope and with the given condition within the given timeout.
        /// </summary>
        public AutomationElement[] FindAll(TreeScope treeScope, ConditionBase condition, TimeSpan timeOut)
        {
            Predicate<AutomationElement[]> whilePredicate = elements => elements.Length == 0;
            Func<AutomationElement[]> retryMethod = () => this.BasicAutomationElement.FindAll(treeScope, condition);
            return Retry.While(retryMethod, whilePredicate, timeOut);
        }

        /// <summary>
        /// Finds the first element which is in the given treescope with the given condition.
        /// </summary>
        public AutomationElement FindFirst(TreeScope treeScope, ConditionBase condition)
        {
            return this.FindFirst(treeScope, condition, Retry.DefaultRetryFor);
        }

        /// <summary>
        /// Finds the first element which is in the given treescope with the given condition within the given timeout period.
        /// </summary>
        public AutomationElement FindFirst(TreeScope treeScope, ConditionBase condition, TimeSpan timeOut)
        {
            Predicate<AutomationElement> whilePredicate = element => element == null;
            Func<AutomationElement> retryMethod = () => this.BasicAutomationElement.FindFirst(treeScope, condition);
            return Retry.While(retryMethod, whilePredicate, timeOut);
        }

        /// <summary>
        /// Finds the first element by looping thru all conditions.
        /// </summary>
        public AutomationElement FindFirstNested(params ConditionBase[] nestedConditions)
        {
            var currentElement = this;
            foreach (var condition in nestedConditions)
            {
                currentElement = currentElement.FindFirstChild(condition);
                if (currentElement == null)
                {
                    return null;
                }
            }

            return currentElement;
        }

        /// <summary>
        /// Finds all elements by looping thru all conditions.
        /// </summary>
        public AutomationElement[] FindAllNested(params ConditionBase[] nestedConditions)
        {
            var currentElement = this;
            for (var i = 0; i < nestedConditions.Length - 1; i++)
            {
                var condition = nestedConditions[i];
                currentElement = currentElement.FindFirstChild(condition);
                if (currentElement == null)
                {
                    return null;
                }
            }

            return currentElement.FindAllChildren(nestedConditions.Last());
        }

        /// <summary>
        /// Finds for the first item which matches the given xpath.
        /// </summary>
        public AutomationElement FindFirstByXPath(string xPath)
        {
            var xPathNavigator = new AutomationElementXPathNavigator(this);
            var nodeItem = xPathNavigator.SelectSingleNode(xPath);
            return (AutomationElement)nodeItem?.UnderlyingObject;
        }

        /// <summary>
        /// Finds all items which match the given xpath.
        /// </summary>
        public AutomationElement[] FindAllByXPath(string xPath)
        {
            var xPathNavigator = new AutomationElementXPathNavigator(this);
            var itemNodeIterator = xPathNavigator.Select(xPath);
            var itemList = new List<AutomationElement>();
            while (itemNodeIterator.MoveNext())
            {
                var automationItem = (AutomationElement)itemNodeIterator.Current.UnderlyingObject;
                itemList.Add(automationItem);
            }

            return itemList.ToArray();
        }

        /// <summary>
        /// Gets a clickable point of the element.
        /// </summary>
        /// <exception cref="Exceptions.NoClickablePointException">Thrown when no clickable point was found</exception>
        public Shapes.Point GetClickablePoint()
        {
            return this.BasicAutomationElement.GetClickablePoint();
        }

        /// <summary>
        /// Tries to get a clickable point of the element.
        /// </summary>
        /// <param name="point">The clickable point or null, if no point was found</param>
        /// <returns>True if a point was found, false otherwise</returns>
        public bool TryGetClickablePoint(out Shapes.Point point)
        {
            return this.BasicAutomationElement.TryGetClickablePoint(out point);
        }

        /// <summary>
        /// Registers the given event
        /// </summary>
        public IAutomationEventHandler RegisterEvent(EventId @event, TreeScope treeScope, Action<AutomationElement, EventId> action)
        {
            if (Equals(@event, EventId.NotSupportedByFramework))
            {
                throw new NotSupportedByFrameworkException();
            }

            return this.BasicAutomationElement.RegisterEvent(@event, treeScope, action);
        }

        /// <summary>
        /// Registers a property changed event with the given property
        /// </summary>
        public IAutomationPropertyChangedEventHandler RegisterPropertyChangedEvent(TreeScope treeScope, Action<AutomationElement, PropertyId, object> action, params PropertyId[] properties)
        {
            return this.BasicAutomationElement.RegisterPropertyChangedEvent(treeScope, action, properties);
        }

        /// <summary>
        /// Registers a structure changed event
        /// </summary>
        public IAutomationStructureChangedEventHandler RegisterStructureChangedEvent(TreeScope treeScope, Action<AutomationElement, StructureChangeType, int[]> action)
        {
            return this.BasicAutomationElement.RegisterStructureChangedEvent(treeScope, action);
        }

        /// <summary>
        /// Removes the given event handler for the event
        /// </summary>
        public void RemoveAutomationEventHandler(EventId @event, IAutomationEventHandler eventHandler)
        {
            this.BasicAutomationElement.RemoveAutomationEventHandler(@event, eventHandler);
        }

        /// <summary>
        /// Removes the given property changed event handler
        /// </summary>
        public void RemovePropertyChangedEventHandler(IAutomationPropertyChangedEventHandler eventHandler)
        {
            this.BasicAutomationElement.RemovePropertyChangedEventHandler(eventHandler);
        }

        /// <summary>
        /// Removes the given structure changed event handler
        /// </summary>
        public void RemoveStructureChangedEventHandler(IAutomationStructureChangedEventHandler eventHandler)
        {
            this.BasicAutomationElement.RemoveStructureChangedEventHandler(eventHandler);
        }

        /// <summary>
        /// Gets the available patterns for an element via properties
        /// </summary>
        public PatternId[] GetSupportedPatterns()
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
        public PatternId[] GetSupportedPatternsDirect()
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
        public PropertyId[] GetSupportedPropertiesDirect()
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
            return $"AutomationId:{this.Properties.AutomationId.ValueOrDefault}, Name:{this.Properties.Name.ValueOrDefault}, ControlType:{this.Properties.LocalizedControlType.ValueOrDefault}, FrameworkId:{this.Properties.FrameworkId.ValueOrDefault}";
        }

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

        public Button AsButton()
        {
            return new Button(this.BasicAutomationElement);
        }

        public CheckBox AsCheckBox()
        {
            return new CheckBox(this.BasicAutomationElement);
        }

        public ComboBox AsComboBox()
        {
            return new ComboBox(this.BasicAutomationElement);
        }

        public Label AsLabel()
        {
            return new Label(this.BasicAutomationElement);
        }

        public Grid AsGrid()
        {
            return new Grid(this.BasicAutomationElement);
        }

        public GridRow AsGridRow()
        {
            return new GridRow(this.BasicAutomationElement);
        }

        public GridCell AsGridCell()
        {
            return new GridCell(this.BasicAutomationElement);
        }

        public GridHeader AsGridHeader()
        {
            return new GridHeader(this.BasicAutomationElement);
        }

        public GridHeaderItem AsGridHeaderItem()
        {
            return new GridHeaderItem(this.BasicAutomationElement);
        }

        public HScrollBar AsHScrollBar()
        {
            return new HScrollBar(this.BasicAutomationElement);
        }

        public Menu AsMenu()
        {
            return new Menu(this.BasicAutomationElement);
        }

        public MenuItem AsMenuItem()
        {
            return new MenuItem(this.BasicAutomationElement);
        }

        public ProgressBar AsProgressBar()
        {
            return new ProgressBar(this.BasicAutomationElement);
        }

        public RadioButton AsRadioButton()
        {
            return new RadioButton(this.BasicAutomationElement);
        }

        public Slider AsSlider()
        {
            return new Slider(this.BasicAutomationElement);
        }

        public Tab AsTab()
        {
            return new Tab(this.BasicAutomationElement);
        }

        public TabItem AsTabItem()
        {
            return new TabItem(this.BasicAutomationElement);
        }

        public TextBox AsTextBox()
        {
            return new TextBox(this.BasicAutomationElement);
        }

        public Thumb AsThumb()
        {
            return new Thumb(this.BasicAutomationElement);
        }

        public TitleBar AsTitleBar()
        {
            return new TitleBar(this.BasicAutomationElement);
        }

        public Tree AsTree()
        {
            return new Tree(this.BasicAutomationElement);
        }

        public TreeItem AsTreeItem()
        {
            return new TreeItem(this.BasicAutomationElement);
        }

        public VScrollBar AsVScrollBar()
        {
            return new VScrollBar(this.BasicAutomationElement);
        }

        public Window AsWindow()
        {
            return new Window(this.BasicAutomationElement);
        }

        public AutomationElement FindFirstChild()
        {
            return this.FindFirst(TreeScope.Children, new TrueCondition());
        }

        public AutomationElement FindFirstChild(string automationId)
        {
            return this.FindFirst(TreeScope.Children, this.ConditionFactory.ByAutomationId(automationId));
        }

        public AutomationElement FindFirstChild(ConditionBase condition)
        {
            return this.FindFirst(TreeScope.Children, condition);
        }

        public AutomationElement FindFirstChild(Func<ConditionFactory, ConditionBase> newConditionFunc)
        {
            var condition = newConditionFunc(this.ConditionFactory);
            return this.FindFirstChild(condition);
        }

        public AutomationElement[] FindAllChildren()
        {
            return this.FindAll(TreeScope.Children, new TrueCondition());
        }

        public AutomationElement[] FindAllChildren(ConditionBase condition)
        {
            return this.FindAll(TreeScope.Children, condition);
        }

        public AutomationElement[] FindAllChildren(Func<ConditionFactory, ConditionBase> newConditionFunc)
        {
            var condition = newConditionFunc(this.ConditionFactory);
            return this.FindAllChildren(condition);
        }

        public AutomationElement FindFirstDescendant()
        {
            return this.FindFirst(TreeScope.Descendants, new TrueCondition());
        }

        public AutomationElement FindFirstDescendant(string automationId)
        {
            return this.FindFirst(TreeScope.Descendants, this.ConditionFactory.ByAutomationId(automationId));
        }

        public AutomationElement FindFirstDescendant(ConditionBase condition)
        {
            return this.FindFirst(TreeScope.Descendants, condition);
        }

        public AutomationElement FindFirstDescendant(Func<ConditionFactory, ConditionBase> newConditionFunc)
        {
            var condition = newConditionFunc(this.ConditionFactory);
            return this.FindFirstDescendant(condition);
        }

        public AutomationElement[] FindAllDescendants()
        {
            return this.FindAll(TreeScope.Descendants, new TrueCondition());
        }

        public AutomationElement[] FindAllDescendants(ConditionBase condition)
        {
            return this.FindAll(TreeScope.Descendants, condition);
        }

        public AutomationElement[] FindAllDescendants(Func<ConditionFactory, ConditionBase> newConditionFunc)
        {
            var condition = newConditionFunc(this.ConditionFactory);
            return this.FindAllDescendants(condition);
        }

        public AutomationElement FindFirstNested(Func<ConditionFactory, IList<ConditionBase>> nestedConditionsFunc)
        {
            var conditions = nestedConditionsFunc(this.ConditionFactory);
            return this.FindFirstNested(conditions.ToArray());
        }

        public AutomationElement[] FindAllNested(Func<ConditionFactory, IList<ConditionBase>> nestedConditionsFunc)
        {
            var conditions = nestedConditionsFunc(this.ConditionFactory);
            return this.FindAllNested(conditions.ToArray());
        }
    }
}
