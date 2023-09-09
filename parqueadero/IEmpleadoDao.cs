using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppArquiSoftDao02
{
    public interface IEmpleadoDao
    {
        double CalcularIngresosMensuales();
        double CalcularIngresosTotales();
        bool Registrar(Empleado parqueadero);   
        List<Empleado> Obtener();
        List<Empleado> ObtenerTodosLosEmpleados();
        bool Actualizar(Empleado parqueadero);
        bool Eliminar(Empleado parqueadero);
        Empleado ObtenerEmpleadoPorId(int id);

    }
}