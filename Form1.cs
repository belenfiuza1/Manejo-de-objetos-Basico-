using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Alumno A; List<Alumno> AList;
        public Form1()
        {
            InitializeComponent();
            A=new Alumno();
            AList = new List<Alumno>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataGridView1.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Alumno B = new Alumno(
                int.Parse(Interaction.InputBox("Ingrese el LEGAJO: ")),
                Interaction.InputBox("Ingrese el NOMBRE: "),
                Interaction.InputBox("Ingrese el APELLIDO: "),
                DateTime.Parse(Interaction.InputBox("Ingrese la FECHA DE NACIMIENTO: ","NO SE PUEDE MODIFICAR")),
                DateTime.Parse(Interaction.InputBox("Ingrese la FECHA DE INGRESO: ", "NO SE PUEDE MODIFICAR")),
                (MessageBox.Show("El alumno se encuentra ACTIVO?", "Seleccione una opcion", MessageBoxButtons.YesNo)) == DialogResult.Yes ? true : false,
                int.Parse(Interaction.InputBox("Ingrese la CANTIDAD DE MATERIAS APROBADAS: ", "NO SE PUEDE MODIFICAR")));
                //List<Alumno> AList = new List<Alumno>();
                AList.Add(B); dataGridView1.DataSource = null; dataGridView1.DataSource = AList;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);}
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                AList.Remove(dataGridView1.SelectedRows[0].DataBoundItem as Alumno);
                dataGridView1.DataSource = null; dataGridView1.DataSource = AList;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Alumno x = (dataGridView1.SelectedRows[0].DataBoundItem as Alumno);
                x.ModificarAlumno(x);
                dataGridView1.DataSource = null; dataGridView1.DataSource = AList;
                //dataGridView1.DataSource = null; dataGridView1.DataSource = AList;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = $"Edad de ingreso: {(dataGridView1.SelectedRows[0].DataBoundItem as Alumno).Edad_De_Ingreso()}\r\n" +
                                $"Antiguedad: {(dataGridView1.SelectedRows[0].DataBoundItem as Alumno).Antiguedad()}\r\n" +
                                $"Cantidad de materias no aprobadas: {(dataGridView1.SelectedRows[0].DataBoundItem as Alumno).Materias_No_Aprobadas()}";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

    }

    public class Alumno
    {
        DateTime Fecha_Nacimiento;
        DateTime Fecha_Ingreso;
        public int edad = 19;
        int materiasaprobadas;
        private int legajo;
        private string nombre;
        private string apellido;
        private bool activo;
        

        public Alumno() { }
        public Alumno(int Legajo, string Nombre, string Apellido, DateTime Fecha_Nacimiento, DateTime Fecha_Ingreso, bool Activo, int materiasaprobadas)
        {
            this.Legajo = Legajo; this.Nombre = Nombre; this.Apellido = Apellido; this.FechaNacimiento = Fecha_Nacimiento; this.FechaIngreso = Fecha_Ingreso; this.Activo = Activo; this.CantMateriasAprobadas = materiasaprobadas; ;
        }


        public int Legajo
        {
            get { return legajo; }
            set { legajo = value; }
        }


        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }


        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }


        public bool Activo
        {
            get { return activo; } // obetener
            set { activo = value; } // modificar
        }


        public DateTime FechaNacimiento { set { Fecha_Nacimiento = value; } }// set => this.Fecha_Nacimiento
        public DateTime FechaIngreso { set { Fecha_Ingreso = value; } }
        public int Edad { get { return edad; } }
        public int CantMateriasAprobadas { set { materiasaprobadas = value; } }


        public int Antiguedad() { return ((DateTime.Now.Year) - (Fecha_Ingreso.Year)); }
        public int Materias_No_Aprobadas() { return (36 - materiasaprobadas); }
        public int Edad_De_Ingreso() { return ((Fecha_Ingreso.Year) - (Fecha_Nacimiento.Year)); }
        public void ModificarAlumno(Alumno a) 
        { this.Legajo = int.Parse(Interaction.InputBox("Nuevo Legajo","",(a.Legajo).ToString()));
            a.Nombre = Interaction.InputBox("Nuevo Nombre","",a.Nombre);
            a.Apellido = Interaction.InputBox("Nuevo Apellido","",a.Apellido);
            a.Activo = (MessageBox.Show("El alumno se encuentra ACTIVO?", "Seleccione una opcion", MessageBoxButtons.YesNo)) == DialogResult.Yes ? true : false;
        }

        ~Alumno() { MessageBox.Show($"Se ha eliminado el alumno: {Nombre} {Apellido}. Legajo {Legajo}."); }
    }

    
}
