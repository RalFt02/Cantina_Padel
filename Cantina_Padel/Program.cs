using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (_, e) => MostrarExcepcionGlobal(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                    MostrarExcepcionGlobal(ex);
            };

            Application.Run(new Principal());
        }

        private static void MostrarExcepcionGlobal(Exception ex)
        {
            if (ex is MySqlException mysqlEx && mysqlEx.Number == 1062)
            {
                MessageBox.Show(
                    "Ya existe un registro con esos mismos datos. Verificá los campos ingresados.",
                    "Datos duplicados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                "Ocurrió un error al procesar la operación:\n" + ex.Message,
                "Aviso del sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}