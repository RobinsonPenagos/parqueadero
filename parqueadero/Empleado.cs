using System.Numerics;

namespace ConsoleAppArquiSoftDao02
{
    public class Empleado
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int cedula { get; set; }
        public int duracion_meses { get; set; }
        public DateTime fecha { get; set; }
        public double precio_mensual { get; set; }

        public Empleado() { }

        public Empleado(int id, string nombre, string apellido, int cedula, int duracion_meses, DateTime fecha, double precio_mensual)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.cedula = cedula;
            this.duracion_meses = duracion_meses;
            this.fecha = fecha;
            this.precio_mensual = precio_mensual;
        }

        public Empleado(string nombre, string apellido, int cedula, int duracion_meses, DateTime fecha, double precio_mensual)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.cedula = cedula;
            this.duracion_meses = duracion_meses;
            this.fecha = fecha;
            this.precio_mensual = precio_mensual;
        }

        public override string ToString()
        {
            return $"ID: {id} Nombre: {nombre} Apellido: {apellido} Cedula {cedula} DuracionMeses: {duracion_meses} Fecha: {fecha} PrecioMensual: {precio_mensual}";
        }
    }
}
