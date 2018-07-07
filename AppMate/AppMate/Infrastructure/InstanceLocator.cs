using System;
using System.Collections.Generic;
using System.Text;
using AppMate.ViewModels;
namespace AppMate.Infrastructure
{
    public class InstanceLocator
    {
        #region porpiedades
        public MainViewModel Main
        {
            get;
            set;

        }
        #endregion
        #region Constructor
        public InstanceLocator()
        {
            this.Main = new MainViewModel();

        }
        #endregion
    }
}
