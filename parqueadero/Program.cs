﻿using ConsoleAppArquiSoftDao02;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Globalization;

//ControllerEmpleado controller = new ControllerEmpleado(dao);

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
                Console.WriteLine("");
                Console.WriteLine("         ////////////////////");
                Console.WriteLine("         CITY PARKING PENAGOS\n");
                Console.WriteLine("\nPor favor digite el numero de la opción deseada:\n");
                Console.WriteLine("[1] Lista clientes\n[2] Ingresar Nuevo\n[3] Actualizar\n[4] Eliminar\n[5] Total ingresos \n[6]Salir:\n ");
                action = Console.ReadLine()?.ToUpper();

                if (!string.IsNullOrEmpty(action))
                {
                    try
                    {
                        switch (action)
                        {
                            case "1":
                                ListarEmpleados();
                                break;
                            case "2":
                                RegistrarEmpleado();
                                break;
                            case "3":
                                ActualizarEmpleado();
                                break;
                            case "4":
                                EliminarEmpleado();
                                break;
                            case "5":
                                ingresosEstadistica ();
                                return;
                            case "6":
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
        // lamada de las estadisticas
        private static void ingresosEstadistica()
        {
            try
            {
                Console.WriteLine("INFORME DE INGRESOS");
                Console.WriteLine("-------------------");
                double ingresosMensuales = dao.CalcularIngresosMensuales();
                double ingresosTotales = dao.CalcularIngresosTotales();

                Console.WriteLine("Ingreso total del mes: = $" + ingresosMensuales);
                Console.WriteLine("Ingreso del mes actual, mas los meses pagos por adelantado");
                Console.WriteLine("                 " +
                    "NETO: = $" + ingresosTotales);
                Console.WriteLine("------------------");
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al calcular las estadísticas: " + e.Message);
            }
        }

        private static void ListarEmpleados()
        {
            try
            {
                Console.WriteLine("Obteniendo todos los clientes activos...");
                List<Empleado> todosLosEmpleados = dao.ObtenerTodosLosEmpleados();
                Console.WriteLine("");
                Console.WriteLine("\nLista de clientes inscritos en el sistema:\n\n");
                foreach (Empleado parqueadero in todosLosEmpleados)
                {
                    Console.WriteLine(parqueadero.ToString() + "\n");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener todos los clientes activos: " + e.Message);
                Console.WriteLine("StackTrace: " + e.StackTrace);
            }
        }
        private static void RegistrarEmpleado()
        {
            try
            {
                Empleado parqueadero = InputEmpleado();
                if (dao.Registrar(parqueadero))
                {
                    Console.WriteLine("Registro exitoso: " + parqueadero.id);
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
            int cedula = InputCedula();
            int duracion_meses = InputDuracionMeses();
            DateTime fecha = InputFecha();
            double precio_mensual = InputPrecioMensual();

            parqueadero = new Empleado(id, nombre, apellido, cedula, duracion_meses, fecha, precio_mensual);
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
                Console.WriteLine("Cliente eliminado: " + parqueadero.id);
            }
            else
            {
                Console.WriteLine("Error al eliminar el cliente.");
            }
        }

        //Inputs
        private static Empleado InputEmpleado()
        {
            string nombre = InputNombre();
            string apellido = InputApellido();
            int cedula = InputCedula();
            int duracion_meses = InputDuracionMeses();
            DateTime fecha = InputFecha();
            double precio_mensual = InputPrecioMensual();
            return new Empleado (nombre, apellido, cedula, duracion_meses, fecha, precio_mensual);
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
        private static int InputCedula()
        {
            int cedula;
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese el número de cédula del cliente: ");
                    if (int.TryParse(Console.ReadLine(), out cedula))
                    {
                        return cedula;
                    }
                    else
                    {
                        Console.WriteLine("Error: Ingrese un número válido de cédula.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Formato de número incorrecto.");
                }
            }
        }
       /* private static string InputPlaca()
        {
            return InputString("Ingrese el número de la placa: ");
        }*/
        private static int InputDuracionMeses()
        {
            int duracion_meses;
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese el número de meses a pagar: ");
                    if (int.TryParse(Console.ReadLine(), out duracion_meses))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número: ");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error del formato de número: ");
                }
            }
            return duracion_meses;
        }
        private static DateTime InputFecha()
        {
            DateTime fecha;
            bool entradaValida = false;

            do
            {
                Console.WriteLine("Ingrese la fecha de inicio de la inscripcion: (Formato: dd/mm/yyyy): ");
                string input = Console.ReadLine();

                if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {
                    entradaValida = true;
                }
                else
                {
                    Console.WriteLine("Entrada no válida. Asegúrese de ingresar la fecha en el formato correcto (dd/mm/yyyy).");
                }
            } while (!entradaValida);

            return fecha;
        }
        private static double InputPrecioMensual()
        {
            double precio;
            bool entradaValida = false;

            do
            {
                Console.WriteLine("Ingrese el precio mensual: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out precio))
                {
                    entradaValida = true;
                }
                else
                {
                    Console.WriteLine("Entrada no válida. Debe ingresar un valor numérico.");
                }
            } while (!entradaValida);

            return precio;
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
