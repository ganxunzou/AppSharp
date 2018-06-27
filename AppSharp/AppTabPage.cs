using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;

namespace AppSharp
{
    public partial class AppTabPage : TabPage
    {
        public IWebBrowser Browser { get; private set; }

        public AppTabPage()
        {
            InitializeComponent();
        }

        public AppTabPage(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void addCefBrowserUserControl(CefBrowserUserControl browserUserControl)
        {
            this.Controls.Add(browserUserControl);

            this.Browser = browserUserControl.Browser;
        }
    }
}
