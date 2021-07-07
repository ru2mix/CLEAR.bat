﻿using System;
using System.IO;

namespace Clear
{
    internal static class Helper
    {
        // Информация о версии
        internal readonly static string VerInfo = " Stable";

        // Загрузка системных папок
        internal static readonly string System32Folder = Environment.GetFolderPath(Environment.SpecialFolder.System);
        internal static readonly string TempFolder = Path.GetTempPath();
        internal static readonly string ProfileAppDataRoaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        internal static readonly string ProgramData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        internal static readonly string ProfileAppDataLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        internal static readonly string ProfileAppDataLocalLow = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low";
        internal static readonly string OSDrive = System32Folder.Substring(0, 3);
        internal static readonly string OSDriveWindows = Environment.GetEnvironmentVariable("WINDIR", EnvironmentVariableTarget.Machine);

        internal static void EmptyFolder(string path)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);

                foreach (FileInfo file in di.GetFiles())
                {
                    try
                    {
                        file.IsReadOnly = false;
                        file.Delete();
                    }
                    catch { }
                }

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    try
                    {
                        dir.Delete(true);
                    }
                    catch { }
                }
            }
            catch { }
        }

        internal static void CleanTemporaries()
        {
            EmptyFolder(TempFolder);
        }





        internal static void CleanMiniDumps()
        {
            EmptyFolder(OSDriveWindows + "\\Minidump");
        }

        internal static void CleanErrorReports()
        {
            EmptyFolder(ProfileAppDataLocal + "\\Microsoft\\Windows\\WER\\ReportArchive");
            EmptyFolder(ProfileAppDataLocal + "\\Microsoft\\Windows\\WER\\ReportQueue");
            EmptyFolder(ProfileAppDataLocal + "\\Microsoft\\Windows\\WER\\Temp");
            EmptyFolder(ProfileAppDataLocal + "\\Microsoft\\Windows\\WER\\ERC");
            EmptyFolder(ProgramData + "\\Microsoft\\Windows\\WER\\ReportArchive");
            EmptyFolder(ProgramData + "\\Microsoft\\Windows\\WER\\ReportQueue");
            EmptyFolder(ProgramData + "\\Microsoft\\Windows\\WER\\Temp");
            EmptyFolder(ProgramData + "\\Microsoft\\Windows\\WER\\ERC");
        }

        internal static void CleanMediaPlayersCache()
        {
            EmptyFolder(ProfileAppDataLocal + "\\Microsoft\\Media Player");
            EmptyFolder(ProfileAppDataLocalLow + "\\Apple Computer\\QuickTime\\downloads");
            EmptyFolder(ProfileAppDataRoaming + "\\Macromedia");

            try
            {
                File.Delete(ProfileAppDataLocalLow + "\\Apple Computer\\QuickTime\\QTPlayerSession.xml");
            }
            catch { }
        }


        internal static void CleanLogs()
        {
            EmptyFolder(System32Folder + "\\LogFiles");
            EmptyFolder(OSDrive + "\\inetpub\\logs\\LogFiles");
        }

    }
}
