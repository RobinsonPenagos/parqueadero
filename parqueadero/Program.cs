using System;
using System.Collections.Generic;

namespace ConsoleAppArquiSoftDao02
{
    class Program
    {
        private static IEmpleadoDao dao = EmpleadoDAOFactory.CrearEmpleadoDAO();

        public static void Main(string[] args)
        {
            string action;

            while (true)
            {
                Console.WriteLine("PARQUEADERO PENAGOSCITY");
                Console.WriteLine("***********************");
                Console.WriteLine("     BIENVENIDOS");
                Console.WriteLine("\n");
                Console.WriteLine("Por favor digite la letra de la opcion deseada:\n");
                Console.WriteLine("[L]istar | [R]egistrar | [A]ctualizar | [E]liminar | [S]alir: ");
                action = Console.ReadLine()?.ToUpper();

                if (!string.IsNullOrEmpty(action))
                {
                    try
                    {
                        switch (action)
                        {
                            case "L":
                                ListarEmpleados();
                                break;
                            case "R":
                                RegistrarEmpleado();
                                break;
                            case "A":
                                ActualizarEmpleado();
                                break;
                            case "E":
                                EliminarEmpleado();
                                break;
                            case "S":
                                return;
                            default:
                                Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                                break;
                        }
                    }
                    catch (DAOException e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                }
            }
        }

        private static void RegistrarEmpleado()
        {
            try
            {
                Empleado parqueadero = InputEmpleado();
                if (dao.Registrar(parqueadero))
                {
                    Console.WriteLine("Registro exitoso: " + parqueadero.Id);
                    Console.WriteLine("\n\nCreado: " + parqueadero);
                }
                else
                {
                    Console.WriteLine("Error al registrar el cliente.");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al registrar el cliente: " + e.Message);
            }
        }

        private static void ActualizarEmpleado()
        {
            int id = InputId();
            Empleado parqueadero = dao.ObtenerEmpleadoPorId(id);
            Console.WriteLine("------------Datos originales------------");
            Console.WriteLine(parqueadero);
            Console.WriteLine("Ingrese los nuevos datos");

            string nombre = InputNombre();
            string apellido = InputApellido();
            string placa = InputPlaca();
            int duracion_meses = InputDuracionMeses("Ingrese la duracion de meses: ");
            double precio_mensual = InputPrecioMensual();

            parqueadero = new Empleado(id, nombre, apellido, placa, duracion_meses, precio_mensual);
            try
            {
                if (dao.Actualizar(parqueadero))
                {
                    Console.WriteLine("Actualización exitosa");
                }
                else
                {
                    Console.WriteLine("Error al actualizar el cliente.");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al actualizar el cliente: " + e.Message);
            }
        }

        private static void EliminarEmpleado()
        {
            int id = InputId();
            Empleado parqueadero = null;

            try
            {
                parqueadero = dao.ObtenerEmpleadoPorId(id);
            }
            catch (DAOException daoe)
            {
                Console.WriteLine("Error: " + daoe.Message);
            }

            if (parqueadero != null && dao.Eliminar(parqueadero))
            {
                Console.WriteLine("Cliente eliminado: " + parqueadero.Id);
            }
            else
            {
                Console.WriteLine("Error al eliminar el Cliente: ");
            }
        }

        private static void ListarEmpleados()
        {
            try
            {
                List<Empleado> todosLosEmpleados = dao.ObtenerTodosLosEmpleados();
                foreach (Empleado parqueadero in todosLosEmpleados)
                {
                    Console.WriteLine(parqueadero.ToString() + "\n");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener todos los clientes: " + e.Message);
                Console.WriteLine("StackTrace: " + e.StackTrace);
            }
        }

        private static Empleado InputEmpleado()
        {
            string nombre = InputNombre();
            string apellido = InputApellido();
            string placa = InputPlaca();
            int duracion_meses = InputDuracionMeses("Ingrese la duracion de meses: ");
            double precio_mensual = InputPrecioMensual();
            return new Empleado(nombre, apellido, placa, duracion_meses, precio_mensual );
        }

        private static int InputId()
        {
            int id;
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese un valor entero para el ID del cliente: ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de número");
                }
            }
            return id;
        }

        private static string InputNombre()
        {
            return InputString("Ingrese el nombre del cliente: ");
        }

        private static string InputApellido()
        {
            return InputString("Ingrese el apellido del cliente: ");
        }

        private static string InputPlaca()
        {
            return InputString("Ingrese la placa del vehiculo: ");
        }

        private static int InputDuracionMeses(string message)
        {
            int duracion_meses;
            /*bool inputValido = false;*/

            /*while (!inputValido)*/
            while (true)
            {
                /*Console.WriteLine("Ingrese el número de meses adquiridos: ");*/
                Console.WriteLine(message);
                if (int.TryParse(Console.ReadLine(), out duracion_meses) /*|| duracion_meses > 0*/)
                {
                    /*inputValido = true;*/
                    break;
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un valor numérico válido y positivo.");
                }
            }

            return duracion_meses;
        }

        private static double InputPrecioMensual()
        {
            double precio_mensual;
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese el valor de la mensualidad: ");
                    if (double.TryParse(Console.ReadLine(), out precio_mensual))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de número");
                }
            }
            return precio_mensual;
        }

        
        private static string InputString(string message)
        {
            string s;
            while (true)
            {
                Console.WriteLine(message);
                s = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(s) && s.Length >= 2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("La longitud de la cadena debe ser >= 2");
                }
            }
            return s;
        }
    }
}
