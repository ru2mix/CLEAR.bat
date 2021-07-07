﻿using System;
using System.IO;

namespace Clear
{
    internal static class ErrorLogger
    {
        internal static string ErrorLogFile = Required.LogsFolder + "sys.log";

        internal static void LogError(string functionName, string errorMessage, string errorStackTrace)
        {
            try
            {
                File.AppendAllText(ErrorLogFile, string.Format("[ERROR] [{0}] in function [{1}]", DateTime.Now.ToString(), functionName));
                File.AppendAllText(ErrorLogFile, Environment.NewLine);
                File.AppendAllText(ErrorLogFile, errorMessage);
                File.AppendAllText(ErrorLogFile, Environment.NewLine);
                File.AppendAllText(ErrorLogFile, Environment.NewLine);
                File.AppendAllText(ErrorLogFile, errorStackTrace);

                // seperator
                File.AppendAllText(ErrorLogFile, Environment.NewLine);
                File.AppendAllText(ErrorLogFile, Environment.NewLine);
                File.AppendAllText(ErrorLogFile, Environment.NewLine);
            }
            catch { }
        }
    }
}
