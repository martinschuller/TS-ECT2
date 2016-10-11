using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

namespace TS_ECT2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

            bool createdNew;

            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out createdNew);
            if (createdNew)
            {
                // bitte Form1 ersetzen 
                Application.Run(new MainWindow());
                // und auch wieder Freigeben besser ist besser
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("Program is still running!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
