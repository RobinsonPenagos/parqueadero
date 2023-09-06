using MySql.Data.MySqlClient;

namespace ConsoleAppArquiSoftDao02
{
    public class EmpleadoDAOFactory
    {
        public static IEmpleadoDao CrearEmpleadoDAO()
        {
            MySqlConnection connection = Conexion.Instance.AbrirConexion();
            return new EmpleadoDaoImpl(connection);
        }
    }
}