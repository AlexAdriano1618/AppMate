using AppMate.Models;
using AppMate.Services;
using AppMate.View;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppMate.ViewModels
{
    public class PreguntasViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;

        public PreguntasViewModel(string codigo)
        {
            //LoadCodigo(codigo);
        }
        #endregion
        private async void LoadCodigo(int codigo)
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
            var response = await this.apiService.GetList<Models.Pregunta>(
                "http://servicioappu.azurewebsites.net",
                "/api",
                "/Preguntas");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            MainViewModel.GetInstance().ListaPreguntas = (List<Pregunta>)response.Result;
            var a = MainViewModel.GetInstance().ListaCodigos.Exists(x => x.IdCodigo == codigo);

            if (a == false)
            {
                //this.IsRunnig = false;
                //this.IsEnable = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "No contiene preguntas.",
                    "Aceptar");
               // this.Codigo = string.Empty;
                return;
            }
            //this.IsRunnig = false;
            //this.IsEnable = true;

            //this.Codigo = string.Empty;

            //MainViewModel.GetInstance().Preguntas = new PreguntasViewModel();
            //await Application.Current.MainPage.Navigation.PushModalAsync(new Preguntas());

        }
    }
}
