using AppMate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppMate.ViewModels
{
    public class MainViewModel
    {

        #region ViewModel
        public List<Codigo> ListaCodigos
        {
            get;
            set;
        }
        public List<Pregunta> ListaPreguntas
        {
            get;
            set;
        }
        public InicioViewModel Inicio
        {
            get;
            set;
        }
        public PreguntasViewModel Preguntas
        {
            get;
            set;
        }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            Instance = this;
            this.Inicio = new InicioViewModel();
        }
        #endregion

        #region Singleton

        private static MainViewModel Instance;
        public static MainViewModel GetInstance()
        {
            if (Instance == null)
            {
                return new MainViewModel();
            }
            return Instance;
        }
        #endregion
    }
}
