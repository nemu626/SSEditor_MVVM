using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SSEditor.ViewModel
{
    
    /// <summary>
    /// Undo, Redo 機能を実装するICommand
    /// </summary>
    public interface UndoRedoIcommand : ICommand
    {
        void Undo();
        void Redo();
    }

    /// <summary>
    /// 実行されるコマンドをスタックに保持し、Undo,Redoを管理するオブジェクト
    /// </summary>
    public class CommandManager
    {
        const int UndoMax = 20;
        private LinkedList<UndoRedoIcommand> UndoStack;
        private LinkedList<UndoRedoIcommand> RedoStack;
        
        
        public CommandManager()
        {
            UndoStack = new LinkedList<UndoRedoIcommand>();
            RedoStack = new LinkedList<UndoRedoIcommand>();
        }

        public void Undo()
        {
            var cmd = UndoStack.Last();
            UndoStack.RemoveLast();
            cmd.Undo();
            RedoStack.AddLast(cmd);
        }
        public void Redo()
        {
            var cmd = RedoStack.Last();
            RedoStack.RemoveLast();
            cmd.Redo();
            UndoStack.AddLast(cmd);
        }
        public bool CanUndo()
        {
            return UndoStack.Any();
        }
        public bool CanRedo()
        {
            return RedoStack.Any();
        }
        public void Execute(UndoRedoIcommand cmd, object param = null)
        {
            UndoStack.AddLast(cmd);
            if (UndoStack.Count > UndoMax)
                UndoStack.RemoveFirst();
            RedoStack.Clear();

                cmd.Execute(param);
        }
    }


}
