using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SSEditor.ViewModel.Commands
{

    public abstract class PersonCommand : UndoRedoIcommand
    {
        protected TabContext tabcontext;
        protected Person backup;
        protected int backupidx;

        protected ObservableCollection<Person>
            List { get { return tabcontext.Project.people; }
                   set { tabcontext.Project.people = value; } }
        protected Person Selected { get { return tabcontext.Context.SelectedPerson; }
                                    set { tabcontext.Context.SelectedPerson = value; } }
        

        public PersonCommand(TabContext tab)
        {
            tabcontext = tab;
            backup = null;
            backupidx = 0;
        }

        public event EventHandler CanExecuteChanged;
        public abstract bool CanExecute(object parameter = null);
        public abstract void Execute(object parameter = null);
        public abstract void Redo();
        public abstract void Undo();
    }

    public class AddPersonCommand : PersonCommand
    {
        AddPersonCommand(TabContext tab)
            : base(tab){ }
        public override bool CanExecute(object parameter = null)
        {
            return (Selected != null && !List.Contains(Selected));
        }

        public override void Execute(object parameter = null)
        {
            backupidx = List.Count;
            backup = Selected;
            tabcontext.Project.AddPerson(Selected);
        }
        public override void Redo()
        {
            tabcontext.Project.AddPerson(backup);
        }
        public override void Undo()
        {
            var temp = backup.CloneDeep();
            tabcontext.Project.RemovePerson(List[backupidx]);
            backup = temp;
        }
    }
    public class DeletePersonCommand : PersonCommand
    {
        public DeletePersonCommand(TabContext tab)
            : base(tab)
        { }
        public override bool CanExecute(object parameter = null)
        {
            return (Selected != null && List.Contains(Selected));
        }
        public override void Execute(object parameter = null)
        {
            backup = Selected.CloneDeep();
            backupidx = List.IndexOf(Selected);
            List.Remove(Selected);
        }
        public override void Redo()
        {
            if (backup == null)
                return;
            Selected = backup;
            backupidx = List.IndexOf(Selected);
            backup = Selected.CloneDeep();
            List.Remove(Selected);
        }
        public override void Undo()
        {
            List.Insert(backupidx, backup);
        }
    }
    public class DeletePersonWithLinesCommand : PersonCommand
    {
        private ObservableCollection<Line> backupLines;
        public DeletePersonWithLinesCommand(TabContext tab)
            : base(tab)
        { backupLines = null; }
        public override bool CanExecute(object parameter = null)
        {
            return (Selected != null && List.Contains(Selected));
        }
        public override void Execute(object parameter = null)
        {
            backup = Selected.CloneDeep();
            backupidx = List.IndexOf(Selected);
            backupLines = tabcontext.Project.lines.CloneDeep();
            tabcontext.Project.RemovePerson(Selected);
        }
        public override void Redo()
        {
            if (backup == null)
                return;
            Selected = backup;
            backupidx = List.IndexOf(Selected);
            backupLines = tabcontext.Project.lines.CloneDeep();

            backup = Selected.CloneDeep();
            List.Remove(Selected);
        }
        public override void Undo()
        {
            tabcontext.Project.lines = backupLines;
            List.Insert(backupidx, backup);
        }
    }
}
