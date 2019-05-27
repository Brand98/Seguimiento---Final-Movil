using System;
using Xamarin.Forms;
using FinalSeguimiento.Models;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FinalSeguimiento
{
    public partial class App : Application
    {
        public App(string filename)
        {
            InitializeComponent();
            Conexion.Inicializador(filename);
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
