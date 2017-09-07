﻿namespace Gu.Wpf.UiAutomation
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
    public class AutomationElement : IEquatable<AutomationElement>
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
        /// Find the first checkbox by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public CheckBox FindCheckBox(string name = null) => this.Find(ControlType.CheckBox, name, x => new CheckBox(x));

        /// <summary>
        /// Find the first toggle button by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public ToggleButton FindToggleButton(string name = null) => this.Find(ControlType.Button, name, x => new ToggleButton(x));

        /// <summary>
        /// Find the first radio button by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public RadioButton FindRadioButton(string name) => this.Find(ControlType.RadioButton, name, x => new RadioButton(x));

        /// <summary>
        /// Find the first button by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public Button FindButton(string name = null) => this.Find(ControlType.Button, name, x => new Button(x));

        /// <summary>
        /// Find the first slider by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public Slider FindSlider(string name = null) => this.Find(ControlType.Slider, name, x => new Slider(x));

        /// <summary>
        /// Find the first ProgressBar by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public ProgressBar FindProgressBar(string name = null) => this.Find(ControlType.ProgressBar, name, x => new ProgressBar(x));

        /// <summary>
        /// Find the first combo box by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public ComboBox FindComboBox(string name = null) => this.Find(ControlType.ComboBox, name, x => new ComboBox(x));

        /// <summary>
        /// Find the first text block by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public TextBlock FindTextBlock(string name = null) => this.Find(ControlType.Text, name, x => new TextBlock(x));

        /// <summary>
        /// Find the first label by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public Label FindLabel(string name = null) => this.Find(ControlType.Text, name, x => new Label(x));

        /// <summary>
        /// Find the first text box by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public TextBox FindTextBox(string name = null) => this.Find(ControlType.Edit, name, x => new TextBox(x));

        /// <summary>
        /// Find the first tab control by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public TabControl FindTabControl(string name = null) => this.Find(ControlType.Tab, name, x => new TabControl(x));

        /// <summary>
        /// Find the first expander by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public UserControl FindUserControl(string name = null) => this.Find(ControlType.Custom, name, x => new UserControl(x));

        /// <summary>
        /// Find the first group box by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public GroupBox FindGroupBox(string name = null) => this.Find(ControlType.Group, name, x => new GroupBox(x));

        /// <summary>
        /// Find the first expander by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public Expander FindExpander(string name = null) => this.Find(ControlType.Group, name, x => new Expander(x));

        /// <summary>
        /// Find the first menu by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public Menu FindMenu(string name = null) => this.Find(ControlType.Menu, name, x => new Menu(x));

        /// <summary>
        /// Find the first horizontal scroll bar by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public HorizontalScrollBar FindHorizontalScrollBar(string name = null) => this.Find(ControlType.ScrollBar, name, x => new HorizontalScrollBar(x));

        /// <summary>
        /// Find the first vertical scroll bar by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public VerticalScrollBar FindVerticalScrollBar(string name = null) => this.Find(ControlType.ScrollBar, name, x => new VerticalScrollBar(x));

        /// <summary>
        /// Find the first list box by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public ListBox FindListBox(string name = null) => this.Find(ControlType.List, name, x => new ListBox(x));

        /// <summary>
        /// Find the first list box by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public ListView FindListView(string name = null) => this.Find(ControlType.DataGrid, name, x => new ListView(x));

        /// <summary>
        /// Find the first list box by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public DataGrid FindDataGrid(string name = null) => this.Find(ControlType.DataGrid, name, x => new DataGrid(x));

        /// <summary>
        /// Find the first list box by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public TreeView FindTreeView(string name = null) => this.Find(ControlType.Tree, name, x => new TreeView(x));

        public AutomationElement FindByNameOrId(string name, ControlType controlType)
        {
            return this.FindFirstDescendant(
                new AndCondition(
                    this.ConditionFactory.ByControlType(controlType),
                    new OrCondition(
                        this.ConditionFactory.ByName(name),
                        this.ConditionFactory.ByAutomationId(name))));
        }

        public AutomationElement Find(ControlType controlType)
        {
            var condition = this.ConditionFactory.ByControlType(controlType);

            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, Retry.DefaultRetryFor))
            {
                var element = this.BasicAutomationElement.FindFirst(TreeScope.Descendants, condition);
                if (element != null)
                {
                    return element;
                }

                Wait.For(Retry.DefaultRetryInterval);
            }

            throw new InvalidOperationException($"Did not find a {controlType}.");
        }

        public T Find<T>(ControlType controlType, Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement
        {
            var condition = this.ConditionFactory.ByControlType(controlType);
            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, Retry.DefaultRetryFor))
            {
                var element = this.BasicAutomationElement.FindFirst(TreeScope.Descendants, condition, wrap);
                if (element != null)
                {
                    return element;
                }

                Wait.For(Retry.DefaultRetryInterval);
            }

            throw new InvalidOperationException($"Did not find a {controlType}.");
        }

        public AutomationElement Find(ControlType controlType, int index)
        {
            var condition = this.ConditionFactory.ByControlType(controlType);
            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, Retry.DefaultRetryFor))
            {
                var element = this.BasicAutomationElement.FindIndexed(TreeScope.Descendants, condition, index);
                if (element != null)
                {
                    return element;
                }

                Wait.For(Retry.DefaultRetryInterval);
            }

            throw new InvalidOperationException($"Did not find a {controlType}.");
        }

        public T Find<T>(ControlType controlType, int index, Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement
        {
            var condition = this.ConditionFactory.ByControlType(controlType);
            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, Retry.DefaultRetryFor))
            {
                var element = this.BasicAutomationElement.FindIndexed(TreeScope.Descendants, condition, index, wrap);
                if (element != null)
                {
                    return element;
                }

                Wait.For(Retry.DefaultRetryInterval);
            }

            throw new InvalidOperationException($"Did not find a {controlType} with index {index}.");
        }

        public AutomationElement Find(ControlType controlType, string name)
        {
            if (name == null)
            {
                return this.Find(controlType);
            }

            var condition = new AndCondition(
                this.ConditionFactory.ByControlType(controlType),
                new OrCondition(
                    this.ConditionFactory.ByName(name),
                    this.ConditionFactory.ByAutomationId(name)));

            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, Retry.DefaultRetryFor))
            {
                var element = this.BasicAutomationElement.FindFirst(TreeScope.Descendants, condition);
                if (element != null)
                {
                    return element;
                }

                Wait.For(Retry.DefaultRetryInterval);
            }

            throw new InvalidOperationException($"Did not find a {controlType} with name {name}.");
        }

        public T Find<T>(ControlType controlType, string name, Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement
        {
            if (name == null)
            {
                return this.Find(controlType, wrap);
            }

            var condition = new AndCondition(
                this.ConditionFactory.ByControlType(controlType),
                new OrCondition(
                    this.ConditionFactory.ByName(name),
                    this.ConditionFactory.ByAutomationId(name)));

            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, Retry.DefaultRetryFor))
            {
                var element = this.BasicAutomationElement.FindFirst(TreeScope.Descendants, condition, wrap);
                if (element != null)
                {
                    return element;
                }

                Wait.For(Retry.DefaultRetryInterval);
            }

            throw new InvalidOperationException($"Did not find a {controlType} with name {name}.");
        }

        public AutomationElement FindByNameOrId(string name)
        {
            return this.FindFirstDescendant(
                new OrCondition(
                    this.ConditionFactory.ByName(name),
                    this.ConditionFactory.ByAutomationId(name)));
        }

        /// <summary>
        /// Finds all elements in the given treescope and with the given condition.
        /// </summary>
        public IReadOnlyList<AutomationElement> FindAll(TreeScope treeScope, ConditionBase condition)
        {
            return this.BasicAutomationElement.FindAll(treeScope, condition);
        }

        /// <summary>
        /// Finds all elements in the given treescope and with the given condition.
        /// </summary>
        public IReadOnlyList<T> FindAll<T>(TreeScope treeScope, ConditionBase condition, Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement
        {
            return this.BasicAutomationElement.FindAll(treeScope, condition, wrap);
        }

        /// <summary>
        /// Finds all elements in the given treescope and with the given condition within the given timeout.
        /// </summary>
        public IReadOnlyList<AutomationElement> FindAll(TreeScope treeScope, ConditionBase condition, TimeSpan timeOut)
        {
            return Retry.While(
                () => this.BasicAutomationElement.FindAll(treeScope, condition),
                elements => elements.Count == 0,
                timeOut);
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
            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, timeOut))
            {
                var element = this.BasicAutomationElement.FindFirst(treeScope, condition);
                if (element != null)
                {
                    return element;
                }

                Wait.For(Retry.DefaultRetryInterval);
            }

            throw new InvalidOperationException($"Did not find an element matching {condition}.");
        }

        /// <summary>
        /// Finds the first element which is in the given treescope with the given condition within the given timeout period.
        /// </summary>
        public AutomationElement FindAt(TreeScope treeScope, ConditionBase condition, int index, TimeSpan timeOut)
        {
            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, timeOut))
            {
                var element = this.BasicAutomationElement.FindIndexed(treeScope, condition, index);
                if (element != null)
                {
                    return element;
                }

                Wait.For(Retry.DefaultRetryInterval);
            }

            throw new InvalidOperationException($"Did not find an element matching {condition}.");
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
        public IReadOnlyList<AutomationElement> FindAllNested(params ConditionBase[] nestedConditions)
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
        public IReadOnlyList<AutomationElement> FindAllByXPath(string xPath)
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

        public AutomationElement FindFirstChild()
        {
            return this.FindFirst(TreeScope.Children, TrueCondition.Default);
        }

        public AutomationElement FindChildAt(int index)
        {
            return this.FindAt(TreeScope.Children, TrueCondition.Default, index, Retry.DefaultRetryFor);
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

        public IReadOnlyList<AutomationElement> FindAllChildren()
        {
            return this.FindAll(TreeScope.Children, TrueCondition.Default);
        }

        public IReadOnlyList<T> FindAllChildren<T>(Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement
        {
            return this.BasicAutomationElement.FindAll(TreeScope.Children, TrueCondition.Default, wrap);
        }

        public IReadOnlyList<AutomationElement> FindAllChildren(ConditionBase condition)
        {
            return this.BasicAutomationElement.FindAll(TreeScope.Children, condition);
        }

        public IReadOnlyList<T> FindAllChildren<T>(ConditionBase condition, Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement
        {
            return this.BasicAutomationElement.FindAll(TreeScope.Children, condition, wrap);
        }

        public IReadOnlyList<AutomationElement> FindAllChildren(Func<ConditionFactory, ConditionBase> newConditionFunc)
        {
            var condition = newConditionFunc(this.ConditionFactory);
            return this.BasicAutomationElement.FindAll(TreeScope.Children, condition);
        }

        public IReadOnlyList<T> FindAllChildren<T>(Func<ConditionFactory, ConditionBase> newConditionFunc, Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement
        {
            var condition = newConditionFunc(this.ConditionFactory);
            return this.FindAllChildren(condition, wrap);
        }

        public AutomationElement FindFirstDescendant()
        {
            return this.FindFirst(TreeScope.Descendants, TrueCondition.Default);
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

        public IReadOnlyList<AutomationElement> FindAllDescendants()
        {
            return this.FindAll(TreeScope.Descendants, TrueCondition.Default);
        }

        public IReadOnlyList<AutomationElement> FindAllDescendants(ConditionBase condition)
        {
            return this.FindAll(TreeScope.Descendants, condition);
        }

        public IReadOnlyList<AutomationElement> FindAllDescendants(Func<ConditionFactory, ConditionBase> newConditionFunc)
        {
            var condition = newConditionFunc(this.ConditionFactory);
            return this.FindAllDescendants(condition);
        }

        public AutomationElement FindFirstNested(Func<ConditionFactory, IList<ConditionBase>> nestedConditionsFunc)
        {
            var conditions = nestedConditionsFunc(this.ConditionFactory);
            return this.FindFirstNested(conditions.ToArray());
        }

        public IReadOnlyList<AutomationElement> FindAllNested(Func<ConditionFactory, IList<ConditionBase>> nestedConditionsFunc)
        {
            var conditions = nestedConditionsFunc(this.ConditionFactory);
            return this.FindAllNested(conditions.ToArray());
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
