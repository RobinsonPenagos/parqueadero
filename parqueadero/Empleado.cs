using System.Numerics;

namespace ConsoleAppArquiSoftDao02
{
    public class Empleado
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int cedula { get; set; }
        public string placa { get; set; }
        public int duracion_meses { get; set; }
        public DateTime fecha { get; set; }
        public double precio_mensual { get; set; }

        public Empleado()
        {
        }

        public Empleado(int id, string nombre, string apellido, int cedula, string placa, int duracion_meses, DateTime fecha, double precio_mensual)
        {
            id = id;
            nombre = nombre;
            apellido = apellido;
            placa = placa;
            cedula = cedula;
            duracion_meses = duracion_meses;
            fecha = fecha;
            precio_mensual = precio_mensual;
        }

        public Empleado(string nombre, string apellido, int cedula, string placa, int duracion_meses, DateTime fecha, double precio_mensual)
        {
            nombre = nombre;
            apellido = apellido;
            placa = placa;
            cedula = cedula;
            duracion_meses = duracion_meses;
            fecha = fecha;
            precio_mensual = precio_mensual;
        }

        public override string ToString()
        {
            return $"ID: {id}\nNombre: {nombre}\nApellido: {apellido}\nPlaca: {placa}\nCedula {cedula}\nDuracionMeses: {duracion_meses}\nFecha: {fecha}\nPrecioMensual: {precio_mensual}";
        }
    }
}
