using MVVM_test1.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_test1.ViewModel
{
    public abstract class Base_VM : Utilities.ViewModelBase
    {
        public ViewModelBase CurrentVM
        {
            get { return currentVM; }
            set
            {
                currentVM = value;
                OnPropertyChanged("CurrentVM");
            }
        }

        private ViewModelBase currentVM;
    }

}
