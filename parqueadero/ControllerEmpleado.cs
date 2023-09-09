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

        public bool RegistrarEmpleado(Empleado cliente)
        {
            try
            {
                return dao.Registrar(cliente);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al registrar el cliente: " + e.Message);
                return false;
            }
        }

        public bool ActualizarEmpleado(Empleado cliente)
        {
            try
            {
                return dao.Actualizar(cliente);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al actualizar el cliente: " + e.Message);
                return false;
            }
        }

        public bool EliminarEmpleado(Empleado cliente)
        {
            try
            {
                return dao.Eliminar(cliente);
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
                List<Empleado> cliente = dao.Obtener();
                vista.VerEmpleados(cliente);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener los clientes: " + e.Message);
            }
        }

        public void VerEstadisticas()
        {
            try
            {
                List<Empleado> cliente = dao.Obtener();
                vista.VerEmpleados(cliente);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener los clientes: " + e.Message);
            }
        }
    }
}