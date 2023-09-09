using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppArquiSoftDao02
{
    public class ViewEmpleado
    {
        public void VerEmpleado(Empleado cliente)
        {
            Console.WriteLine("Datos del Cliente:\n" + cliente.ToString());
        }

        public void VerEmpleados(List<Empleado> empleados)
        {
            if (empleados.Count == 0)
            {
                Console.WriteLine("No hay clientes para mostrar.");
                return;
            }

            Console.WriteLine("Lista de clientes:");
            foreach (Empleado cliente in empleados)
            {
                Console.WriteLine("------------");
                Console.WriteLine(cliente.ToString());
            }
        }
    }
}