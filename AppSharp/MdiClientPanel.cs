using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace AppSharp
{
    /// <summary>
    /// MdiClient Panel 容器
    /// 可以在Form中动态加入MdiClientPanel，然后在将其他的Form作为Child Form 放入 MdiClient中
    /// 从而解决 MDI Form 不能被添加到其他的Form中的问题
    /// </summary>
    public partial class MdiClientPanel : Panel 
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_CLIENTEDGE = 0x200;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOREDRAW = 0x0008;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_FRAMECHANGED = 0x0020;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const uint SWP_HIDEWINDOW = 0x0080;
        private const uint SWP_NOCOPYBITS = 0x0100;
        private const uint SWP_NOOWNERZORDER = 0x0200;
        private const uint SWP_NOSENDCHANGING = 0x0400;

        private Form mdiForm;
        private MdiClient ctlClient = new MdiClient();
        
        public MdiClientPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public MdiClientPanel()
        {
            InitializeComponent();

            initMdiClientStyle();

            base.Controls.Add(this.ctlClient);
            
        }

        /// <summary>
        /// 初始化 MdiClient 的样式
        /// </summary>
        private void initMdiClientStyle()
        {
            ctlClient.BackColor = System.Drawing.Color.FromArgb(0, 19, 49);
            cleanMdiClient3dBorder();
        }

        /// <summary>
        /// 清除MdiClient 3D 边框
        /// </summary>
        private void cleanMdiClient3dBorder()
        {
            int windowLong = GetWindowLong(ctlClient.Handle, GWL_EXSTYLE);

            //if (mdiForm.)
            //{
            //    windowLong |= WS_EX_CLIENTEDGE;
            //}
            //else
            //{
            //    windowLong &= ~WS_EX_CLIENTEDGE;
            //}
            windowLong &= ~WS_EX_CLIENTEDGE;

            SetWindowLong(ctlClient.Handle, GWL_EXSTYLE, windowLong);

            // Update the non-client area.
            SetWindowPos(ctlClient.Handle, IntPtr.Zero, 0, 0, 0, 0,
                SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER |
                SWP_NOOWNERZORDER | SWP_FRAMECHANGED);
        }
        
        /// <summary>
        /// 获取MdiForm
        /// </summary>
        public Form MdiForm
        {
            get
            { 
                if (this.mdiForm == null)
                {
                    this.mdiForm = new Form();
                    this.mdiForm.FormBorderStyle = FormBorderStyle.None;
                    // this.mdiForm.BackColor = System.Drawing.Color.FromArgb(0, 19, 49); 
                    /// set the hidden ctlClient field which is used to determine if the form is an MDI form
                    System.Reflection.FieldInfo field = typeof(Form).GetField("ctlClient", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    field.SetValue(this.mdiForm, this.ctlClient);
                }
                return this.mdiForm;
            }
        }
    }
}
