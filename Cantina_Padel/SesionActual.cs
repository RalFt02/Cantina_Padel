namespace Cantina_Padel
{
    /// <summary>
    /// Guarda quién está logueado. Se completa en el login y se limpia al volver
    /// a él, así cualquier form puede saber el usuario actual sin recibirlo por constructor.
    /// </summary>
    public static class SesionActual
    {
        public static int IdUsuario { get; private set; }
        public static string Rol { get; private set; } = "";

        public static bool HayUsuario => IdUsuario > 0;

        public static void Iniciar(int idUsuario, string rol)
        {
            IdUsuario = idUsuario;
            Rol = rol;
        }

        public static void Limpiar()
        {
            IdUsuario = 0;
            Rol = "";
        }
    }
}
