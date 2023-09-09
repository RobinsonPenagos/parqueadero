using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppArquiSoftDao02
{
    public interface IEmpleadoDao
    {
        double CalcularIngresosMensuales();
        double CalcularIngresosTotales();
        bool Registrar(Empleado cliente);   
        List<Empleado> Obtener();
        List<Empleado> ObtenerTodosLosEmpleados();
        bool Actualizar(Empleado cliente);
        bool Eliminar(Empleado cliente);
        Empleado ObtenerEmpleadoPorId(int id);

    }
}