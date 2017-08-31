namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
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
        /// The current used automation object.
        /// </summary>
        public AutomationElement Parent => this.Automation
                                               .TreeWalkerFactory
                                               .GetRawViewWalker()
                                               .GetParent(this);

        /// <summary>
        /// The current used automation object.
        /// </summary>
        public double ActualWidth => this.BoundingRectangle.Width;

        /// <summary>
        /// The current used automation object.
        /// </summary>
        public double ActualHeight => this.BoundingRectangle.Height;

        /// <summary>
        /// The current used automation object.
        /// </summary>
        public Rect BoundingRectangle
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

        public string ItemStatus => this.Properties.ItemStatus;

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
            return this.DrawHighlight(blocking: true, color: color, durationInMs: 2000);
        }

        /// <summary>
        /// Draws a manually colored highlight around the element.
        /// </summary>
        public AutomationElement DrawHighlight(GdiColor color)
        {
            return this.DrawHighlight(blocking: true, color: color, durationInMs: 2000);
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
            if (!rectangle.IsZeroes())
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
        public System.Drawing.Bitmap Capture()
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
        /// Find the first checkbox by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public CheckBox FindCheckBox(string name = null) => this.Find(ControlType.CheckBox, name).AsCheckBox();

        /// <summary>
        /// Find the first toggle button by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public ToggleButton FindToggleButton(string name = null) => this.Find(ControlType.Button, name).AsToggleButton();

        /// <summary>
        /// Find the first radio button by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public RadioButton FindRadioButton(string name) => this.Find(ControlType.RadioButton, name).AsRadioButton();

        /// <summary>
        /// Find the first button by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public Button FindButton(string name = null) => this.Find(ControlType.Button, name).AsButton();

        /// <summary>
        /// Find the first slider by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public Slider FindSlider(string name = null) => this.Find(ControlType.Slider, name).AsSlider();

        /// <summary>
        /// Find the first ProgressBar by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public ProgressBar FindProgressBar(string name = null) => this.Find(ControlType.ProgressBar, name).AsProgressBar();

        /// <summary>
        /// Find the first combo box by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public ComboBox FindComboBox(string name = null) => this.Find(ControlType.ComboBox, name).AsComboBox();

        /// <summary>
        /// Find the first text block by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public TextBlock FindTextBlock(string name = null) => this.Find(ControlType.Text, name).AsTextBlock();

        /// <summary>
        /// Find the first label by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public Label FindLabel(string name = null) => this.Find(ControlType.Text, name).AsLabel();

        /// <summary>
        /// Find the first text box by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public TextBox FindTextBox(string name = null) => this.Find(ControlType.Edit, name).AsTextBox();

        /// <summary>
        /// Find the first tab control by x:Name, Content or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public TabControl FindTabControl(string name = null) => this.Find(ControlType.Tab, name).AsTabControl();

        /// <summary>
        /// Find the first group box by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public GroupBox FindGroupBox(string name = null) => this.Find(ControlType.Group, name).AsGroupBox();

        /// <summary>
        /// Find the first expander by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public Expander FindExpander(string name = null) => this.Find(ControlType.Group, name).AsExpander();

        /// <summary>
        /// Find the first menu by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public Menu FindMenu(string name = null) => this.Find(ControlType.Menu, name).AsMenu();

        /// <summary>
        /// Find the first horizontal scroll bar by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public HorizontalScrollBar FindHorizontalScrollBar(string name = null) => this.Find(ControlType.ScrollBar, name).AsHorizontalScrollBar();

        /// <summary>
        /// Find the first vertical scroll bar by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public VerticalScrollBar FindVerticalScrollBar(string name = null) => this.Find(ControlType.ScrollBar, name).AsVerticalScrollBar();

        /// <summary>
        /// Find the first list box by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public ListBox FindListBox(string name = null) => this.Find(ControlType.Group, name).AsListBox();

        /// <summary>
        /// Find the first list box by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public ListView FindListView(string name = null) => this.Find(ControlType.DataGrid, name).AsListView();

        /// <summary>
        /// Find the first list box by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public DataGrid FindDataGrid(string name = null) => this.Find(ControlType.DataGrid, name).AsDataGrid();

        /// <summary>
        /// Find the first list box by x:Name, Header or AutomationID
        /// </summary>
        /// <param name="name">x:Name, Content or AutomationID</param>
        public TreeView FindTreeView(string name = null) => this.Find(ControlType.Tree, name).AsTreeView();

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
            return this.FindFirstDescendant(this.ConditionFactory.ByControlType(controlType));
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
            return this.FindAll(treeScope, condition, Retry.DefaultRetryFor);
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
            return $"AutomationId:{this.Properties.AutomationId.ValueOrDefault}, Name:{this.Properties.Name.ValueOrDefault}, ControlType:{this.Properties.LocalizedControlType.ValueOrDefault}, FrameworkId:{this.Properties.FrameworkId.ValueOrDefault}";
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

        public GridHeader AsGridHeader() => new GridHeader(this.BasicAutomationElement);

        public GridHeaderItem AsGridHeaderItem() => new GridHeaderItem(this.BasicAutomationElement);

        public Menu AsMenu() => new Menu(this.BasicAutomationElement);

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

        public Window AsWindow() => new Window(this.BasicAutomationElement);

        public AutomationElement FindFirstChild()
        {
            return this.FindFirst(TreeScope.Children, TrueCondition.Default);
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

        public IReadOnlyList<AutomationElement> FindAllChildren(ConditionBase condition)
        {
            return this.FindAll(TreeScope.Children, condition);
        }

        public IReadOnlyList<AutomationElement> FindAllChildren(Func<ConditionFactory, ConditionBase> newConditionFunc)
        {
            var condition = newConditionFunc(this.ConditionFactory);
            return this.FindAllChildren(condition);
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

        protected AutomationElement Find(ControlType controlType, string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                return this.Find(controlType) ??
                       throw new InvalidOperationException($"Did not find a {controlType}.");
            }

            return this.FindFirstDescendant(
                new AndCondition(
                    this.ConditionFactory.ByControlType(controlType),
                    new OrCondition(
                        this.ConditionFactory.ByName(name),
                        this.ConditionFactory.ByAutomationId(name)))) ??
                        throw new InvalidOperationException($"Did not find a {controlType} with name {name}.");
        }
    }
}
