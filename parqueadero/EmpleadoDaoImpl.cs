using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;


namespace ConsoleAppArquiSoftDao02
{
    public class EmpleadoDaoImpl : IEmpleadoDao
    {
        private const string INSERT_QUERY = "INSERT INTO parqueadero (nombre, apellido, placa, duracion_meses, precio_mensual) VALUES ( @nombre, @apellido, @placa, @duracion_meses, @precio_mensual)";
        private const string SELECT_ALL_QUERY = "SELECT * FROM parqueadero ORDER BY ID";
        private const string UPDATE_QUERY = "UPDATE parqueadero SET nombre=@nombre, apellido=@apellido, placa=@placa, duracion_meses=@duracion_meses, precio_mensual=@precio_mensual WHERE ID=@id";
        private const string DELETE_QUERY = "DELETE FROM parqueadero WHERE ID=@id";
        private const string SELECT_BY_ID_QUERY = "SELECT * FROM parqueadero WHERE id=@id";
        private const string SELECT_ALL_EMPLEADOS_QUERY = "SELECT * FROM parqueadero";

        private readonly MySqlConnection _connection;

        public EmpleadoDaoImpl(MySqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public bool Registrar(Empleado parqueadero)
        {
            bool registrado = false;

            try
            {
                ProveState();
                using (MySqlCommand cmd = new MySqlCommand(INSERT_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@nombre", parqueadero.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", parqueadero.Apellido);
                    cmd.Parameters.AddWithValue("@placa", parqueadero.Placa);
                    cmd.Parameters.AddWithValue("@duracion_meses", parqueadero.DuracionMeses);
                    cmd.Parameters.AddWithValue("@precioMensual", parqueadero.PrecioMensual);
                    cmd.ExecuteNonQuery();

                    parqueadero.Id = (int)cmd.LastInsertedId;

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
                            Empleado parqueadero = CrearEmpleadoDesdeDataReader(reader);
                            listaEmpleados.Add(parqueadero);
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

        public bool Actualizar(Empleado parqueadero)
        {
            bool actualizado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(UPDATE_QUERY, _connection))
                {

                    cmd.Parameters.AddWithValue("@nombre", parqueadero.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", parqueadero.Apellido);
                    cmd.Parameters.AddWithValue("@placa", parqueadero.Placa);
                    cmd.Parameters.AddWithValue("@duracion_meses", parqueadero.DuracionMeses);
                    cmd.Parameters.AddWithValue("@precio_mensual", parqueadero.PrecioMensual);
                    cmd.Parameters.AddWithValue("@id", parqueadero.Id);
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

        public bool Eliminar(Empleado parqueadero)
        {
            bool eliminado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(DELETE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", parqueadero.Id);
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
            Empleado parqueadero = null;

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
                            parqueadero = CrearEmpleadoDesdeDataReader(reader);
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

            return parqueadero;
        }

        public List<Empleado> ObtenerTodosLosEmpleados()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_EMPLEADOS_QUERY, _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empleado parqueadero = CrearEmpleadoDesdeDataReader(reader);
                            listaEmpleados.Add(parqueadero);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener todos los clientes", ex);
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
            string nombre = reader.GetString("nombre");
            string apellido = reader.GetString("apellido");
            string placa = reader.GetString("placa");
            int duracion_meses = reader.GetInt32("duracion_meses");
            double precio_mensual = reader.GetDouble("precio_mensual");
            return new Empleado(id, nombre, apellido, placa, duracion_meses, precio_mensual );
        }

        private void ProveState()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

    }
}