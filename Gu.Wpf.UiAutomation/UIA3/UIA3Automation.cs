namespace Gu.Wpf.UiAutomation.UIA3
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.EventHandlers;
    using Gu.Wpf.UiAutomation.UIA3.Extensions;
    using OperatingSystem = Gu.Wpf.UiAutomation.OperatingSystem;

    /// <summary>
    /// Automation implementation for UIA3
    /// </summary>
    public class UIA3Automation : AutomationBase
    {
        public UIA3Automation()
            : base(new UIA3PropertyLibrary(), new UIA3EventLibrary(), new UIA3PatternLibrary())
        {
            if (OperatingSystem.IsWindows8_1())
            {
                // Try CUIAutomation8 (Windows 8)
                try
                {
                    this.NativeAutomation = new Interop.UIAutomationClient.CUIAutomation8();
                }
                catch (COMException)
                {
                    // Fall back to CUIAutomation
                    this.NativeAutomation = new Interop.UIAutomationClient.CUIAutomation();
                }
            }
            else
            {
                this.NativeAutomation = new Interop.UIAutomationClient.CUIAutomation();
            }

            this.TreeWalkerFactory = new UIA3TreeWalkerFactory(this);
        }

        public override ITreeWalkerFactory TreeWalkerFactory { get; }

        public override object NotSupportedValue => this.NativeAutomation.ReservedNotSupportedValue;

        /// <summary>
        /// Native object for the ui automation
        /// </summary>
        public Interop.UIAutomationClient.IUIAutomation NativeAutomation { get; }

        /// <summary>
        /// Native object for Windows 8 automation
        /// </summary>
        public Interop.UIAutomationClient.IUIAutomation2 NativeAutomation2 => this.GetAutomationAs<Interop.UIAutomationClient.IUIAutomation2>();

        /// <summary>
        /// Native object for Windows 8.1 automation
        /// </summary>
        public Interop.UIAutomationClient.IUIAutomation3 NativeAutomation3 => this.GetAutomationAs<Interop.UIAutomationClient.IUIAutomation3>();

        public override AutomationElement GetDesktop()
        {
            return Com.Call(() =>
            {
                var desktop = CacheRequest.IsCachingActive
                    ? this.NativeAutomation.GetRootElementBuildCache(CacheRequest.Current.ToNative(this))
                    : this.NativeAutomation.GetRootElement();
                return this.WrapNativeElement(desktop);
            });
        }

        /// <summary>
        /// Creates an <see cref="AutomationElement" /> from a given point
        /// </summary>
        public override AutomationElement FromPoint(Point point)
        {
            return Com.Call(() =>
            {
                var nativePoint = point.ToTagPoint();
                var nativeElement = CacheRequest.IsCachingActive
                    ? this.NativeAutomation.ElementFromPointBuildCache(nativePoint, CacheRequest.Current.ToNative(this))
                    : this.NativeAutomation.ElementFromPoint(nativePoint);
                return this.WrapNativeElement(nativeElement);
            });
        }

        /// <summary>
        /// Creates an <see cref="AutomationElement" /> from a given windows handle (HWND)
        /// </summary>
        public override AutomationElement FromHandle(IntPtr hwnd)
        {
            return Com.Call(() =>
            {
                var nativeElement = CacheRequest.IsCachingActive
                    ? this.NativeAutomation.ElementFromHandleBuildCache(hwnd, CacheRequest.Current.ToNative(this))
                    : this.NativeAutomation.ElementFromHandle(hwnd);
                return this.WrapNativeElement(nativeElement);
            });
        }

        public override AutomationElement FocusedElement()
        {
            return Com.Call(() =>
            {
                var nativeElement = CacheRequest.IsCachingActive
                    ? this.NativeAutomation.GetFocusedElementBuildCache(CacheRequest.Current.ToNative(this))
                    : this.NativeAutomation.GetFocusedElement();
                return this.WrapNativeElement(nativeElement);
            });
        }

        public override IAutomationFocusChangedEventHandler RegisterFocusChangedEvent(Action<AutomationElement> action)
        {
            var eventHandler = new UIA3FocusChangedEventHandler(this, action);
            Com.Call(() => this.NativeAutomation.AddFocusChangedEventHandler(null, eventHandler));
            return eventHandler;
        }

        public override void UnRegisterFocusChangedEvent(IAutomationFocusChangedEventHandler eventHandler)
        {
            this.NativeAutomation.RemoveFocusChangedEventHandler((UIA3FocusChangedEventHandler)eventHandler);
        }

        public override void UnregisterAllEvents()
        {
            try
            {
                this.NativeAutomation.RemoveAllEventHandlers();
            }
            catch
            {
                // Noop
            }
        }

        public AutomationElement WrapNativeElement(Interop.UIAutomationClient.IUIAutomationElement nativeElement)
        {
            return nativeElement == null
                ? null
                : new AutomationElement(new UIA3BasicAutomationElement(this, nativeElement));
        }

        public override bool Compare(AutomationElement element1, AutomationElement element2)
        {
            return this.NativeAutomation.CompareElements(element1.ToNative(), element2.ToNative()) != 0;
        }

        /// <summary>
        /// Tries to cast the automation to a specific interface.
        /// Throws an exception if that is not possible.
        /// </summary>
        private T GetAutomationAs<T>()
            where T : class, Interop.UIAutomationClient.IUIAutomation
        {
            var element = this.NativeAutomation as T;
            if (element == null)
            {
                throw new NotSupportedException($"OS does not have {typeof(T).Name} support.");
            }

            return element;
        }
    }
}
