using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FinalSeguimiento.Models;
using System.Text.RegularExpressions;
using SQLite;

namespace FinalSeguimiento
{
    public partial class MainPage : ContentPage
    {
        int cont = 0;
        public string fecha;
        public MainPage()
        {
            InitializeComponent();
            mov.Items.Add("INGRESO");
            mov.Items.Add("GASTO");
            rec.Items.Add("SI");
            rec.Items.Add("NO");
            //cuadrar logica de llenado de acuerdo al tipo de movimiento 
        }

        private void BtnAgregar_Clicked(object sender, EventArgs e)
        {   
            Estado.Text = string.Empty;
            Conexion.Instancia.addNew(mov.Items[mov.SelectedIndex], con.Items[mov.SelectedIndex], Convert.ToDouble(val.Text), det.Text, Convert.ToDateTime(fecha), rec.Items[rec.SelectedIndex]);
            Estado.Text = Conexion.Instancia.EstadoDeMensaje;
            DisplayAlert("Alert", Estado.Text, "OK");
            
            //Clean();         
        }

        private void BtnConsultar_Clicked(object sender, EventArgs e)
        {            
            string num = Conexion.Instancia.cont().ToString();
            DisplayAlert("Alert", "numero de registros " + num, "OK");

            var allMovimientos = Conexion.Instancia.GetAllReg();
            RegistroMov.ItemsSource = allMovimientos;
            Estado.Text = Conexion.Instancia.EstadoDeMensaje;
        }

        private void BtnCargar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(con.Items[mov.SelectedIndex]) || string.IsNullOrWhiteSpace(con.Items[mov.SelectedIndex]))
            {
                DisplayAlert("Alert", "Por favor ingrese el concepto", "ERROR");
            }
            else
            {
                string[] data = new string[10];
                string info = string.Empty;
                data = Conexion.Instancia.Cargar(con.Items[mov.SelectedIndex]);
                for (int i = 0; i < 10; i++)
                {
                    info = info + " " + data[i];
                }
                mov.Items[mov.SelectedIndex] = data[0];
                val.Text = data[2];
                fecha = data[3];
                rec.Items[rec.SelectedIndex] = data[4];
                det.Text = data[5];
                DisplayAlert("Mensaje", info, "OK");
            }
        }

        private void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            Conexion.Instancia.Update(mov.Items[mov.SelectedIndex], con.Items[mov.SelectedIndex], Convert.ToDouble(val.Text), det.Text, Convert.ToDateTime(fecha), rec.Items[rec.SelectedIndex]);
            DisplayAlert("Mensaje", "Los valores se actualizaron correctamente", "OK");
            //Clean();
        }        

        private void BtnEliminarxNombre_Clicked(object sender, EventArgs e)
        {
            Conexion.Instancia.DeleteByName(con.Items[mov.SelectedIndex]);
            DisplayAlert("Alert", "El registro fue eliminado con exito", "OK");
        }

        private void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            Conexion.Instancia.Delete();
            DisplayAlert("Alert", "Los registros se eliminaron con exito", "OK");            
        }



        //public int ValidarGeneral()
        //{
        //    int contador = 0;
        //    ValidarBlanco(mov.Items[mov.Selected);
        //    ValidarBlanco(con);
        //    ValidarBlanco(val);
        //    ValidarBlanco(det);
        //    ValidarBlanco(fec);
        //    contador = ValidarBlanco(rec);
        //    //ValidarNumero();
        //    return contador;
        //}

        public int ValidarBlanco(Entry label)
        {
            if (String.IsNullOrEmpty(label.Text) || string.IsNullOrWhiteSpace(label.Text))
            {
                DisplayAlert("Alert", "Por favor ingrese valores en los campos correspondientes", "OK");
                cont += 1;
            }
            return cont;
        }

        public void ValidarNumero()
        {
            if (!val.Text.ToCharArray().All(char.IsDigit))
            {
                DisplayAlert("Alert", "Dentro del campo valor, solo se aceptan números", "OK");
                val.Text.Equals("");
            }
        }

        //public void Clean()
        //{
        //    mov.Items[mov.SelectedIndex] = string.Empty;
        //    con.Items[mov.SelectedIndex] = string.Empty;
        //    val.Text = string.Empty;
        //    det.Text = string.Empty;
        //    rec.Items[rec.SelectedIndex] = string.Empty;
        //}

        private void Mov_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Items.Clear();
            if (mov.Items[mov.SelectedIndex] == "INGRESO")
            {
                con.Items.Add("NOMINA EMPLEO 1");
                con.Items.Add("NOMINA EMPLEO 2");
                con.Items.Add("NOMINA EMPLEO 3");
                con.Items.Add("SUBSIDIO FAMILIAR");
                con.Items.Add("INTERESES SOBRE CAPITAL");
            }
            else if (mov.Items[mov.SelectedIndex] == "GASTO")
            {
                con.Items.Add("SERVICIOS PUBLICOS");
                con.Items.Add("SERVICIOS DE TELEFONIA");
                con.Items.Add("SERVICIOS DE INTERNET");
                con.Items.Add("SERVICIOS DE TV");
                con.Items.Add("ALIMENTACION");
            }
            else if (mov.Items[mov.SelectedIndex] == "")
            {

            }
        }

        private void Fec_DateSelected(object sender, DateChangedEventArgs e)
        {
            fecha = e.NewDate.ToString();
        }
    }
}
