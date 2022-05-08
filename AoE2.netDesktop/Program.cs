namespace AoE2NetDesktop;

using System;
using System.Windows.Forms;
using AoE2NetDesktop.Form;
using AoE2NetDesktop.LibAoE2Net.Parameters;

/// <summary>
/// Main Program.
/// </summary>
internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FormMain(Language.en));
    }
}
