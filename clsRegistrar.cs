using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace wGestorDeTarea
{
    
    internal class clsRegistrar
    {
        public int intCodigo;
        public string strTitulo;
        public string strDescripcion;
        public DateTime datFechaVencimiento;
        public string strPrioridad;
        public string resultado;
        
        public clsRegistrar()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTitulo"> Se almacena el título que el usuario le asigne a la tarea </param>
        /// <param name="strDescripcion">Se almacena la descripcíón que el usuario le asigne a la tarea</param>
        /// <param name="datFechaVencimiento">Se almacena la fecha de vencimiento que el usuario le asigne a la tarea</param>
        /// <param name="strPrioridad">Se almacena la prioridad (Alta,Media,Baja) que el usuario le asigne a la tarea</param>
        public clsRegistrar(string strTitulo, string strDescripcion, DateTime datFechaVencimiento, string strPrioridad)
        {

            this.strTitulo = strTitulo;
            this.strDescripcion = strDescripcion;
            this.datFechaVencimiento = datFechaVencimiento;
            this.strPrioridad = strPrioridad;

        }

        /// <summary>
        /// Se obtiene la opcion que haya tomado el usuario para establecer el Estado de la tarea. 
        /// </summary>
        /// <param name="pendiente">Se almacena el estado (Pendiente) que el usuario le asigne a la tarea</param>
        /// <param name="terminado">Se almacena el estado (Terminado) que el usuario le asigne a la tarea</param>
        /// <returns></returns>
        public string ObtenerEstado(bool pendiente, bool terminado)
        {
            
                if (pendiente)
                {
                    return "Pendiente";

                }
                else if (terminado)
                {
                    return "Terminado";

                }
                else
                {
                    return string.Empty; // Ninguno seleccionado
                }
        }

        /// <summary>
        /// Se registra todos los datos llenandos en el formulario y se guardan en la base de datos.
        /// </summary>
        /// <returns> True : cuando hay datos en el formulario</returns>
        public bool registrar()
        {
            clsConexion.conectar();

            string registrar = "insert  into tblIngresoTarea values( @strTitulo,@strDescripcion," +
                "@datFechaVencimiento, @strPrioridad, @strEstado)";

            SqlCommand sql = new SqlCommand(registrar, clsConexion.conectar());

            sql.Parameters.AddWithValue("@strTitulo", this.strTitulo);
            sql.Parameters.AddWithValue("@strDescripcion", this.strDescripcion);
            sql.Parameters.AddWithValue("@datFechaVencimiento", this.datFechaVencimiento);
            sql.Parameters.AddWithValue("@strPrioridad", this.strPrioridad);
            sql.Parameters.AddWithValue("@strEstado", this.resultado);


            sql.ExecuteNonQuery();

            return true;
        }

        /// <summary>
        /// Se consultan las tareas que anteriormente haya registrado el usuario y se muestra en el DataGridView.
        /// </summary>
        /// <returns>Los datos extraidos desde la base de datos</returns>
        public DataTable consultar()
        {
            clsConexion.conectar();

            DataTable dataTable = new DataTable();
            string consulta = "select * from tblIngresoTarea";
            SqlCommand sqlCommand = new SqlCommand(consulta, clsConexion.conectar());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        /// <summary>
        /// Se elimina un registro "tarea" de la base de datos. 
        /// </summary>
        /// <param name="intCodigo">Elimina toda la tupla en la base de datos</param>
        /// <returns>Retorna la informacion menos la tupla anteriormente eliminada </returns>
        public bool eliminar(int intCodigo)
        {
            clsConexion.conectar();

            this.intCodigo = intCodigo;
            string eliminar = "Delete tblIngresoTarea where intCodigo=@intCodigo";
            SqlCommand sql = new SqlCommand(eliminar, clsConexion.conectar());
            sql.Parameters.AddWithValue("@intCodigo", this.intCodigo);
            sql.ExecuteNonQuery();
            return true;
        }

        /// <summary>
        /// Se modifica un registro " Tarea" , para tener un nuevo registro con la información actual. 
        /// </summary>
        public void modificar()
        {
            string strEstado = resultado;
            clsConexion.conectar();

            string modificar = "UPDATE tblIngresoTarea SET strTitulo=@strTitulo, strDescripcion=@strDescripcion,datFechaVencimiento=@datFechaVencimiento," +
                " strPrioridad=@strPrioridad , strEstado=@strEstado WHERE intCodigo=@intCodigo";

            // Crea un nuevo comando SQL y configura los parámetros
            SqlCommand sql = new SqlCommand(modificar, clsConexion.conectar());
            sql.Parameters.AddWithValue("@intCodigo", this.intCodigo);
            sql.Parameters.AddWithValue("@strTitulo", this.strTitulo);
            sql.Parameters.AddWithValue("@strDescripcion", this.strDescripcion);
            sql.Parameters.AddWithValue("@datFechaVencimiento", this.datFechaVencimiento);
            sql.Parameters.AddWithValue("@strPrioridad", this.strPrioridad);
            sql.Parameters.AddWithValue("@strEstado", strEstado);


            sql.ExecuteNonQuery();
        }

    }
}
