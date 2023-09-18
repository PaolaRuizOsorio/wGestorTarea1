using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace wGestorDeTarea
{
    internal class clsConexion
    {
        /// <summary>
        /// En este metodo realizamos la cadena de conexon con la base de datos
        /// </summary>
        /// <returns>conexion</returns>
        public static SqlConnection conectar()
        {
            SqlConnection conexion = new SqlConnection("server=LAPTOP-J6U1P8U7;database=dbGestorDeTarea;integrated security=true");
            conexion.Open();
            return conexion;
        }
    }
}
