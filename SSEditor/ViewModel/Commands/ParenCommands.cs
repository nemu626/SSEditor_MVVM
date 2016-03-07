using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSEditor.ViewModel.Commands
{
    public class ParenCommand : UndoRedoIcommand
    {
        protected TabContext tabcontext;
        protected Parentheses backup;
        protected int backupidx;

        protected ObservableCollection<Parentheses> List
        {
            get { return tabcontext.Project.parens; }
            set { tabcontext.Project.parens = value; }
        }
        protected Parentheses Selected
        {
            get { return tabcontext.Context.SelectedParen; }
            set { tabcontext.Context.SelectedParen = value; }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
