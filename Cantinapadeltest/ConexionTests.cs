using Cantina_Padel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;

namespace Cantina_Padel.Tests
{
    [TestClass]
    public class ConexionTests
    {
        [TestMethod]
        public void ObtenerConexion_DevuelveInstanciaValida()
        {
            // Act
            using MySqlConnection conexion = Conexion.ObtenerConexion();

            // Assert
            Assert.IsNotNull(conexion);
        }

        [TestMethod]
        public void ObtenerConexion_EstadoInicialEsClosed()
        {
            // Act
            using MySqlConnection conexion = Conexion.ObtenerConexion();

            // Assert
            Assert.AreEqual(System.Data.ConnectionState.Closed, conexion.State);
        }

        [TestMethod]
        public void ObtenerConexion_CadenaDeConexionTieneLosDatosCorrectos()
        {
            // Arrange
            using MySqlConnection conexion = Conexion.ObtenerConexion();
            var builder = new MySqlConnectionStringBuilder(conexion.ConnectionString);

            // Assert
            Assert.AreEqual("localhost", builder.Server);
            Assert.AreEqual("cantina_padel", builder.Database);
            Assert.AreEqual("root", builder.UserID);
        }

        [TestMethod]
        public void ObtenerConexion_CadaLlamadaDevuelveUnaInstanciaNueva()
        {
            // Act
            using MySqlConnection conexion1 = Conexion.ObtenerConexion();
            using MySqlConnection conexion2 = Conexion.ObtenerConexion();

            // Assert
            Assert.AreNotSame(conexion1, conexion2);
            Assert.AreEqual(conexion1.ConnectionString, conexion2.ConnectionString);
        }
    }
}
