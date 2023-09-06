using System.Numerics;

namespace ConsoleAppArquiSoftDao02
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Placa { get; set; }
        public int DuracionMeses { get; set; }
        public double PrecioMensual { get; set; }

        public Empleado()
        {
        }

        public Empleado(int id, string nombre, string apellido, string placa, int duracion_meses, double precio_mensual)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Placa = placa;
            DuracionMeses = duracion_meses;
            PrecioMensual = precio_mensual;
        }

        public Empleado(string nombre, string apellido, string placa, int duracion_meses, double precio_mensual )
        {
            Nombre = nombre;
            Apellido = apellido;
            Placa = placa;
            DuracionMeses = duracion_meses;
            PrecioMensual = precio_mensual;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nNombre: {Nombre}\nApellido: {Apellido}\nPlaca: {Placa}\nDuracionMeses: {DuracionMeses}\nPrecioMensual: {PrecioMensual}";
        }
    }
}
