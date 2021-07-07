using FluentFTP;
using HtmlAgilityPack;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Clear
{
    internal static class Utilities
    {
        internal static readonly string LocalMachineRun = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        internal static readonly string LocalMachineRunOnce = "Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce";
        internal static readonly string LocalMachineRunWoW = "Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run";
        internal static readonly string LocalMachineRunOnceWow = "Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\RunOnce";
        internal static readonly string CurrentUserRun = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        internal static readonly string CurrentUserRunOnce = "Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce";

        internal static readonly string LocalMachineStartupFolder = Helper.ProgramData + "\\Microsoft\\Windows\\Start Menu\\Programs\\Startup";
        internal static readonly string CurrentUserStartupFolder = Helper.ProfileAppDataRoaming + "\\Microsoft\\Windows\\Start Menu\\Programs\\Startup";

        internal readonly static string DefaultEdgeDownloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

        internal static WindowsVersion CurrentWindowsVersion = WindowsVersion.Unsupported;

        internal static Ping pinger = new Ping();

        internal delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        internal static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }

        internal static IEnumerable<Control> GetSelfAndChildrenRecursive(Control parent)
        {
            List<Control> controls = new List<Control>();

            foreach (Control child in parent.Controls)
            {
                controls.AddRange(GetSelfAndChildrenRecursive(child));
            }

            controls.Add(parent);
            return controls;
        }

        internal static Color ToGrayScale(this Color originalColor)
        {
            if (originalColor.Equals(Color.Transparent))
                return originalColor;

            int grayScale = (int)((originalColor.R * .299) + (originalColor.G * .587) + (originalColor.B * .114));
            return Color.FromArgb(grayScale, grayScale, grayScale);
        }





        internal static string GetOS()
        {
            string os = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ProductName", "");

            if (os.Contains("Windows 7"))
            {
                CurrentWindowsVersion = WindowsVersion.Windows7;
            }
            if ((os.Contains("Windows 8")) || (os.Contains("Windows 8.1")))
            {
                CurrentWindowsVersion = WindowsVersion.Windows8;
            }
            if (os.Contains("Windows 10"))
            {
                CurrentWindowsVersion = WindowsVersion.Windows10;
            }

            if (Program.UNSAFE_MODE)
            {
                if (os.Contains("Windows Server 2008"))
                {
                    CurrentWindowsVersion = WindowsVersion.Windows7;
                }
                if (os.Contains("Windows Server 2012"))
                {
                    CurrentWindowsVersion = WindowsVersion.Windows8;
                }
                if (os.Contains("Windows Server 2016"))
                {
                    CurrentWindowsVersion = WindowsVersion.Windows10;
                }
            }

            return os;
        }

        internal static string GetBitness()
        {
            string bitness = string.Empty;

            if (Environment.Is64BitOperatingSystem)
            {
                bitness = "You are working with 64-bit architecture";
            }
            else
            {
                bitness = "You are working with 32-bit architecture";
            }

            return bitness;
        }

        internal static bool IsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        internal static bool IsCompatible()
        {
            bool legit;
            string os = GetOS();

            if ((os.Contains("XP")) || (os.Contains("Vista")) || os.Contains("Server 2003"))
            {
                legit = false;
            }
            else
            {
                legit = true;
            }
            return legit;
        }



        internal static void SetEdgeDownloadFolder(string path)
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Edge", "DownloadDirectory", path, RegistryValueKind.String);
        }

        internal static void RunBatchFile(string batchFile)
        {
            try
            {
                using (Process p = new Process())
                {
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.FileName = batchFile;
                    p.StartInfo.UseShellExecute = false;

                    p.Start();
                    p.WaitForExit();
                    p.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Utilities.RunBatchFile", ex.Message, ex.StackTrace);
            }
        }
        internal static void RunBatchFileNoHidden(string batchFile)
        {
            try
            {
                using (Process pnh = new Process())
                {
                    pnh.StartInfo.CreateNoWindow = false;
                    pnh.StartInfo.FileName = batchFile;
                    pnh.StartInfo.UseShellExecute = false;

                    pnh.Start();
                    pnh.WaitForExit();
                    pnh.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Utilities.RunBatchFile", ex.Message, ex.StackTrace);
            }
        }
        // Генератор Config BACKOFFICE
        internal static void CofnigBackOffice(string url, string port, string http, string dir, string edition, string versionraw, string ComputerName, string serverName)
        {
            try
            {
                string false1 = @"""false""";
                string true1 = @"""true""";
                string ten1 = @"""1.0""";
                string XMLSchema = @"""http://www.w3.org/2001/XMLSchema""";
                string XMLSchema1 = @"""http://www.w3.org/2001/XMLSchema-instance""";
                string config = "<?xml version=" + ten1 + "?> " + Environment.NewLine + @"<config xmlns:xsd=" + XMLSchema + " xmlns:xsi=" + XMLSchema1 + "> " + Environment.NewLine + @"  <ServerProtocol>unknown</ServerProtocol> " + Environment.NewLine + @"  <ServerAddr>localhost</ServerAddr> " + Environment.NewLine + @"  <ServerPort>8080</ServerPort> " + Environment.NewLine + @"  <ServerSubUrl>/resto</ServerSubUrl> " + Environment.NewLine + @"  <ChainServerProtocol>unknown</ChainServerProtocol> " + Environment.NewLine + @"  <ChainServerAddr>localhost</ChainServerAddr> " + Environment.NewLine + @"  <ChainServerPort>9080</ChainServerPort> " + Environment.NewLine + @"  <ChainServerSubUrl>/resto</ChainServerSubUrl> " + Environment.NewLine + @"  <ServersList> " + Environment.NewLine + @"    <ServerName>CLEAR.bat</ServerName> " + Environment.NewLine + @"    <Edition>" + edition + "</Edition> " + Environment.NewLine + @"    <Version>" + versionraw + "</Version> " + Environment.NewLine + @"    <ComputerName>" + ComputerName + "</ComputerName> " + Environment.NewLine + @"    <ServerState>STARTED_SUCCESSFULLY</ServerState> " + Environment.NewLine + @"    <Protocol>" + http + "</Protocol> " + Environment.NewLine + @"    <ServerAddr>" + url + "</ServerAddr> " + Environment.NewLine + @"    <ServerSubUrl>/resto</ServerSubUrl> " + Environment.NewLine + @"    <Port>" + port + "</Port> " + Environment.NewLine + @"    <IsPresent>true</IsPresent> " + Environment.NewLine + @"  </ServersList> " + Environment.NewLine + @"  <serverChooserIsOpened>true</serverChooserIsOpened> " + Environment.NewLine + @"  <Login>admin</Login> " + Environment.NewLine + @"  <Update> " + Environment.NewLine + @"    <Url>update</Url> " + Environment.NewLine + @"  </Update> " + Environment.NewLine + @"  <SaveLogin>true</SaveLogin> " + Environment.NewLine + @"  <ShouldGroupGoodMoveReport>true</ShouldGroupGoodMoveReport> " + Environment.NewLine + @"  <crashReportServerAddr>https://sevilia.glowbyte.com/restobugs/crash.php</crashReportServerAddr> " + Environment.NewLine + @"  <OneCExportPath>C:\1C</OneCExportPath> " + Environment.NewLine + @"  <NomenclatureExportFileName>C:\Users\2mix\Documents\menu.xml.gz</NomenclatureExportFileName> " + Environment.NewLine + @"  <NomenclatureImportFileName>C:\Users\2mix\Documents\menu.xml.gz</NomenclatureImportFileName> " + Environment.NewLine + @"  <MenuOpenGroupName /> " + Environment.NewLine + @"  <NavPanelDockVisibility>Visible</NavPanelDockVisibility> " + Environment.NewLine + @"  <ProductionOrderLastBlank xsi:nil=" + true1 + " /> " + Environment.NewLine + @"  <OrderPropertiesPosition /> " + Environment.NewLine + @"  <OrderPropertiesFio /> " + Environment.NewLine + @"  <ShowProductEditor>false</ShowProductEditor> " + Environment.NewLine + @"  <ShowPositions>false</ShowPositions> " + Environment.NewLine + @"  <ShowMenu /> " + Environment.NewLine + @"  <MenuChangesSplitterWidth xsi:nil=" + true1 + " /> " + Environment.NewLine + @"  <RoundPercent xsi:nil=" + true1 + " /> " + Environment.NewLine + @"  <needSubstituteSupplierPrice>true</needSubstituteSupplierPrice> " + Environment.NewLine + @"  <ModificatorsIsProcessed>false</ModificatorsIsProcessed> " + Environment.NewLine + @"  <NeedSubstituteLastInvoiceDate>false</NeedSubstituteLastInvoiceDate> " + Environment.NewLine + @"  <UseSupplierProducts>false</UseSupplierProducts> " + Environment.NewLine + @"  <LastInvoiceDate xsi:nil=" + true1 + " /> " + Environment.NewLine + @"  <needSubstituteLastInvoiceStore>false</needSubstituteLastInvoiceStore> " + Environment.NewLine + @"  <LastInvoiceStore xsi:nil=" + true1 + " /> " + Environment.NewLine + @"  <GoodsMoveShowNds xsi:nil=" + true1 + " /> " + Environment.NewLine + @"  <GoodsMoveShowAmount xsi:nil=" + true1 + " /> " + Environment.NewLine + @"  <ReplaceInGroupActionForm>false</ReplaceInGroupActionForm> " + Environment.NewLine + @"  <ResortOnNomenclatureCacheUpdate>true</ResortOnNomenclatureCacheUpdate> " + Environment.NewLine + @"  <AutoUpdateNomenclature>true</AutoUpdateNomenclature> " + Environment.NewLine + @"  <UpdateCacheAfterReconnection>false</UpdateCacheAfterReconnection> " + Environment.NewLine + @"  <CheckObjectInCacheForConverter>false</CheckObjectInCacheForConverter> " + Environment.NewLine + @"  <ServiceMode>FAST_FOOD</ServiceMode> " + Environment.NewLine + @"  <MaxUserPhotoWidth>600</MaxUserPhotoWidth> " + Environment.NewLine + @"  <MaxUserPhotoHeight>600</MaxUserPhotoHeight> " + Environment.NewLine + @"  <ProductPhotoMaxWidth>600</ProductPhotoMaxWidth> " + Environment.NewLine + @"  <ProductPhotoMaxHeight>600</ProductPhotoMaxHeight> " + Environment.NewLine + @"  <RecomputeSteadySalary>false</RecomputeSteadySalary> " + Environment.NewLine + @"  <RecomputeSalaryDates> " + Environment.NewLine + @"    <item> " + Environment.NewLine + @"      <key> " + Environment.NewLine + @"        <string>start</string> " + Environment.NewLine + @"      </key> " + Environment.NewLine + @"      <value> " + Environment.NewLine + @"        <dateTime xsi:nil=" + true1 + " /> " + Environment.NewLine + @"      </value> " + Environment.NewLine + @"    </item> " + Environment.NewLine + @"    <item> " + Environment.NewLine + @"      <key> " + Environment.NewLine + @"        <string>finish</string> " + Environment.NewLine + @"      </key> " + Environment.NewLine + @"      <value> " + Environment.NewLine + @"        <dateTime xsi:nil=" + true1 + " /> " + Environment.NewLine + @"      </value> " + Environment.NewLine + @"    </item> " + Environment.NewLine + @"  </RecomputeSalaryDates> " + Environment.NewLine + @"  <UICultureName>ru-RU</UICultureName> " + Environment.NewLine + @"  <ShowGroupsHierarchyForProductInCompletion>true</ShowGroupsHierarchyForProductInCompletion> " + Environment.NewLine + @"  <ShowPaidInvoices>false</ShowPaidInvoices> " + Environment.NewLine + @"  <ShowPaidOutgoingDocuments>false</ShowPaidOutgoingDocuments> " + Environment.NewLine + @"  <backVersion /> " + Environment.NewLine + @"  <KeepAliveHttpWebRequestFlag>false</KeepAliveHttpWebRequestFlag> " + Environment.NewLine + @"  <RepeatCallsServerCount>0</RepeatCallsServerCount> " + Environment.NewLine + @"  <RepeatCallsServerTimeoutIsMs>500</RepeatCallsServerTimeoutIsMs> " + Environment.NewLine + @"  <AutoUpdateAnnouncements>true</AutoUpdateAnnouncements> " + Environment.NewLine + @"  <IikoNewsRssUrl /> " + Environment.NewLine + @"  <IikoNewsCacheMinutes>60</IikoNewsCacheMinutes> " + Environment.NewLine + @"  <deliveryCustomersCanBeExportedToExcel>true</deliveryCustomersCanBeExportedToExcel> " + Environment.NewLine + @"  <DeliveryAggregationFieldsVisible>true</DeliveryAggregationFieldsVisible> " + Environment.NewLine + @"  <DailyCacheSaving>true</DailyCacheSaving> " + Environment.NewLine + @"  <CacheSavingPeriod>5</CacheSavingPeriod> " + Environment.NewLine + @"  <CacheSavingMinutesTime>480</CacheSavingMinutesTime> " + Environment.NewLine + @"  <canArchiveCache>true</canArchiveCache> " + Environment.NewLine + @"  <IikoNetOperationTimeoutSeconds xsi:nil=" + true1 + " /> " + Environment.NewLine + @"  <IikoNetEndpointIdentity>iiko.net</IikoNetEndpointIdentity> " + Environment.NewLine + @"  <StiSubreportSettingsPrintOnPreviousPage>false</StiSubreportSettingsPrintOnPreviousPage> " + Environment.NewLine + @"  <SyncReportDockPanelHeight xsi:nil=" + true1 + " /> " + Environment.NewLine + @"  <TraceAbcXyzAnalysis>false</TraceAbcXyzAnalysis> " + Environment.NewLine + @"  <YandexUrlMaskv2>http://maps\.yandex\.(?&lt;domain&gt;\w{2,9})/(export/usermaps/(?&lt;id&gt;(-|\w)+)/?|\?um=(?&lt;id&gt;(-|\w)+)\S*)</YandexUrlMaskv2> " + Environment.NewLine + @"  <YandexUrlFormatv2>http://maps.yandex.{domain}/export/usermaps/{id}/</YandexUrlFormatv2> " + Environment.NewLine + @"  <GoogleUrlMaskv2>https://.*\.google\.(?&lt;domain&gt;\w{2,9}?)/.*id=(?&lt;id&gt;([^?&amp;])+)</GoogleUrlMaskv2> " + Environment.NewLine + @"  <GoogleUrlFormatv2>https://www.google.{domain}/maps/d/view?mid={id}</GoogleUrlFormatv2> " + Environment.NewLine + @"  <EnableOzekiLog>false</EnableOzekiLog> " + Environment.NewLine + @"  <audioDevicesDetectionTimeout>00:00:05</audioDevicesDetectionTimeout> " + Environment.NewLine + @"</config>";



                if (Directory.Exists(Helper.ProfileAppDataRoaming + @"\iiko\Rms\CLEAR_bat"))
                {
                    try
                    {
                        Directory.Delete(Helper.ProfileAppDataRoaming + @"\iiko\Rms\CLEAR_bat", true);
                    }
                    catch
                    {
                    }

                }
                {
                    Directory.CreateDirectory(Helper.ProfileAppDataRoaming + @"\iiko\Rms\CLEAR_bat");
                    Directory.CreateDirectory(Helper.ProfileAppDataRoaming + @"\iiko\Rms\CLEAR_bat\config");
                    File.WriteAllText(Helper.ProfileAppDataRoaming + @"\iiko\Rms\CLEAR_bat\config\backclient.config.xml", config);
                }
                {

                    System.Threading.Thread.Sleep(1000);
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.FileName = dir;
                    p.StartInfo.Arguments = "/AdditionalTmpFolder=CLEAR_bat";
                    p.Start();

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Utilities.CofnigBackOffice", ex.Message, ex.StackTrace);
            }

        }

        // FTP Скачивалки
        internal static void TestFTPExeDownload(string iver, string setup, string name)
        {
            try
            {
                using (var ftp = new FtpClient("ftp.iiko.ru", "partners", "partners#iiko"))
                {
                    ftp.Connect();

                    // define the progress tracking callback
                    Action<FtpProgress> progress = delegate (FtpProgress p)
                    {
                        if (p.Progress == 1)
                        {

                        }
                        else
                        {
                            // percent done = (p.Progress * 100)

                        }
                    };

                    // download a file with progress tracking
                    ftp.DownloadFile(@"C:\iiko_Distr\" + name, @"/release_iiko/" + iver + setup, FtpLocalExists.Overwrite, FtpVerify.None, progress);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Utilities.TestFTPFolderDownload", ex.Message, ex.StackTrace);
            }
        }
        //
        // FTP exe файлы
        internal static void DownloadFTPEXE(string iver, string setup, string name)
        {
            try
            {
                FtpWebRequest frpWebRequest = (FtpWebRequest)FtpWebRequest.Create("ftp://ftp.iiko.ru/release_iiko/" + iver + setup);
                frpWebRequest.Credentials = new NetworkCredential("partners", "partners#iiko");
                frpWebRequest.KeepAlive = true;
                frpWebRequest.UsePassive = true;
                frpWebRequest.UseBinary = true;
                frpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                FtpWebResponse response = (FtpWebResponse)frpWebRequest.GetResponse();
                Stream stream = response.GetResponseStream();
                List<byte> list = new List<byte>();
                int b;
                while ((b = stream.ReadByte()) != -1)
                    list.Add((byte)b);
                File.WriteAllBytes(@"C:\iiko_Distr\" + name, list.ToArray());
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Utilities.DownloadFTPEXE", ex.Message, ex.StackTrace);
            }
        }
        // Получение списка плагинов

        internal static void UpdatePluginsList(string iver)
        {
            try
            {
                //Получение списка папок с ftp
                string path = Helper.ProgramData + @"\CLEAR_bat\bat\iiko_plugins.txt";
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.iiko.ru/release_iiko/" + iver + "/Plugins/Front/");
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential("partners", "partners#iiko");
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                File.WriteAllText(path, reader.ReadToEnd());

                reader.Close();
                response.Close();
                // Удаление старых версий из списка
                string tempFile = Path.GetTempFileName();
                var fileList3 = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_plugins.txt").ToList(); for (var i = fileList3.Count - 1; i >= 0; i--) { if (fileList3[i].EndsWith(".zip")) fileList3.RemoveAt(i); }
                File.WriteAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_plugins.txt", fileList3);
                var fileList4 = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_plugins.txt").ToList(); for (var i = fileList4.Count - 1; i >= 0; i--) { if (fileList4[i].EndsWith("Logs")) fileList4.RemoveAt(i); }
                File.WriteAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_plugins.txt", fileList4);
                var fileList5 = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_plugins.txt").ToList(); for (var i = fileList5.Count - 1; i >= 0; i--) { if (fileList5[i].EndsWith("RestartHelper")) fileList5.RemoveAt(i); }
                File.WriteAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_plugins.txt", fileList5);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Utilities.UpdatePluginsList", ex.Message, ex.StackTrace);
            }
        }

        // Обновление версий iiko

        internal static void UpdateVersioniiko()
        {
            //Получение списка папок с ftp
            string path = Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.iiko.ru/release_iiko/");
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("partners", "partners#iiko");
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            File.WriteAllText(path, reader.ReadToEnd());

            reader.Close();
            response.Close();
            // Удаление старых версий из списка
            string tempFile = Path.GetTempFileName();
            var fileList3 = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt").ToList(); for (var i = fileList3.Count - 1; i >= 0; i--) { if (fileList3[i].StartsWith("3")) fileList3.RemoveAt(i); }
            File.WriteAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt", fileList3);
            var fileList4 = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt").ToList(); for (var i = fileList4.Count - 1; i >= 0; i--) { if (fileList4[i].StartsWith("4")) fileList4.RemoveAt(i); }
            File.WriteAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt", fileList4);
            var fileList5 = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt").ToList(); for (var i = fileList5.Count - 1; i >= 0; i--) { if (fileList5[i].StartsWith("5")) fileList5.RemoveAt(i); }
            File.WriteAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt", fileList5);
            var fileList1 = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt").ToList(); for (var i = fileList1.Count - 1; i >= 0; i--) { if (fileList1[i].StartsWith("!")) fileList1.RemoveAt(i); }
            File.WriteAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt", fileList1);
            var fileListM = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt").ToList(); for (var i = fileListM.Count - 1; i >= 0; i--) { if (fileListM[i].StartsWith("М")) fileListM.RemoveAt(i); }
            File.WriteAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt", fileListM);
            var fileListC = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt").ToList(); for (var i = fileListC.Count - 1; i >= 0; i--) { if (fileListC[i].StartsWith("С")) fileListC.RemoveAt(i); }
            File.WriteAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt", fileListC);
            var fileListi = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt").ToList(); for (var i = fileListi.Count - 1; i >= 0; i--) { if (fileListi[i].StartsWith("i")) fileListi.RemoveAt(i); }
            File.WriteAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt", fileListi);
            var fileListW = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt").ToList(); for (var i = fileListW.Count - 1; i >= 0; i--) { if (fileListW[i].StartsWith("W")) fileListW.RemoveAt(i); }
            File.WriteAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt", fileListW);
            var fileList_C = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt").ToList(); for (var i = fileList_C.Count - 1; i >= 0; i--) { if (fileList_C[i].StartsWith("C")) fileList_C.RemoveAt(i); }
            File.WriteAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt", fileList_C);
        }

        // КОНЕЦ FTP
        // HTTP скачивалки
        internal static void HTTPDownload(string url, string dir)
        {
            try
            {
                WebClient webClient = new WebClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                webClient.DownloadFile(url, dir);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Utilities.HTTPDownload", ex.Message, ex.StackTrace);
            }
        }
        //КОНЕЦ HTTP
        internal static void PluginsRapidDownload(string URL, string dirname, string name)
        {
            try
            {
                //END
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc = web.Load(URL);
                string iikoPath = @"C:\Program Files\iiko\iikoRMS\Front.Net\Plugins\";
                string plugins = @"C:/iiko_Distr/plugins/";
                string zipPath = plugins + name;
                string extractPath = iikoPath + dirname;
                Directory.CreateDirectory(plugins);
                Directory.CreateDirectory(iikoPath + dirname);
                // extracting all links
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    HtmlAttribute att = link.Attributes["href"];

                    if (att.Value.Contains(".zip"))
                    {
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(URL + att.Value, plugins + name);
                        }
                    }
                }
                {

                    if (Directory.Exists(iikoPath + dirname))
                    {
                        Directory.Delete(iikoPath + dirname, true);
                    }
                    System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
                }
                {
                    Directory.Delete(plugins, true);
                }
                // Конец сбер
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Utilities.HTTPDownload", ex.Message, ex.StackTrace);
            }
        }
        internal static void EXESilentSetup(string dir)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = dir;
                p.StartInfo.Arguments = "/quiet /norestart";
                p.Start();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Utilities.EXESilentSetup", ex.Message, ex.StackTrace);
            }
        }
        //Установщик
        internal static void EXERunSetup(string dir)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = dir;
                p.StartInfo.Arguments = "";
                p.Start();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Utilities.EXESilentSetup", ex.Message, ex.StackTrace);
            }
        }
        // Конец установки
        internal static void ImportRegistryScript(string scriptFile)
        {
            string path = "\"" + scriptFile + "\"";

            Process p = new Process();
            try
            {
                p.StartInfo.FileName = "regedit.exe";
                p.StartInfo.UseShellExecute = false;

                p = Process.Start("regedit.exe", "/s " + path);

                p.WaitForExit();
            }
            catch (Exception ex)
            {
                p.Dispose();
                ErrorLogger.LogError("Utilities.ImportRegistryScript", ex.Message, ex.StackTrace);
            }
            finally
            {
                p.Dispose();
            }
        }

        internal static void DownloadPlugins(string name, string URL, string dirname)
        {


        }

        //Corflags
        internal static void Corflags()
        {
            try
            {
                {
                    string url = "https://github.com/ru2mix/ru2mix/raw/main/vcredist_x64.exe";
                    string dir = @"C:/iiko_Distr/vcredist_x64.exe";
                    Utilities.HTTPDownload(url, dir);
                    Utilities.EXESilentSetup(dir);
                }
                {
                    string url = "https://github.com/ru2mix/ru2mix/raw/main/vcredist_x86.exe";
                    string dir = @"C:/iiko_Distr/vcredist_x86.exe";
                    Utilities.HTTPDownload(url, dir);
                    Utilities.EXESilentSetup(dir);
                    
                }
                {
                    System.Threading.Thread.Sleep(2000);
                    string url = "https://ru.iiko.help/resources/Storage/knowlege-base/CorFlags.exe";
                    string dir = @"C:/iiko_Distr/CorFlags.exe";
                    Utilities.HTTPDownload(url, dir);
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = @"C:/iiko_Distr/CorFlags.exe";
                    p.StartInfo.Arguments = @"C:/iiko_Distr/CorFlags.exe" + "/32bit+ C:/Program Files/iiko/iikoRMS/Front.Net/iikoFront.Net.exe";
                    p.Start();
                    System.Threading.Thread.Sleep(1000);
                }
                {
                    System.Threading.Thread.Sleep(1000);
                    Utilities.RunBatchFile(Required.ScriptsFolder + "corflags.bat");
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("PluginDownloader.Corflags", ex.Message, ex.StackTrace);
            }
        }
        //end corflags
        internal static void Reboot()
        {
            Process.Start("shutdown", "/r /t 0");
        }

        internal static void EnableFirewall()
        {
            RunCommand("netsh advfirewall set currentprofile state on");
        }

        internal static void EnableCommandPrompt()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\Policies\\Microsoft\\Windows\\System"))
            {
                key.SetValue("DisableCMD", 0, RegistryValueKind.DWord);
            }
        }

        internal static void EnableControlPanel()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer"))
            {
                key.SetValue("NoControlPanel", 0, RegistryValueKind.DWord);
            }
        }

        internal static void EnableFolderOptions()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer"))
            {
                key.SetValue("NoFolderOptions", 0, RegistryValueKind.DWord);
            }
        }

        internal static void EnableRunDialog()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer"))
            {
                key.SetValue("NoRun", 0, RegistryValueKind.DWord);
            }
        }

        internal static void EnableContextMenu()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer"))
            {
                key.SetValue("NoViewContextMenu", 0, RegistryValueKind.DWord);
            }
        }

        internal static void EnableTaskManager()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System"))
            {
                key.SetValue("DisableTaskMgr", 0, RegistryValueKind.DWord);
            }
        }

        internal static void EnableRegistryEditor()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System"))
            {
                key.SetValue("DisableRegistryTools", 0, RegistryValueKind.DWord);
            }
        }

        internal static void RunCommand(string command)
        {
            using (Process p = new Process())
            {
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/C " + command;

                try
                {
                    p.Start();
                    p.WaitForExit();
                    p.Close();
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError("Utilities.RunCommand", ex.Message, ex.StackTrace);
                }
            }
        }

        internal static void RunCommandNoHidden(string command)
        {
            using (Process pnh = new Process())
            {
                pnh.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                pnh.StartInfo.FileName = "cmd.exe";
                pnh.StartInfo.Arguments = "/C " + command;

                try
                {
                    pnh.Start();
                    pnh.WaitForExit();
                    pnh.Close();
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError("Utilities.RunCommand", ex.Message, ex.StackTrace);
                }
            }
        }

        internal static void FindFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                Process.Start("explorer.exe", "/select, " + fileName);
            }
        }


        internal static void RestartExplorer()
        {
            const string explorer = "explorer.exe";
            string explorerPath = string.Format("{0}\\{1}", Environment.GetEnvironmentVariable("WINDIR"), explorer);

            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    if (string.Compare(process.MainModule.FileName, explorerPath, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        process.Kill();
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError("Utilities.RestartExplorer", ex.Message, ex.StackTrace);
                }
            }

            Process.Start(explorer);
        }

        internal static void FindKeyInRegistry(string key)
        {
            try
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Applets\Regedit", "LastKey", key);
                Process.Start("regedit");
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Utilities.FindKeyInRegistry", ex.Message, ex.StackTrace);
            }
        }


        internal static void ResetConfiguration()
        {
            try
            {
                Directory.Delete(Required.CoreFolder, true);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Utilities.ResetConfiguration", ex.Message, ex.StackTrace);
            }
            finally
            {
                Application.Restart();
            }
        }

        internal static Task RunAsync(this Process process)
        {
            var tcs = new TaskCompletionSource<object>();
            process.EnableRaisingEvents = true;
            process.Exited += (s, e) => tcs.TrySetResult(null);

            if (!process.Start()) tcs.SetException(new Exception("Failed to start process."));
            return tcs.Task;
        }

        internal static PingReply PingHost(string nameOrAddress)
        {
            PingReply reply;
            try
            {
                reply = pinger.Send(nameOrAddress);
                return reply;
            }
            catch
            {
                return null;
            }
        }

        internal static bool IsInternetAvailable()
        {
            const int timeout = 1000;
            const string host = "1.1.1.1";

            var ping = new Ping();
            var buffer = new byte[32];
            var pingOptions = new PingOptions();

            try
            {
                var reply = ping.Send(host, timeout, buffer, pingOptions);
                return (reply != null && reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static void FlushDNSCache()
        {
            Utilities.RunCommand("ipconfig /release && ipconfig /flushdns && ipconfig /renew");
        }
    }
}
