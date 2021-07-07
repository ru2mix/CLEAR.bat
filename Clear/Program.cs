using System;
using System.IO;
using System.Windows.Forms;


namespace Clear
{
    static class Program
    {

        /* VERSION PROPERTIES */
        /* DO NOT LEAVE THEM EMPTY */

        // Enter current version here

        internal readonly static float Major = 7;
        internal readonly static float Minor = 8;

        internal readonly static bool EXPERIMENTAL_BUILD = false;

        internal static string GetCurrentVersionTostring()
        {
            return Major.ToString() + "." + Minor.ToString();
        }

        internal static float GetCurrentVersion()
        {
            return float.Parse(GetCurrentVersionTostring());
        }

        /* END OF VERSION PROPERTIES */

        // Enables the corresponding Windows tab for Windows Server machines
        internal static bool UNSAFE_MODE = false;

        const string _jsonAssembly = @"Optimizer.Newtonsoft.Json.dll";

        internal static Home Home;

        [STAThread]
        static void Main(string[] switches)
        {

            // check if another instance is running
            // problem? prevents auto-patching...

            //if (System.Diagnostics.Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Length > 1)
            //{
            //    MessageBox.Show("Optimizer is already running in the background!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Utilities.IsAdmin())
            {
                Application.Exit();
            }
            else
            {
                if (Utilities.IsCompatible())
                {

                        Required.Deploy();
                }
            }
                Application.Run(new Home());
        }
    }
}
