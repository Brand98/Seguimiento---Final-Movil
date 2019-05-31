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
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            int blanco = ValidarGeneral();
            if (blanco <= 0)
            {
                Estado.Text = string.Empty;
                Conexion.Instancia.addNew(mov.Text, con.Text, Convert.ToDouble(val.Text), det.Text, Convert.ToDateTime(fec.Text), rec.Text);
                Estado.Text = Conexion.Instancia.EstadoDeMensaje;
                DisplayAlert("Alert", Estado.Text, "OK");
                Clean();
            }
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
            if(string.IsNullOrEmpty(con.Text) || string.IsNullOrWhiteSpace(con.Text))
            {
                DisplayAlert("Alert", "Por favor ingrese el concepto", "ERROR");
            }
            else
            {
                string[] data = new string[10];
                string info = string.Empty;
                data = Conexion.Instancia.Cargar(con.Text);
                for(int i = 0; i < 10; i++)
                {
                    info = info + " " + data[i];
                }
                mov.Text = data[0];
                val.Text = data[2];
                fec.Text = data[3];
                rec.Text = data[4];
                det.Text = data[5];
                DisplayAlert("Mensaje", info, "OK");
            }            
        }

        private void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            Conexion.Instancia.Update(mov.Text, con.Text, Convert.ToDouble(val.Text), det.Text, Convert.ToDateTime(fec.Text), rec.Text);
            DisplayAlert("Mensaje", "Los valores se actualizaron correctamente", "OK")
            Clean();
        }        

        private void BtnEliminarxNombre_Clicked(object sender, EventArgs e)
        {
            Conexion.Instancia.DeleteByName(con.Text);
            DisplayAlert("Alert", "El registro fue eliminado con exito", "OK");
        }

        private void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            Conexion.Instancia.DeleteByName(con.Text);
            DisplayAlert("Alert", "Los registros se eliminaron con exito", "OK");            
        }

        

        public int ValidarGeneral()
        {
            int contador = 0;
            ValidarBlanco(mov);
            ValidarBlanco(con);
            ValidarBlanco(val);
            ValidarBlanco(det);
            ValidarBlanco(fec);
            contador = ValidarBlanco(rec);
            //ValidarNumero();
            return contador;
        }

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

        public void Clean()
        {
            mov.Text = string.Empty;
            con.Text = string.Empty;
            val.Text = string.Empty;
            det.Text = string.Empty;
            fec.Text = string.Empty;
            rec.Text = string.Empty;
        }

        private async Task validarFormulario()
        {
            //Valida si el valor en el Entry se encuentra vacio o es igual a Null
            if (String.IsNullOrWhiteSpace(mov.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo del nombre es obligatorio.", "OK");
               // return false;
            }
            //Valida que solo se ingresen letras
            else if (!mov.Text.ToCharArray().All(Char.IsLetter))
            {
                await this.DisplayAlert("Advertencia", "Tu información contiene números, favor de validar.", "OK");
                //return false;
            }
            else
            {
                //Valida si se ingresan caracteres especiales
                string caractEspecial = @"^[^ ][a-zA-Z ]+[^ ]$";
                bool resultado = Regex.IsMatch(mov.Text, caractEspecial, RegexOptions.IgnoreCase);
                if (!resultado)
                {
                    await this.DisplayAlert("Advertencia", "No se aceptan caracteres especiales, intente de nuevo.", "OK");
                  //  return false;
                }
            }
        }        
    }
}
