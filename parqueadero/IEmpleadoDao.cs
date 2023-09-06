using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppArquiSoftDao02
{
    public interface IEmpleadoDao
    {
        bool Registrar(Empleado parqueadero);   
        List<Empleado> Obtener();
        bool Actualizar(Empleado parqueadero);
        bool Eliminar(Empleado parqueadero);
        Empleado ObtenerEmpleadoPorId(int id);
        List<Empleado> ObtenerTodosLosEmpleados();

    }
}