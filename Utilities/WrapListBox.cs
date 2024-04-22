using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace MVVM_test1.Utilities
{
    
        class WrapListBox : ListBox
        {
            static WrapListBox()
            {
                DefaultStyleKeyProperty.OverrideMetadata(typeof(WrapListBox), new FrameworkPropertyMetadata(typeof(WrapListBox)));
            }
        }
    
}
