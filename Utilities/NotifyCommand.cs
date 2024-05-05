using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVVM_test1.Utilities
{
    public class NotifyCommand : BaseCommand
    {
        private readonly NotifyIcon _notifyIcon;
        public NotifyCommand (NotifyIcon notifyIcon)
        {
            _notifyIcon = notifyIcon;
        }
        public override void Execute(object parameter)
        {
            
        }
    }
}
