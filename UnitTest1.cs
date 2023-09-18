using Microsoft.VisualStudio.TestPlatform.TestHost;
using wGestorDeTarea;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System;


namespace AutoTests
{

    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            //esta prueba es para mirar si la conexion a la base de datos es correcta
            bool ConectarTest = Convert.ToBoolean(clsMetodosTest.ConectarTest());
            Assert.IsTrue(ConectarTest, "La conexión a la base de datos debe ser exitosa.");
        }


        [TestMethod]
        public void INSERTAR()
        {
            // Esta prueba es para mirar si lo datos si estan siendo ingresado
            clsMetodosTest tarea = new clsMetodosTest();

            var datos = tarea.ObtenerDatosTarea();
            string strTitulo = datos.Titulo;
            string descripcion = datos.Descripcion;
            DateTime fechaVencimiento = datos.FechaVencimiento;
            string prioridad = datos.Prioridad;
            string estado = datos.Estado;
            // Assert
            Assert.AreEqual("Tarea de prueba", strTitulo);
            Assert.AreEqual("Descripción de la tarea de prueba", descripcion);
            Assert.IsTrue(DateTime.Now - fechaVencimiento < TimeSpan.FromSeconds(1));
            Assert.AreEqual("Alta", prioridad);
            Assert.AreEqual("Pendiente", estado);
        }




        [TestMethod]
        public void Consultar()
        {
            //Metodo asume que ya se insertaron los datos y solo mira si estan registrado
            clsMetodosTest tarea = new clsMetodosTest();
            var datos = tarea.ObtenerDatosTarea();
            string strTitulo = datos.Titulo;
            string descripcion = datos.Descripcion;
            DateTime fechaVencimiento = datos.FechaVencimiento;
            string prioridad = datos.Prioridad;
            string estado = datos.Estado;


            // Assert
            var dates = tarea.ConsultarDatos();
            string strTitulo1 = datos.Titulo;
            string descripcion1 = datos.Descripcion;
            DateTime fechaVencimiento1 = datos.FechaVencimiento;
            string prioridad1 = datos.Prioridad;
            string estado1 = datos.Estado;


            Assert.AreEqual(strTitulo1, strTitulo);
            Assert.AreEqual(descripcion1, descripcion);
            Assert.IsTrue(DateTime.Now - fechaVencimiento < TimeSpan.FromSeconds(1));
            Assert.AreEqual(prioridad1, prioridad);
            Assert.AreEqual(estado1, estado);
        }

        [TestMethod]
        public void Eliminar()
        {
            // Asumiendo el metodo anterior miramos que los datos no traigan nada de esta manera asumimos que en los datos no hay nada
            clsMetodosTest tarea = new clsMetodosTest();
            var datos = tarea.EliminarDatos();
            string strTitulo = datos.Titulo;
            string descripcion = datos.Descripcion;
            DateTime? fechaVencimiento = datos.FechaVencimiento;
            string prioridad = datos.Prioridad;
            string estado = datos.Estado;


            Assert.AreEqual(string.Empty, strTitulo);
            Assert.AreEqual(string.Empty, descripcion);
            Assert.IsNull(fechaVencimiento);
            Assert.AreEqual(string.Empty, prioridad);
            Assert.AreEqual(string.Empty, estado);

        }

        [TestMethod]
        public void Actualizar()
        {
            // Miramos si los datos de entrada son diferentes a los nuevos datos y ahi se verifica que se modificó. 
            clsMetodosTest tarea1 = new clsMetodosTest();
            var datos1 = tarea1.ObtenerDatosTarea();
            string strTitulo1 = datos1.Titulo;
            string descripcion1 = datos1.Descripcion;
            DateTime fechaVencimiento1 = datos1.FechaVencimiento;
            string prioridad1 = datos1.Prioridad;
            string estado1 = datos1.Estado;

            clsMetodosTest tarea = new clsMetodosTest();
            var datos = tarea.Actualizar();
            string strTitulo = datos.Titulo;
            string descripcion = datos.Descripcion;
            DateTime? fechaVencimiento = datos.FechaVencimiento;
            string prioridad = datos.Prioridad;
            string estado = datos.Estado;


            Assert.AreNotEqual(strTitulo1, strTitulo);
            Assert.AreNotEqual(descripcion1, descripcion);
            Assert.AreNotEqual(fechaVencimiento, fechaVencimiento1);
            Assert.AreNotEqual(prioridad1, prioridad);
            Assert.AreNotEqual(estado1, estado);

        }

    }
}