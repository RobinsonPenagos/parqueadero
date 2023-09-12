using ConsoleAppArquiSoftDao02;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppArquiSoftDao02
{
    public class ControllerEmpleado
    {
        private ViewEmpleado vista = new ViewEmpleado();
        private IEmpleadoDao dao;

        public ControllerEmpleado(IEmpleadoDao dao)
        {
            this.dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        public bool RegistrarEmpleado(Empleado parqueadero)
        {
            try
            {
                return dao.Registrar(parqueadero);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al registrar el cliente: " + e.Message);
                return false;
            }
        }

        public bool ActualizarEmpleado(Empleado parqueadero)
        {
            try
            {
                return dao.Actualizar(parqueadero);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al actualizar el cliente: " + e.Message);
                return false;
            }
        }

        public bool EliminarEmpleado(Empleado parqueadero)
        {
            try
            {
                return dao.Eliminar(parqueadero);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al eliminar el cliente: " + e.Message);
                return false;
            }
        }

        public void VerEmpleados()
        {
            try
            {
                List<Empleado> parqueadero = dao.Obtener();
                vista.VerEmpleados(parqueadero);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener los detos de los clientes: " + e.Message);
            }
        }
        // calcular los porcentajes
        public void VerEstadisticas()
        {
            try
            {
                double ingresosMensuales = dao.CalcularIngresosMensuales();
                double ingresosTotales = dao.CalcularIngresosTotales();

                Console.WriteLine("Ingreso total por mes: = $" + ingresosMensuales);
                Console.WriteLine("Ingreso del mes actual mas los meses pagos por adelantado");
                Console.WriteLine("NETO: = $" + ingresosTotales);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al calcular las estadísticas: " + e.Message);
            }
        }
    }
}

