using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace AppSharp
{
    public partial class CefBrowserUserControl : UserControl
    {
        public IWebBrowser Browser { get; private set; }

        public CefBrowserUserControl(string url)
        {
            InitializeComponent();

            var browser = new ChromiumWebBrowser(url)
            {
                Dock = DockStyle.Fill
            };

            browserPanel.Controls.Add(browser);

            //Browser = browser;

            //browser.MenuHandler = new MenuHandler();
            //browser.RequestHandler = new WinFormsRequestHandler(openNewTab);
            //browser.JsDialogHandler = new JsDialogHandler();
            //browser.GeolocationHandler = new GeolocationHandler();
            //browser.DownloadHandler = new DownloadHandler();
            browser.KeyboardHandler = new KeyboardHandler();
            //browser.LifeSpanHandler = new LifeSpanHandler();
            //browser.LoadingStateChanged += OnBrowserLoadingStateChanged;
            //browser.ConsoleMessage += OnBrowserConsoleMessage;
            //browser.TitleChanged += OnBrowserTitleChanged;
            //browser.AddressChanged += OnBrowserAddressChanged;
            //browser.StatusMessage += OnBrowserStatusMessage;
            //browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
            //browser.LoadError += OnLoadError;
            //browser.DragHandler = new DragHandler();
            //browser.RegisterJsObject("bound", new BoundObject());
            //browser.RegisterAsyncJsObject("boundAsync", new AsyncBoundObject());
            //browser.RenderProcessMessageHandler = new RenderProcessMessageHandler();
            ////browser.MouseDown += OnBrowserMouseClick;
            //browser.HandleCreated += OnBrowserHandleCreated;
            ////browser.ResourceHandlerFactory = new FlashResourceHandlerFactory();

            //var eventObject = new ScriptedMethodsBoundObject();
            //eventObject.EventArrived += OnJavascriptEventArrived;
            //// Use the default of camelCaseJavascriptNames
            //// .Net methods starting with a capitol will be translated to starting with a lower case letter when called from js
            //browser.RegisterJsObject("boundEvent", eventObject, camelCaseJavascriptNames: true);

            //CefExample.RegisterTestResources(browser);

            //var version = String.Format("Chromium: {0}, CEF: {1}, CefSharp: {2}", Cef.ChromiumVersion, Cef.CefVersion, Cef.CefSharpVersion);
            //DisplayOutput(version);
        }
    }
}
