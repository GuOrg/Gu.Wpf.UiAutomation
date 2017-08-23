namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Globalization;
    using System.Windows;

    public class AutomationElementPropertyValues
    {
        private AutomationProperty<string> acceleratorKey;
        private AutomationProperty<string> accessKey;
        private AutomationProperty<string> ariaProperties;
        private AutomationProperty<string> ariaRole;
        private AutomationProperty<string> automationId;
        private AutomationProperty<Rect> boundingRectangle;
        private AutomationProperty<string> className;
        private AutomationProperty<Point> clickablePoint;
        private AutomationProperty<AutomationElement[]> controllerFor;
        private AutomationProperty<ControlType> controlType;
        private AutomationProperty<CultureInfo> culture;
        private AutomationProperty<AutomationElement[]> describedBy;
        private AutomationProperty<AutomationElement[]> flowsFrom;
        private AutomationProperty<AutomationElement[]> flowsTo;
        private AutomationProperty<string> frameworkId;
        private AutomationProperty<bool> hasKeyboardFocus;
        private AutomationProperty<string> helpText;
        private AutomationProperty<bool> isContentElement;
        private AutomationProperty<bool> isControlElement;
        private AutomationProperty<bool> isDataValidForForm;
        private AutomationProperty<bool> isEnabled;
        private AutomationProperty<bool> isKeyboardFocusable;
        private AutomationProperty<bool> isOffscreen;
        private AutomationProperty<bool> isPassword;
        private AutomationProperty<bool> isPeripheral;
        private AutomationProperty<bool> isRequiredForForm;
        private AutomationProperty<string> itemStatus;
        private AutomationProperty<string> itemType;
        private AutomationProperty<AutomationElement> labeledBy;
        private AutomationProperty<LiveSetting> liveSetting;
        private AutomationProperty<string> localizedControlType;
        private AutomationProperty<string> name;
        private AutomationProperty<IntPtr> nativeWindowHandle;
        private AutomationProperty<bool> optimizeForVisualContent;
        private AutomationProperty<OrientationType> orientation;
        private AutomationProperty<int> processId;
        private AutomationProperty<string> providerDescription;
        private AutomationProperty<int[]> runtimeId;

        public AutomationElementPropertyValues(BasicAutomationElementBase basicAutomationElement)
        {
            this.BasicAutomationElement = basicAutomationElement;
        }

        private BasicAutomationElementBase BasicAutomationElement { get; }

        private AutomationBase Automation => this.BasicAutomationElement.Automation;

        private IAutomationElementProperties Properties => this.Automation.PropertyLibrary.Element;

        public AutomationProperty<string> AcceleratorKey => this.GetOrCreate(ref this.acceleratorKey, this.Properties.AcceleratorKey);

        public AutomationProperty<string> AccessKey => this.GetOrCreate(ref this.accessKey, this.Properties.AccessKey);

        public AutomationProperty<string> AriaProperties => this.GetOrCreate(ref this.ariaProperties, this.Properties.AriaProperties);

        public AutomationProperty<string> AriaRole => this.GetOrCreate(ref this.ariaRole, this.Properties.AriaRole);

        public AutomationProperty<string> AutomationId => this.GetOrCreate(ref this.automationId, this.Properties.AutomationId);

        public AutomationProperty<Rect> BoundingRectangle => this.GetOrCreate(ref this.boundingRectangle, this.Properties.BoundingRectangle);

        public AutomationProperty<string> ClassName => this.GetOrCreate(ref this.className, this.Properties.ClassName);

        public AutomationProperty<Point> ClickablePoint => this.GetOrCreate(ref this.clickablePoint, this.Properties.ClickablePoint);

        public AutomationProperty<AutomationElement[]> ControllerFor => this.GetOrCreate(ref this.controllerFor, this.Properties.ControllerFor);

        public AutomationProperty<ControlType> ControlType => this.GetOrCreate(ref this.controlType, this.Properties.ControlType);

        public AutomationProperty<CultureInfo> Culture => this.GetOrCreate(ref this.culture, this.Properties.Culture);

        public AutomationProperty<AutomationElement[]> DescribedBy => this.GetOrCreate(ref this.describedBy, this.Properties.DescribedBy);

        public AutomationProperty<AutomationElement[]> FlowsFrom => this.GetOrCreate(ref this.flowsFrom, this.Properties.FlowsFrom);

        public AutomationProperty<AutomationElement[]> FlowsTo => this.GetOrCreate(ref this.flowsTo, this.Properties.FlowsTo);

        public AutomationProperty<string> FrameworkId => this.GetOrCreate(ref this.frameworkId, this.Properties.FrameworkId);

        public AutomationProperty<bool> HasKeyboardFocus => this.GetOrCreate(ref this.hasKeyboardFocus, this.Properties.HasKeyboardFocus);

        public AutomationProperty<string> HelpText => this.GetOrCreate(ref this.helpText, this.Properties.HelpText);

        public AutomationProperty<bool> IsContentElement => this.GetOrCreate(ref this.isContentElement, this.Properties.IsContentElement);

        public AutomationProperty<bool> IsControlElement => this.GetOrCreate(ref this.isControlElement, this.Properties.IsControlElement);

        public AutomationProperty<bool> IsDataValidForForm => this.GetOrCreate(ref this.isDataValidForForm, this.Properties.IsDataValidForForm);

        public AutomationProperty<bool> IsEnabled => this.GetOrCreate(ref this.isEnabled, this.Properties.IsEnabled);

        public AutomationProperty<bool> IsKeyboardFocusable => this.GetOrCreate(ref this.isKeyboardFocusable, this.Properties.IsKeyboardFocusable);

        public AutomationProperty<bool> IsOffscreen => this.GetOrCreate(ref this.isOffscreen, this.Properties.IsOffscreen);

        public AutomationProperty<bool> IsPassword => this.GetOrCreate(ref this.isPassword, this.Properties.IsPassword);

        public AutomationProperty<bool> IsPeripheral => this.GetOrCreate(ref this.isPeripheral, this.Properties.IsPeripheral);

        public AutomationProperty<bool> IsRequiredForForm => this.GetOrCreate(ref this.isRequiredForForm, this.Properties.IsRequiredForForm);

        public AutomationProperty<string> ItemStatus => this.GetOrCreate(ref this.itemStatus, this.Properties.ItemStatus);

        public AutomationProperty<string> ItemType => this.GetOrCreate(ref this.itemType, this.Properties.ItemType);

        public AutomationProperty<AutomationElement> LabeledBy => this.GetOrCreate(ref this.labeledBy, this.Properties.LabeledBy);

        public AutomationProperty<LiveSetting> LiveSetting => this.GetOrCreate(ref this.liveSetting, this.Properties.LiveSetting);

        public AutomationProperty<string> LocalizedControlType => this.GetOrCreate(ref this.localizedControlType, this.Properties.LocalizedControlType);

        public AutomationProperty<string> Name => this.GetOrCreate(ref this.name, this.Properties.Name);

        public AutomationProperty<IntPtr> NativeWindowHandle => this.GetOrCreate(ref this.nativeWindowHandle, this.Properties.NativeWindowHandle);

        public AutomationProperty<bool> OptimizeForVisualContent => this.GetOrCreate(ref this.optimizeForVisualContent, this.Properties.OptimizeForVisualContent);

        public AutomationProperty<OrientationType> Orientation => this.GetOrCreate(ref this.orientation, this.Properties.Orientation);

        public AutomationProperty<int> ProcessId => this.GetOrCreate(ref this.processId, this.Properties.ProcessId);

        public AutomationProperty<string> ProviderDescription => this.GetOrCreate(ref this.providerDescription, this.Properties.ProviderDescription);

        public AutomationProperty<int[]> RuntimeId => this.GetOrCreate(ref this.runtimeId, this.Properties.RuntimeId);

        private AutomationProperty<T> GetOrCreate<T>(ref AutomationProperty<T> val, PropertyId propertyId)
        {
            return val ?? (val = new AutomationProperty<T>(propertyId, this.BasicAutomationElement));
        }
    }
}
