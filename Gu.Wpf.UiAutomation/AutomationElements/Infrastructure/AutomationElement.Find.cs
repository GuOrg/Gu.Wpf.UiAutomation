namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class AutomationElement
    {
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
        /// Finds the first element which is in the given treescope with the given condition.
        /// </summary>
        public T FindFirst<T>(TreeScope treeScope, ConditionBase condition, Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement
        {
            return this.FindFirst(treeScope, condition, wrap, Retry.DefaultRetryFor);
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
        public T FindFirst<T>(TreeScope treeScope, ConditionBase condition, Func<BasicAutomationElementBase, T> wrap, TimeSpan timeOut)
            where T : AutomationElement
        {
            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, timeOut))
            {
                var element = this.BasicAutomationElement.FindFirst(treeScope, condition, wrap);
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

        public AutomationElement FindFirstChild()
        {
            return this.FindFirst(TreeScope.Children, TrueCondition.Default);
        }

        public T FindFirstChild<T>(Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement
        {
            return this.FindFirst(TreeScope.Children, TrueCondition.Default, wrap);
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

        public T FindFirstDescendant<T>(Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement
        {
            return this.FindFirst(TreeScope.Descendants, TrueCondition.Default, wrap);
        }

        public AutomationElement FindFirstDescendant(string automationId)
        {
            return this.FindFirst(TreeScope.Descendants, this.ConditionFactory.ByAutomationId(automationId));
        }

        public T FindFirstDescendant<T>(string automationId, Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement
        {
            return this.FindFirst(TreeScope.Descendants, this.ConditionFactory.ByAutomationId(automationId), wrap);
        }

        public AutomationElement FindFirstDescendant(ControlType controlType)
        {
            return this.FindFirst(TreeScope.Descendants, this.ConditionFactory.ByControlType(controlType));
        }

        public T FindFirstDescendant<T>(ControlType controlType, Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement
        {
            return this.FindFirst(TreeScope.Descendants, this.ConditionFactory.ByControlType(controlType), wrap);
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
    }
}