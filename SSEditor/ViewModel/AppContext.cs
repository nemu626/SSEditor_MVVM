using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSEditor.ViewModel
{
    public enum EditMode { insert, modify, interpolate}
    /// <summary>
    /// プロパティ
    /// FileName : 現在開かれているファイル名
    /// FilePath : 現在開かれているファイルパス
    /// EditorMode : 現在のエディタモード（挿入、修正、割り込み挿入）
    /// 
    /// SelectedPerson : 現在選択されているperson
    /// SelectedLine : 現在選択されているLine
    /// SelectedParen : 現在選択されているParen
    /// 
    /// InputText : 入力ボックスに現在入力されている文字列
    /// </summary>
    public class AppContext : INotifyPropertyChanged
    {
        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                OnPropertyChanged("FileName");
            }
        }
        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                OnPropertyChanged("FilePath");
            }
        }


        //private bool modifyModeFlag;
        //public bool ModifyModeFlag
        //{
        //    get { return modifyModeFlag; }
        //    set
        //    {
        //        modifyModeFlag = value;
        //        OnPropertyChanged("ModifyModeFlag");
        //    }
        //}
        //private bool interruptModeFlag;
        //public bool InterruptModeFlag
        //{
        //    get { return interruptModeFlag; }
        //    set
        //    {
        //        interruptModeFlag = value;
        //        OnPropertyChanged("InterruptModeFlag");
        //    }
        //}

        private EditMode editorMode;
        public int EditorMode
        {
            get { return (int)editorMode; }
            set
            {
                editorMode = (EditMode)value;
                OnPropertyChanged("EditorMode");
            }
        }

        private Person selectedPerson;
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }
        private Line selectedLine;
        public Line SelectedLine
        {
            get { return selectedLine; }
            set
            {
                selectedLine = value;
                OnPropertyChanged("SelectedLine");
            }
        }


        private string inputText;
        public string InputText
        {
            get { return inputText; }
            set
            {
                inputText = value;
                OnPropertyChanged("InputText");
            }
        }


        #region 括弧管理設定画面

        private Parentheses selectedParen;
        public Parentheses SelectedParen
        {
            get { return selectedParen; }
            set
            {
                selectedParen = value;
                OnPropertyChanged("SelectedParen");
            }
        }
        private bool modifyParenModeFlag;
        public bool ModifyParenModeFlag
        {
            get { return modifyParenModeFlag; }
            set
            {
                modifyParenModeFlag = value;
                //Add Modeになった場合SelectedParenを新しく取得する
                if (value == false)
                    SelectedParen = new Parentheses("", "", "");
                OnPropertyChanged("ModifyParenModeFlag");
            }
        }

        #endregion


        public AppContext()
        {
            fileName = "Untitled";
            filePath = "";
            EditorMode = (int)EditMode.insert;
            selectedPerson = null;
            selectedParen = null;
            selectedLine = null;
            inputText = "";
            modifyParenModeFlag = true;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        //未実装
        #region File Control Commands
        public RelayCommand FileOpenCom { get; private set; }
        public RelayCommand FileSaveCom { get; private set; }
        public RelayCommand FileSaveAsCom { get; private set; }
        #endregion
    }
}
