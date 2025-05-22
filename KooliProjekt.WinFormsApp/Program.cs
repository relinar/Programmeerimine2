using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var form = new Form1();
            var apiClient = new ApiClient();
            var presenter = new AmountPresenter(form, apiClient);
            Application.Run(form);
        }
    }
}