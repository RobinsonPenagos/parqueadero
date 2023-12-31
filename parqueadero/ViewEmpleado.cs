﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppArquiSoftDao02
{
    public class ViewEmpleado
    {
        public void VerEmpleado(Empleado parqueadero)
        {
            Console.WriteLine("Datos del parqueadero:\n" + parqueadero.ToString());
        }

        public void VerEmpleados(List<Empleado> empleados)
        {
            if (empleados.Count == 0)
            {
                Console.WriteLine("No hay parqueaderos para mostrar.");
                return;
            }

            Console.WriteLine("Lista de parqueaderos:");
            foreach (Empleado parqueadero in empleados)
            {
                Console.WriteLine("------------");
                Console.WriteLine(parqueadero.ToString());
            }
        }
    }
}