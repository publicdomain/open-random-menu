
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OpenRandomMenu
{
    /// <summary>
    /// Class with program entry point.
    /// </summary>
    internal sealed class Program
    {
        /// <summary>
        /// Program entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // Check arguments for context menu start
            if (args.Length > 0)
            {
                // Collect files, including subdirectories, into the file list
                List<string> fileList = new List<string>(Directory.GetFiles(args[0], "*.*", SearchOption.AllDirectories));

                // Check there's something to work with
                if (fileList.Count > 0)
                {
                    // Set random
                    var random = new Random();

                    // Set file
                    var file = fileList[random.Next(fileList.Count)];

                    try
                    {
                        // Start a random file
                        Process.Start(file);
                    }
                    catch (Exception ex)
                    {
                        // Set error message
                        var errorMessage = $"Error when opening random file.{Environment.NewLine}{Environment.NewLine}{file}{Environment.NewLine}{Environment.NewLine}{ex.Message}";

                        // Advise user
                        MessageBox.Show(errorMessage, "Random file open", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // Save to error log
                        File.AppendAllText("OpenRandomMenu-ErrorLog.txt", $"{Environment.NewLine}{Environment.NewLine}=========={Environment.NewLine}{ errorMessage }");
                    }
                }
            }
            else // By user
            {
                // Run main form
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }

    }
}
