using System.IO;

namespace Clear
{
    internal static class Required
    {
        internal readonly static string CoreFolder = Helper.ProgramData + "\\CLEAR_bat\\";
        internal readonly static string ScriptsFolder = Helper.ProgramData + "\\CLEAR_bat\\bat\\";
        internal readonly static string binFolder = Helper.ProgramData + "\\CLEAR_bat\\bin\\";
        internal readonly static string LogsFolder = Helper.ProgramData + "\\CLEAR_bat\\Logs\\";
        internal readonly static string TempFolder = Helper.ProgramData + "\\CLEAR_bat\\Temp\\";
        internal readonly static string DistrFolder = "C:\\iiko_Distr\\";


        internal static void Deploy()
        {
            if (!Directory.Exists(CoreFolder))
            {
                Directory.CreateDirectory(CoreFolder);
                Directory.CreateDirectory(DistrFolder);
            }
            if (!Directory.Exists(ScriptsFolder))
            {
                Directory.CreateDirectory(ScriptsFolder);
            }
            if (!Directory.Exists(binFolder))
            {
                Directory.CreateDirectory(binFolder);
            }
            if (!Directory.Exists(LogsFolder))
            {
                Directory.CreateDirectory(LogsFolder);
            }
            if (!Directory.Exists(TempFolder))
            {
                Directory.CreateDirectory(TempFolder);
            }

            try
            {
                File.WriteAllText(ScriptsFolder + "clear_cds.bat", Clear.Properties.Resources.clear_cds);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "clear_front.bat", Clear.Properties.Resources.clear_front);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "clear_iikocard.bat", Clear.Properties.Resources.clear_iikocard);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "corflags.bat", Clear.Properties.Resources.corflags);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "del_plazius.bat", Clear.Properties.Resources.del_plazius);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "iiko_ports.bat", Clear.Properties.Resources.iiko_ports);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "install_plugins.bat", Clear.Properties.Resources.install_plugins);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "start_front.bat", Clear.Properties.Resources.start_front);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "clean_all.bat", Clear.Properties.Resources.clean_all);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "clean_wo_pl.bat", Clear.Properties.Resources.clean_wo_pl);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "clear_system.bat", Clear.Properties.Resources.clear_system);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "update.bat", Clear.Properties.Resources.update);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "qr_win_upd.bat", Clear.Properties.Resources.qr_win_upd);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "stop_service.bat", Clear.Properties.Resources.stop_service);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "time_sync.bat", Clear.Properties.Resources.time_sync);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "folder_distr.bat", Clear.Properties.Resources.folder_distr);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "folder_iikofront.bat", Clear.Properties.Resources.folder_iikofront);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "folder_iikofrontlogs.bat", Clear.Properties.Resources.folder_iikofrontlogs);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "rmdir.bat", Clear.Properties.Resources.rmdir);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "iiko_versions.txt", Clear.Properties.Resources.iiko_versions);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "iiko_plugins.txt", Clear.Properties.Resources.iiko_plugins);
            }
            catch { }
            try
            {
                File.WriteAllText(ScriptsFolder + "folder_iikocard.txt", Clear.Properties.Resources.folder_iikocard);
            }
            catch { }
        }

        internal static void Clean()
        {
            Helper.EmptyFolder(CoreFolder);
        }
    }
}