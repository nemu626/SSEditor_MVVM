using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SSEditor.ViewModel.Commands
{
    public abstract class ParenCommand : ICommand
    {

        protected ObservableCollection<Parentheses> List { get; set; }

        public event EventHandler CanExecuteChanged;

        public ParenCommand(ObservableCollection<Parentheses> parens)
        {
            List = parens;
        }
        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);
    }

}
