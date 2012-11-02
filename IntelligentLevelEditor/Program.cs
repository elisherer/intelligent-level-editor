using System;
using System.IO;
using System.Windows.Forms;
using IntelligentLevelEditor.Properties;

namespace IntelligentLevelEditor
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
            if (!string.IsNullOrEmpty(Settings.Default.Culture))
            {
                Settings.Default.Culture = "en-US";
                Settings.Default.Save();
            }
            if (File.Exists(Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Settings.Default.Culture + ".ini"))
                Localization.Load(Settings.Default.Culture);
            Application.Run(new FormEditor());
        }
    }
}
