
namespace AppMate.ViewModels
{
    using AppMate.Models;
    using AppMate.View;
    using GalaSoft.MvvmLight.Command;
    using AppMate.Services;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class InicioViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
        #region Atributos
        private string codigo;
        private bool isEnable;
        private bool isRunnig;
        #endregion

        #region Propiedades
        public string Codigo
        {
            get { return this.codigo; }
            set { SetValue(ref this.codigo, value); }

        }

        public bool IsRunnig
        {
            get { return this.isRunnig; }
            set { SetValue(ref this.isRunnig, value); }
        }
        public bool Recordado { get; set; }
        public bool IsEnable
        {
            get { return this.isEnable; }
            set { SetValue(ref this.isEnable, value); }
        }
        #endregion

        #region constructor

        public InicioViewModel()
        {
            this.Recordado = true;
            this.IsEnable = true;
            // this.codigo = "alexliga1998@outlook.com";
            // http://restcountries.eu/rest/v2/all

        }
        #endregion

        #region Methods
        private async void LoadCodigo()
        {
            this.apiService = new ApiService();
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            var response = await this.apiService.GetList<Codigo>(
                "http://servicioappu.azurewebsites.net",
                "/api",
                "/Codigoes");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            MainViewModel.GetInstance().ListaCodigos = (List<Codigo>)response.Result;
            var a = MainViewModel.GetInstance().ListaCodigos.Exists(x=>x.Identificacion== this.Codigo);
            
            if (a ==false)
            {
                this.IsRunnig = false;
                this.IsEnable = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Codigo incorecto.",
                    "Aceptar");
                this.Codigo = string.Empty;
                return;
            }
            this.IsRunnig = false;
            this.IsEnable = true;

           // this.Codigo = string.Empty;

            MainViewModel.GetInstance().Preguntas = new PreguntasViewModel(this.Codigo);
            await Application.Current.MainPage.Navigation.PushModalAsync(new Preguntas());

        }


        #endregion


        #region Command
        public ICommand InicioCommand
        {
            get
            {
                return new RelayCommand(Inicio);
            }
        }
        private async void Inicio()
        {
            if (string.IsNullOrEmpty(this.Codigo))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Por favor ingrese el codigo.",
                    "Aceptar");
                return;
            }


            this.IsRunnig = true;
            this.IsEnable = false;
            LoadCodigo();
            //if (this.Codigo != "alexliga1998@outlook.com")
            //{
            //    this.IsRunnig = false;
            //    this.IsEnable = true;
            //    await Application.Current.MainPage.DisplayAlert(
            //        "Error",
            //        "Codigo incorecto.",
            //        "Aceptar");
            //    this.Codigo = string.Empty;
            //    return;
            //}
            //this.IsRunnig = false;
            //this.IsEnable = true;


            //this.Codigo = string.Empty;

            //MainViewModel.GetInstance().Preguntas = new PreguntasViewModel();
            //await Application.Current.MainPage.Navigation.PushModalAsync(new Preguntas());
        }
        #endregion

    }

}
