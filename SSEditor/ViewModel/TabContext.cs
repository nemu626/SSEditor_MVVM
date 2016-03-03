using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSEditor.ViewModel
{
    /// <summary>
    /// 一つのタブに収納されるstateのクラス
    /// ProjectとAppContextを保持するが、Undo,Redoのために各々履歴を保つ。
    /// 
    /// コンスタント
    /// MaxMemoryNumber : 履歴を幾つまで保てるかを設定。上記Memento Listの上限サイズ.
    /// 
    /// フィールド
    /// projectMemento : Projectの状態を履歴を保つ。
    /// contextMemento : Contextの状態の履歴を保つ。
    /// project : memento Listから現在のProjectのstateを指すNodeを指す
    /// context : memento Listから現在のContextのstateを指すNodeを指す
    /// 
    /// プロパティ  
    /// Project : 現在のProjectのstateを返す。
    /// AppContext : 現在のContextのstateを返す。
    /// </summary>
    public class TabContext
    {
        const int MaxMemoryNumber = 10;
        private LinkedList<Project> projectMemento;
        private LinkedList<AppContext> contextMemento;

        private LinkedListNode<Project> project;
        private LinkedListNode<AppContext> context;

        public Project Project { get { return project.Value; } private set { } }
        public AppContext Context { get { return context.Value; } private set { } }

        #region コンストラクタ
        public TabContext()
        {
            projectMemento = new LinkedList<Project>();
            contextMemento = new LinkedList<AppContext>();
            project = projectMemento.AddLast(new Project());
            context = contextMemento.AddLast(new AppContext());
        }
        public TabContext(Project p, AppContext c)
        {
            projectMemento = new LinkedList<Project>();
            contextMemento = new LinkedList<AppContext>();
            project = projectMemento.AddLast(p);
            context = contextMemento.AddLast(c);
        }
        #endregion

        #region Memento 操作
        /// <summary>
        /// 
        /// 新たなProject,Context（新たに更新された状態）を記録し、現在の状態にする。
        /// Memento記録数上限に達している場合、一番古いものを消す。
        /// 
        /// また、現在のStateが最新のものでない場合
        /// (Undoで過去に戻した状態から、Undo,Redo以外のアクションを起こしStateを変えた場合が該当)
        /// 現在のStateより未来の状態だったもの(UndoでたどってきたState)をすべて消去し、
        /// 新たにパラメータで指定されたp,cを記録する。
        /// </summary>
        /// <param name="p">新たなProject</param>
        /// <param name="c">新たなContext</param>
        public void recordMemento(Project p, AppContext c)
        {
            while(project.Next != null)
                projectMemento.Remove(project.Next);
            project = projectMemento.AddLast(p);
            context = contextMemento.AddLast(c);

            while (projectMemento.Count > MaxMemoryNumber)
                projectMemento.RemoveFirst();
            while (contextMemento.Count > MaxMemoryNumber)
                contextMemento.RemoveFirst();
        }
        
        /// <summary>
        /// Undo機能を使える状態か(戻れる履歴があるか)を判断する。
        /// </summary>
        /// <returns></returns>
        public bool CanUndo()
        {
            return (project.Previous != null && context.Previous != null);
        }
        /// <summary>
        /// ひとつ前のstateに戻る。
        /// </summary>
        public void Undo()
        {
            if (CanUndo())
            {
                project = project.Previous;
                context = context.Previous;
            }
        } 
        /// <summary>
        /// Redo機能を使える状態か(再び帰るstateがあるか)を判断する。
        /// </summary>
        /// <returns></returns>
        public bool CanRedo()
        {
            return (project.Next != null && context.Previous != null);
        }
        /// <summary>
        /// 一つ後のstateに戻る。
        /// </summary>
        public void Redo()
        {
            if(CanRedo())
            {
                project = project.Next;
                context = context.Next;
            }
        }
        #endregion

    }

}
