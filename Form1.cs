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
    /// <summary>
    /// Fecha: 18 - 09 - 2023
    /// Descripcion:Aplicacion para gestionar las tareas pendientes.
    /// Nombres: Jhonatan Mosquera - Paola Ruiz
    /// </summary>
    public partial class frmIngresarTarea : Form
    {
        public frmIngresarTarea()
        {
            InitializeComponent();
        }
        
        //BOTÓN REGISTRAR
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                clsConexion.conectar();

                DateTime datFechaVencimiento = dtFechaVencimiento.Value.Date;

                clsRegistrar ingresarTarea = new clsRegistrar( txtTitulo.Text, txtDescripcion.Text,datFechaVencimiento, cboPrioridad.Text);


                ingresarTarea.ObtenerEstado(rbPendiente.Checked, rbTerminado.Checked);
 
                string resultado = ingresarTarea.ObtenerEstado(rbPendiente.Checked, rbTerminado.Checked);
                ingresarTarea.resultado = ingresarTarea.ObtenerEstado(rbPendiente.Checked, rbTerminado.Checked);
                ingresarTarea.registrar();

                MessageBox.Show("Se ha realizado el registro correctamente", "Alerta",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception)
            {

                MessageBox.Show("Error al ingresar la tarea", "Alerta",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        //BOTÓN LIMPIAR
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtTitulo.Clear();
            txtDescripcion.Clear();
            cboPrioridad.SelectedIndex = -1;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                clsConexion.conectar();

                clsRegistrar tarea = new clsRegistrar();
                dtgGestorTarea.DataSource = tarea.consultar();
            }
            catch (Exception)
            {

                MessageBox.Show("Error al consultar el dato", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        //BOTÓN ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                clsConexion.conectar();

                clsRegistrar tarea = new clsRegistrar();
                tarea.eliminar(Convert.ToInt32(txtCodigo.Text));
                dtgGestorTarea.DataSource = tarea.consultar();
            }
            catch (Exception)
            {

                MessageBox.Show("Error al eliminar", "Alerta",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //BOTÓN MODIFICAR
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                clsConexion.conectar();


                DateTime datFechaVencimiento = dtFechaVencimiento.Value.Date;

                clsRegistrar tarea = new clsRegistrar();
                tarea.intCodigo = Convert.ToInt16(txtCodigo.Text);
                tarea.strTitulo = txtTitulo.Text; 
                tarea.strDescripcion = txtDescripcion.Text;
                tarea.datFechaVencimiento = datFechaVencimiento;
                tarea.strPrioridad = cboPrioridad.Text;

                tarea.ObtenerEstado(rbPendiente.Checked, rbTerminado.Checked);
                string resultado = tarea.ObtenerEstado(rbPendiente.Checked, rbTerminado.Checked);
                tarea.resultado = resultado;

                tarea.modificar();

                dtgGestorTarea.DataSource = tarea.consultar();

                MessageBox.Show("Datos modificados", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el dato: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        //EVENTO PARA SELECCIONAR EN EL DATADRIEGVIEW, (Cualquier lugar)
        private void dtgGestorTarea_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dtgGestorTarea.SelectedRows.Count > 0) // Verifica si hay al menos una fila seleccionada
                {
                    int indice = dtgGestorTarea.SelectedRows[0].Index; // Obtenemos el índice de la fila seleccionada
                    if (indice >= 0 && indice < dtgGestorTarea.Rows.Count) // Verifica si el índice está dentro del rango válido
                    {
                        DataGridViewRow fila = dtgGestorTarea.Rows[indice];

                        if (fila.Cells[0].Value != null)
                            txtCodigo.Text = fila.Cells[0].Value.ToString();
                        else
                            txtCodigo.Text = string.Empty;

                        if (fila.Cells[1].Value != null)
                            txtTitulo.Text = fila.Cells[1].Value.ToString();
                        else
                            txtTitulo.Text = string.Empty;

                        if (fila.Cells[2].Value != null)
                            txtDescripcion.Text = fila.Cells[2].Value.ToString();
                        else
                            txtDescripcion.Text = string.Empty;

                        if (fila.Cells[3].Value != null)
                            dtFechaVencimiento.Text = fila.Cells[3].Value.ToString();
                        else
                            dtFechaVencimiento.Text = string.Empty;

                        if (fila.Cells[4].Value != null)
                            cboPrioridad.Text = fila.Cells[4].Value.ToString();
                        else
                            cboPrioridad.Text = string.Empty;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //EVENTOS PARA SELECCIONAR EN EL DATADRIEGVIEW, (Celda)
        private void dtgGestorTarea_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int indice = e.RowIndex;
                if (indice >= 0 && indice < dtgGestorTarea.Rows.Count)
                {
                    DataGridViewRow fila = dtgGestorTarea.Rows[indice];

                    if (fila.Cells[0].Value != null)
                        txtCodigo.Text = fila.Cells[0].Value.ToString();
                    else
                        txtCodigo.Text = string.Empty;

                    if (fila.Cells[1].Value != null)
                        txtTitulo.Text = fila.Cells[1].Value.ToString();
                    else
                        txtTitulo.Text = string.Empty;

                    if (fila.Cells[2].Value != null)
                        txtDescripcion.Text = fila.Cells[2].Value.ToString();
                    else
                        txtDescripcion.Text = string.Empty;

                    if (fila.Cells[3].Value != null)
                        dtFechaVencimiento.Text = fila.Cells[3].Value.ToString();
                    else
                        dtFechaVencimiento.Text = string.Empty;

                    if (fila.Cells[4].Value != null)
                        cboPrioridad.Text = fila.Cells[4].Value.ToString();
                    else
                        cboPrioridad.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Estás seleccionando fuera de la información", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //Filtrar Información 
        private void txtTitulo_KeyUp(object sender, KeyEventArgs e)
        {
            clsConexion.conectar();

            clsRegistrar consultas = new clsRegistrar();
            string consulta = "SELECT * FROM tblIngresoTarea WHERE strTitulo LIKE @filtroTitulo";

            SqlCommand cmd = new SqlCommand(consulta, clsConexion.conectar());
            cmd.Parameters.AddWithValue("@filtroTitulo", "%" + txtTitulo.Text + "%");

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dtgGestorTarea.DataSource = dt;

        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            clsConexion.conectar();

            clsRegistrar consultas1 = new clsRegistrar();
            string consulta1 = "SELECT * FROM tblIngresoTarea WHERE strDescripcion LIKE @filtroDescripcion";

            SqlCommand cmd = new SqlCommand(consulta1, clsConexion.conectar());
            cmd.Parameters.AddWithValue("@filtroDescripcion", "%" + txtDescripcion.Text + "%"); 

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dtgGestorTarea.DataSource = dt;
        }

        private void cboPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPrioridad.SelectedIndex != -1)
            {
                string prioridadSeleccionada = cboPrioridad.SelectedItem.ToString();

                clsConexion.conectar();

                clsRegistrar consultas = new clsRegistrar();
                string consulta = "SELECT * FROM tblIngresoTarea WHERE strPrioridad LIKE @filtroPrioridad";

                SqlCommand cmd = new SqlCommand(consulta, clsConexion.conectar());
                cmd.Parameters.AddWithValue("@filtroPrioridad", "%" + prioridadSeleccionada + "%");

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dtgGestorTarea.DataSource = dt;
            }
        }

        private void dtFechaVencimiento_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaSeleccionada = dtFechaVencimiento.Value.Date;

            clsConexion.conectar();

            clsRegistrar consultas = new clsRegistrar();
            string consulta = "SELECT * FROM tblIngresoTarea WHERE datFechaVencimiento = @filtroFecha";

            SqlCommand cmd = new SqlCommand(consulta, clsConexion.conectar());
            cmd.Parameters.AddWithValue("@filtroFecha", fechaSeleccionada);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dtgGestorTarea.DataSource = dt;
        }

        

        /// <summary>
        ///     Se muestra la notificación en la parte inferior de la pantalla 
        ///     Si eres nuevo o no tienes tareas saldra una informacion diferente a si tienes tareas pendientes 
        ///     en este caso te aparecerá la fecha y el titulo de la tarea correspondiente
        /// </summary>
        public void mostrarNotificacion()
        {
            clsConexion.conectar();
            clsRegistrar consultaFecha = new clsRegistrar();
            string consultaSql = "select top 1 datFechaVencimiento, strTitulo from tblIngresoTarea order by datFechaVencimiento asc";
            SqlCommand cmd = new SqlCommand(consultaSql, clsConexion.conectar());

            // Ejecutar la consulta SQL y obtener el resultado
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                DateTime fechaVencimiento = (DateTime)reader["datFechaVencimiento"];
                string titulo = reader["strTitulo"].ToString();

                notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath(@"../../imagenes/intimidad.ico"));
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipTitle = " Gestor de Tareas ";  
                notifyIcon1.BalloonTipText = " Recuerda que tu próxima actividad es el : " + fechaVencimiento.ToString("yyyy-MM-dd") + ", la tarea es de  " + titulo; 
                notifyIcon1.ShowBalloonTip(200);
            }
            else
            {
                // Si la consulta no encontró ningún resultado, muestra un mensaje de bienvenida
                notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath(@"../../imagenes/intimidad.ico")); 
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipTitle = " Bienvenido "; 
                notifyIcon1.BalloonTipText = " No tienes tareas pendientes en este momento. ¡Bienvenido!"; 
                notifyIcon1.ShowBalloonTip(200);
            }

            reader.Close();
        }

        //Iniciar con la notificación 
        private void frmIngresarTarea_Load(object sender, EventArgs e)
        {
            mostrarNotificacion();
        }

        
    }
}




