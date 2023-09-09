using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;


namespace ConsoleAppArquiSoftDao02
{
    public class EmpleadoDaoImpl : IEmpleadoDao
    {
        private const string INSERT_QUERY = "INSERT INTO cliente ( nombre, apellido, cedula, duracion_meses, fecha, precio_mensual) VALUES ( @nombre, @apellido, @cedula,  @duracion_meses, @fecha, @precio_mensual);";
        private const string SELECT_ALL_QUERY = "SELECT id, nombre, apellido, cedula, duracion_meses, fecha, precio_mensual FROM cliente ORDER BY id;";
        private const string UPDATE_QUERY = "UPDATE cliente SET nombre=@nombre, apellido=@apellido, cedula=@cedula, duracion_meses=@duracion_meses, fecha=@fecha, precio_mensual=@precio_mensual WHERE id=@id;";
        private const string DELETE_QUERY = "DELETE FROM cliente WHERE id=@id;";
        private const string SELECT_BY_ID_QUERY = "SELECT * FROM cliente WHERE id=@id;";
        private const string SELECT_ALL_EMPLEADOS_QUERY = "SELECT * FROM cliente;";
        private readonly MySqlConnection _connection;
        public EmpleadoDaoImpl(MySqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public double CalcularIngresosMensuales()
        {
            try
            {
                ProveState();
                double totalIngresos = 0.0;

                using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_QUERY, _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            double precioMensual = reader.GetDouble("precio_mensual");
                            totalIngresos += precioMensual;
                        }
                    }
                }
                return totalIngresos;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al calcular los ingresos totales: " + ex.Message);
                return 0.0;
            }
            finally
            {
                _connection.Close();
            }
        }

        public double CalcularIngresosTotales()
        {
            try
            {
                ProveState();
                double totalIngresos = 0.0;

                using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_QUERY, _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            double precioMensual = reader.GetDouble("precio_mensual");
                            int duracionMeses = reader.GetInt32("duracion_meses");

                            totalIngresos += precioMensual * duracionMeses;
                        }
                    }
                }

                return totalIngresos;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al calcular los ingresos totales: " + ex.Message);
                return 0.0;
            }
            finally
            {
                _connection.Close();
            }
        }

        public bool Registrar(Empleado cliente)
        {
            bool registrado = false;
            try
            {
                ProveState();
                using (MySqlCommand cmd = new MySqlCommand(INSERT_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", cliente.id);
                    cmd.Parameters.AddWithValue("@nombre", cliente.nombre);
                    cmd.Parameters.AddWithValue("@apellido", cliente.apellido);
                    cmd.Parameters.AddWithValue("@cedula", cliente.cedula);
                    cmd.Parameters.AddWithValue("@duracion_meses", cliente.duracion_meses);
                    cmd.Parameters.AddWithValue("@fecha", cliente.fecha);
                    cmd.Parameters.AddWithValue("@precio_mensual", cliente.precio_mensual);
                    cmd.ExecuteNonQuery();

                    cliente.id = (int)cmd.LastInsertedId;

                    registrado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al registrar el cliente", ex);
            }
            finally
            {
                _connection.Close();
            }
            return registrado;
        }

        public List<Empleado> Obtener()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_QUERY, _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empleado cliente = CrearEmpleadoDesdeDataReader(reader);
                            listaEmpleados.Add(cliente);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener los clientes", ex);
            }
            finally
            {
                _connection.Close();
            }

            return listaEmpleados;
        }

        public bool Actualizar(Empleado cliente)
        {
            bool actualizado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(UPDATE_QUERY, _connection))
                {

                    cmd.Parameters.AddWithValue("@nombre", cliente.nombre);
                    cmd.Parameters.AddWithValue("@apellido", cliente.apellido);
                    cmd.Parameters.AddWithValue("@cedula", cliente.cedula);
                    cmd.Parameters.AddWithValue("@duracion_meses", cliente.duracion_meses);
                    cmd.Parameters.AddWithValue("@fecha", cliente.fecha);
                    cmd.Parameters.AddWithValue("@precio_mensual", cliente.precio_mensual);
                    cmd.Parameters.AddWithValue("@id", cliente.id);
                    cmd.ExecuteNonQuery();
                    actualizado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al actualizar el cliente", ex);
            }
            finally
            {
                _connection.Close();
            }

            return actualizado;
        }

        public bool Eliminar(Empleado cliente)
        {
            bool eliminado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(DELETE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", cliente.id);
                    cmd.ExecuteNonQuery();
                    eliminado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al eliminar el cliente", ex);
            }
            finally
            {
                _connection.Close();
            }

            return eliminado;
        }

        public Empleado ObtenerEmpleadoPorId(int id)
        {
            Empleado cliente = null;

            try
            {

                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_BY_ID_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = CrearEmpleadoDesdeDataReader(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener el cliente por ID", ex);
            }
            finally
            {
                _connection.Close();
            }

            return cliente;
        }

        public List<Empleado> ObtenerTodosLosEmpleados()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_QUERY, _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empleado cliente = CrearEmpleadoDesdeDataReader(reader);
                            listaEmpleados.Add(cliente);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los clientes: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
            }
            finally
            {
                _connection.Close();
            }

            return listaEmpleados;
        }

        private Empleado CrearEmpleadoDesdeDataReader(MySqlDataReader reader)
        {
            int id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
            string nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString("nombre");
            string apellido = reader.IsDBNull(reader.GetOrdinal("apellido")) ? string.Empty : reader.GetString("apellido");
            int cedula = reader.IsDBNull(reader.GetOrdinal("cedula")) ? 0 : reader.GetInt32("cedula");
            int duracion_meses = reader.IsDBNull(reader.GetOrdinal("duracion_meses")) ? 0 : reader.GetInt32("duracion_meses");
            DateTime fecha = reader.IsDBNull(reader.GetOrdinal("fecha")) ? DateTime.MinValue : reader.GetDateTime("fecha");
            double precio_mensual = reader.IsDBNull(reader.GetOrdinal("precio_mensual")) ? 0.0 : reader.GetDouble("precio_mensual");

            return new Empleado(id, nombre, apellido, cedula, duracion_meses, fecha, precio_mensual);
        }

        private void ProveState()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public List<Empleado> Estadistica()
        {
            throw new NotImplementedException();
        }
    }
}