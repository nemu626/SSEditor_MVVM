using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SSEditor.ViewModel
{
    public abstract class LineCommand : UndoRedoIcommand
    {
        protected TabContext tabcontext;
        protected Line backup;
        protected int backupidx;

        protected ObservableCollection<Line> List
        {
            get { return tabcontext.Project.lines; }
            set { tabcontext.Project.lines = value; }
        }
        protected Line Selected
        {
            get { return tabcontext.Context.SelectedLine; }
            set { tabcontext.Context.SelectedLine = value; }
        }


        public LineCommand(TabContext tab)
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

    public class AddLineCommand : LineCommand
    {
        public AddLineCommand(TabContext tab)
            : base(tab)
        { }
        public override bool CanExecute(object parameter = null)
        {
            if (tabcontext.Context.SelectedPerson == Person.DESCRIPT)
                return (!string.IsNullOrEmpty(tabcontext.Context.InputText) 
                    && tabcontext.Context.EditorMode == (int)EditMode.insert);
            else
                return (tabcontext.Context.SelectedPerson != null && tabcontext.Context.SelectedParen != null
                && tabcontext.Context.EditorMode == (int)EditMode.insert);
        }
        public override void Execute(object parameter = null)
        {
            backupidx = (List.Count);
            backup = new Line(tabcontext.Context.InputText, tabcontext.Context.SelectedPerson, tabcontext.Context.SelectedParen);
            tabcontext.Project.AddLine(backup);
            tabcontext.Context.InputText = "";
        }
        public override void Redo()
        {
            tabcontext.Project.AddLine(backup);
        }
        public override void Undo()
        {
            var temp = backup.CloneDeep();
            tabcontext.Project.RemoveLine(List[backupidx]);
            backup = temp;
        }
    }
    public class DeleteLineCommand : LineCommand
    {
        public DeleteLineCommand(TabContext tab)
            : base(tab){ }
        public override bool CanExecute(object parameter = null)
        {
            return (Selected != null)
                && (List.Contains(Selected));
        }
        public override void Execute(object parameter = null)
        {
            backupidx = List.IndexOf(tabcontext.Context.SelectedLine);
            backup = tabcontext.Context.SelectedLine.CloneDeep();
            
            tabcontext.Project.RemoveLine(tabcontext.Context.SelectedLine);
            tabcontext.Context.SelectedLine = null;
        }
        public override void  Redo()
        {
            if(backup == null)
                return;
            Selected = backup;
            backupidx = List.IndexOf(Selected);
            backup = Selected.CloneDeep();
            tabcontext.Project.RemoveLine(Selected);
        }
        public override void Undo()
        {
            tabcontext.Project.AddLine(backup, List[backupidx - 1]);
        }
    }
    public class ModifyLineCommand : LineCommand
    {
        public ModifyLineCommand(TabContext tab)
            :base(tab){}

        public override bool CanExecute(object parameter = null)
        {
            return (tabcontext.Context.SelectedPerson != null && tabcontext.Context.SelectedParen != null
                && (List.Contains(Selected))
                && tabcontext.Context.EditorMode == (int)EditMode.modify);
        }
        public override void Execute(object parameter = null)
        {
            backup = Selected.CloneDeep();
            backupidx = List.IndexOf(Selected);
            tabcontext.Project.ModifyLine(Selected, tabcontext.Context.InputText,
                tabcontext.Context.SelectedPerson,tabcontext.Context.SelectedParen);
            tabcontext.Context.InputText = "";

        }

        public override void Redo()
        {
            var temp = List[backupidx];
            List[backupidx] = backup;
            backup = temp;
        }

        public override void Undo()
        {
            var temp = List[backupidx];
            List[backupidx] = backup;
            backup = temp;
        }
    }
    public class InterpolateLineCommand : LineCommand
    {
        public InterpolateLineCommand(TabContext tab)
            : base(tab){ }

        public override bool CanExecute(object parameter = null)
        {
            return (tabcontext.Context.SelectedPerson != null && tabcontext.Context.SelectedParen != null
                && (List.Contains(Selected))
                && tabcontext.Context.EditorMode == (int)EditMode.interpolate);
        }
        public override void Execute(object parameter = null)
        {
            backupidx = (List.IndexOf(Selected)) + 1;
            backup = new Line(tabcontext.Context.InputText, tabcontext.Context.SelectedPerson, tabcontext.Context.SelectedParen);
            tabcontext.Project.AddLine(backup, Selected);
        }

        public override void Redo()
        {
            tabcontext.Project.AddLine(backup, List[backupidx - 1]);
        }
        public override void Undo()
        {
            var temp = backup.CloneDeep();
            tabcontext.Project.RemoveLine(List[backupidx]);
            backup = temp;
        }
    }
    public class AddDescriptionLineCommand : LineCommand
    {
        public AddDescriptionLineCommand(TabContext tab)
            : base(tab)
        { }
        public override bool CanExecute(object parameter = null)
        {
            return !string.IsNullOrEmpty(tabcontext.Context.InputText)
                && tabcontext.Context.EditorMode == (int)EditMode.insert;
        }

        public override void Execute(object parameter = null)
        {
            var person = tabcontext.Context.SelectedPerson;
            var paren = tabcontext.Context.SelectedParen;
            tabcontext.Context.SelectedPerson = Person.DESCRIPT;
            tabcontext.Context.SelectedParen = Parentheses.BASE_EMPTY;

            backupidx = (List.Count);
            backup = new Line(tabcontext.Context.InputText, tabcontext.Context.SelectedPerson, tabcontext.Context.SelectedParen);
            tabcontext.Project.AddLine(backup);
            tabcontext.Context.InputText = "";

            tabcontext.Context.SelectedPerson = person;
            tabcontext.Context.SelectedParen = paren;
        }

        public override void Redo()
        {
            tabcontext.Project.AddLine(backup);

        }

        public override void Undo()
        {
            var temp = backup.CloneDeep();
            tabcontext.Project.RemoveLine(List[backupidx]);
            backup = temp;
        }
    }
}
