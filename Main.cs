﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace x1
{
    public partial class Main : Form
    {
        #region epic dLLimportz
        [DllImport("user32.dll")]
        static extern ushort GetAsyncKeyState(int vKey);

        public static bool IsKeyPushedDown(System.Windows.Forms.Keys vKey)
        {
            return 0 != (GetAsyncKeyState((int)vKey) & 0x8000);
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        
        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        private static IntPtr m_hTaskBar;

        public static void HideTaskBar()
        {
            m_hTaskBar = FindWindow("Shell_TrayWnd", null);

            if ((int)m_hTaskBar != 0)
            {
                ShowWindow(m_hTaskBar, 0);
            }
        }
        #endregion

        public Main()
        {
            InitializeComponent();
        }

        bool helpdialog = false;

        Help help = new Help();


        private void Main_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 1040);
            t1.Start();

            HideTaskBar();
        }

        #region haha im epic
        private void t1_Tick(object sender, EventArgs e)
        {
            title.Text = GetActiveWindowTitle();
            time.Text = DateTime.Now.ToString("HH:mm");

            if (IsKeyPushedDown(Keys.F7) && helpdialog == false)
            {
                help.Show();
                helpdialog = true;
            }

            if (IsKeyPushedDown(Keys.F8) && helpdialog == true)
            {
                helpdialog = false;
                help.Hide();
            }

            if (IsKeyPushedDown(Keys.CapsLock))
            {
                if (IsKeyLocked(Keys.CapsLock))
                {
                    nt1.ShowBalloonTip(5, "x1", "capslock enabled", ToolTipIcon.None);
                }
                else
                {
                    nt1.ShowBalloonTip(5, "x1", "capslock disabled", ToolTipIcon.None);
                }
            }
        }
        #endregion
    }
}
