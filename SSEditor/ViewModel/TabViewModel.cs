using SSEditor.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSEditor.ViewModel
{
    public class TabViewModel
    {
        public CommandManager Manager { get; private set; }

        private TabContext tabcontext;
        public TabContext TabContext { get { return tabcontext; } }

        public DeleteLineCommand DeleteLine { get; private set; }
        public AddLineCommand AddLine { get; private set; }
        public ModifyLineCommand ModifyLine { get; private set; }
        public InterpolateLineCommand InterpolateLine { get; private set; }
        public AddDescriptionLineCommand AddDescription { get; private set; }

        public TabViewModel(TabContext t = null)
        {
            
            tabcontext = t ?? new TabContext();
            Manager = new CommandManager();
            DeleteLine = new DeleteLineCommand(tabcontext);
            AddLine = new AddLineCommand(tabcontext);
            ModifyLine = new ModifyLineCommand(tabcontext);
            InterpolateLine = new InterpolateLineCommand(tabcontext);
            AddDescription = new AddDescriptionLineCommand(tabcontext);
        }


        public void SubmitLine()
        {
            switch (tabcontext.Context.EditorMode)
            {
                case (int)EditMode.insert:
                    Manager.Execute(new AddLineCommand(tabcontext));
                    break;
                case (int)EditMode.modify:
                    Manager.Execute(new ModifyLineCommand(tabcontext));
                    break;
                case (int)EditMode.interpolate:
                    Manager.Execute(new InterpolateLineCommand(tabcontext));
                    break;
            }
        }
        public bool CanSubmitLine()
        {
            switch (tabcontext.Context.EditorMode)
            {
                case (int)EditMode.insert:
                    return AddLine.CanExecute();
                case (int)EditMode.modify:
                    return ModifyLine.CanExecute();
                case (int)EditMode.interpolate:
                    return InterpolateLine.CanExecute();
                default:
                    return false;
            }
        }
    }
}
