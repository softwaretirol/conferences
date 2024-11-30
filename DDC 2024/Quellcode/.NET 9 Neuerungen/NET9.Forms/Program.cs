namespace NET9.Forms;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
#pragma warning disable WFO5001
        Application.SetColorMode(SystemColorMode.Dark);
        Application.Run(new Form1());
    }
}