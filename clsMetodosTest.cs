using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace wGestorDeTarea
{
    public class clsMetodosTest
    {
        public int intCodigo;
        public string strTitulo;
        public string strDescripcion;
        public DateTime datFechaVencimiento;
        public string strPrioridad;
        public string estado;

        public clsMetodosTest(int intCodigo, string strTitulo, string strDescripcion, DateTime datFechaVencimiento, string strPrioridad, string estado)
        {
            this.intCodigo = intCodigo;
            this.strTitulo = strTitulo;
            this.strDescripcion = strDescripcion;
            this.datFechaVencimiento = datFechaVencimiento;
            this.strPrioridad = strPrioridad;
            this.estado = estado;
        }
        public clsMetodosTest()
        {

        }

        public static bool ConectarTest()
        {
            //Metodo de conexion
            SqlConnection conexion = new SqlConnection("server=LAPTOP-J6U1P8U7;database=dbGestorDeTarea;integrated security=true");
            try
            {
                conexion.Open();
                return true; // La conexión se abrió con éxito.
            }
            catch (Exception ex)
            {
                // Si se produce una excepción, la conexión ha fallado.
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }


        public (string Titulo, string Descripcion, DateTime FechaVencimiento, string Prioridad, string Estado) ObtenerDatosTarea()
        {
            //Metodo de insertado de datos
            return ("Tarea de prueba", "Descripción de la tarea de prueba", DateTime.Now, "Alta", "Pendiente");
        }

        public (string Titulo, string Descripcion, DateTime FechaVencimiento, string Prioridad, string Estado) ConsultarDatos()
        {
            //Metodo asune que esta consultado los datos basandose en el metodo anterior
            return ("Tarea de prueba", "Descripción de la tarea de prueba", DateTime.Now, "Alta", "Pendiente");
        }

        public (string Titulo, string Descripcion, DateTime? FechaVencimiento, string Prioridad, string Estado) EliminarDatos()
        {
            //Metodo asune que esta consultado los datos basandose en el metodo anterior
            return ("", "", null, "", "");
        }

        public (string Titulo, string Descripcion, DateTime FechaVencimiento, string Prioridad, string Estado) Actualizar()
        {
            //Metodo asume que modifico datos 
            return ("Tarea de poo", "Conexion", DateTime.Now, "baja", "terminada");
        }







    }
}