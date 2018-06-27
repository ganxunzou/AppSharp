using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CefSharp;
using System.Diagnostics;

namespace AppSharp
{
    public partial class MainForm : Form
    {
        private static readonly bool DebuggingSubProcess = Debugger.IsAttached;
        private MdiClientPanel p = null;

        public MainForm()
        {
            InitializeComponent();

            initCef();

            // initMdiClientPanel();

            initTaskCenterPanel();
        }

        private void initCef()
        {
            var settings = new CefSettings();
            settings.RemoteDebuggingPort = 8088;
            //The location where cache data will be stored on disk. If empty an in-memory cache will be used for some features and a temporary disk cache for others.
            //HTML5 databases such as localStorage will only persist across sessions if a cache path is specified. 
            settings.CachePath = "cache";
            // settings.MultiThreadedMessageLoop = multiThreadedMessageLoop;

            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = CefSharpSchemeHandlerFactory.SchemeName,
                SchemeHandlerFactory = new CefSharpSchemeHandlerFactory()
            });

            if (!Cef.Initialize(settings, shutdownOnProcessExit: true, performDependencyCheck: !DebuggingSubProcess))
            {
                throw new Exception("Unable to Initialize Cef");
            }
        }

        private void initTaskCenterPanel()
        {
            string url = System.Windows.Forms.Application.StartupPath + "/task/Home.html";

            CefBrowserUserControl browser = new CefBrowserUserControl(url)
            {
                Dock = DockStyle.Fill
            };

            AppTabPage page = new AppTabPage()
            {
                Dock = DockStyle.Fill
            };
            page.Text = "任务中心";
            page.addCefBrowserUserControl(browser);

            this.moduleTabControl.Controls.Add(page);
        }

        /// <summary>
        /// 初始化 MdiClientPanel
        /// </summary>
        private void initMdiClientPanel()
        {
            p = new MdiClientPanel()
            {
                Dock = DockStyle.Fill,
                Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right))),
                Size = new Size(600, 370),
                Location = new System.Drawing.Point(0, 30)

            };

            Controls.Add(p);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ChildForm f = new ChildForm();
            f.MdiParent = p.MdiForm;
            f.Dock = DockStyle.Fill;
            f.Show();
        }
    }
}