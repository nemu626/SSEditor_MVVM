using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SSEditor.ViewModel
{
    
    public class DeleteLineCommand : UndoRedoIcommand
    {
        private TabContext tabcontext;

        private Line backup;
        private int backupidx;

        public event EventHandler CanExecuteChanged;

        public DeleteLineCommand(TabContext t)
        {
            tabcontext = t;
            backup = null;
            backupidx = 0;
        }

        public bool CanExecute(object parameter = null)
        {
            return (tabcontext.Context.SelectedLine != null)
                && (tabcontext.Project.lines.Contains(tabcontext.Context.SelectedLine));
        }

        public void Execute(object parameter = null)
        {
            backupidx = tabcontext.Project.lines.IndexOf(tabcontext.Context.SelectedLine);
            backup = tabcontext.Context.SelectedLine.CloneDeep();
            
            tabcontext.Project.RemoveLine(tabcontext.Context.SelectedLine);
            tabcontext.Context.SelectedLine = null;
        }

        public void Redo()
        {
            if(backup == null)
                return;
            tabcontext.Context.SelectedLine = backup;
            backupidx = tabcontext.Project.lines.IndexOf(tabcontext.Context.SelectedLine);
            backup = tabcontext.Context.SelectedLine.CloneDeep();
            tabcontext.Project.RemoveLine(tabcontext.Context.SelectedLine);
        }

        public void Undo()
        {
            tabcontext.Project.AddLine(backup, tabcontext.Project.lines[backupidx - 1]);
        }
    }
}
