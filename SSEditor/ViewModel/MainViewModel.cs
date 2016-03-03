using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SSEditor.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SSEditor.ViewModel
{
    /// <summary>
    /// Mvvm light��ViewModelBase���p��
    /// 
    ///�v���p�e�B(�ύX��Notify����Property)
    ///Project : �ҏW���̃t�@�C���Ɋւ���v���p�e�B
    ///appOption : �A�v���̊��ݒ�l�Ɋւ���v���p�e�B�i�����I��settings.settings�ő�ւł��邩��)
    ///appContext : ���݂̃A�v���̃R���e�L�X�g�Ɋւ���v���p�e�B(�I�����ꂽLine,Person��)

    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        
        public ObservableCollection<TabContext> tabs { get; set; }

        private int currentTabIdx;
        public int CurrentTabIdx {
            get { return currentTabIdx; }
            set
            {
                if(value < tabs.Count)
                {
                    currentTabIdx = value;
                    RaisePropertyChanged("CurrentTabIdx");
                    RaisePropertyChanged("Tab");
                    RaisePropertyChanged("Project");
                    RaisePropertyChanged("AppContext");
                }
            }
            }

        public List<string> TabNames
        {
            get
            {
                var s = new List<string>();
                foreach (TabContext t in tabs)
                    s.Add(t.Project.title);
                return s;
            }
        }

        public TabContext Tab
        {
            get { return tabs[CurrentTabIdx]; }
            private set
            {
                currentTabIdx = tabs.IndexOf(value);
                RaisePropertyChanged("Tab");
            }
        }

//        private Project project;
        public Project Project
        {
            get { return Tab.Project; }
            //private set
            //{
            //    project = value;
            //    RaisePropertyChanged("Project");
            //}
        }

//        private AppContext appContext;
        public AppContext AppContext
        {
            get { return Tab.Context; }
            //set
            //{
            //    appContext = value;
            //    RaisePropertyChanged("AppContext");
            //}
        }

        private AppOption appoption;
        public AppOption AppOption
        {
            get { return appoption; }
            set
            {
                appoption = value;
                RaisePropertyChanged("AppOption");
            }
        }




        public MainViewModel()
        {
            //AppContext = new AppContext();
            //Project = new Project();
            tabs = new ObservableCollection<TabContext>();
            tabs.Add(new TabContext());
            currentTabIdx = 0;

            AppOption = new AppOption();
            AppContext.SelectedParen = Parentheses.BASE_KAGI;

            #region Line�Ǘ��R�}���h
            TypeLineCom = new RelayCommand(AddModifyLine,
                () => (AppContext.SelectedPerson != null && AppContext.SelectedParen != null));
            TypeDescriptCom = new RelayCommand(
                () => {
                    var person = AppContext.SelectedPerson;
                    var paren = AppContext.SelectedParen;
                    AppContext.SelectedPerson = Person.DESCRIPT;
                    AppContext.SelectedParen = Parentheses.BASE_EMPTY;
                    AddModifyLine();
                    AppContext.SelectedPerson = person;
                    AppContext.SelectedParen = paren;
                },
                () => !string.IsNullOrEmpty(AppContext.InputText));
            #endregion

            #region Mode�Ǘ��R�}���h
            SetModifyModeCom = new RelayCommand(
                () =>
                {
                    AppContext.EditorMode = (int)EditMode.modify;
                    AppContext.InputText = AppContext.SelectedLine.line;
                    AppContext.SelectedPerson = AppContext.SelectedLine.speaker;
                    AppContext.SelectedParen = AppContext.SelectedLine.paren;
                },
                () => AppContext.SelectedLine != null && 
                (       (AppContext.EditorMode == (int)EditMode.modify ) 
                        ||(AppContext.EditorMode == (int)EditMode.insert && AppContext.InputText == "")) );

            SetInterpolateModeCom = new RelayCommand(
                () => { AppContext.EditorMode = (int)EditMode.interpolate; },
                () => (AppContext != null && Project.lines.Contains(AppContext.SelectedLine))
                );
            #endregion


            DeleteLineCom = new RelayCommand(
                () =>
                {
                    if (true) Project.RemoveLine(AppContext.SelectedLine);
                    AppContext.SelectedLine = null;
                },
                () => (AppContext.SelectedLine != null && Project.lines.Contains(AppContext.SelectedLine))
                );

            #region Paren�Ǘ�
            AddParenCom = new RelayCommand(
                () => { Project.parens.Add(AppContext.SelectedParen);
                    AppContext.SelectedParen = new Parentheses("", "", ""); },
                () => { return (AppContext.SelectedParen != null && AppContext.ModifyParenModeFlag == false);
                });
            DeleteParenCom = new RelayCommand<bool>(
                (doExecute) => { if (doExecute) Project.parens.Remove(AppContext.SelectedParen);
                    AppContext.SelectedParen = null; },
                (doExecute) => (AppContext.SelectedParen != null && Project.parens.Contains(AppContext.SelectedParen)));

            #endregion

            #region Person�Ǘ�
            AddPersonCom = new RelayCommand(AddnewPerson,
                () => (AppContext.SelectedPerson != null && !Project.people.Contains(AppContext.SelectedPerson))
                );
            DeletePersonCom = new RelayCommand<bool>(
                (doExecuse) => { if (doExecuse) Project.RemovePerson(AppContext.SelectedPerson); },
                (doExecuse) => (AppContext.SelectedPerson != null && Project.people.Contains(AppContext.SelectedPerson))
                );
            #endregion

            #region
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            #endregion
        }

        #region Paren�Ǘ���ʃR�}���h
        public RelayCommand AddParenCom { get; private set; }

        public RelayCommand<bool> DeleteParenCom { get; private set; }
        private void DeleteParen()
        {
            if (AppContext != null && Project.parens.Contains(AppContext.SelectedParen))
                Project.parens.Remove(AppContext.SelectedParen);
        }
        #endregion

        #region Line�Ǘ��R�}���h
        private void AddModifyLine()
        {
            if (AppContext.EditorMode == (int)EditMode.insert)
                Project.AddLine(new Line(AppContext.InputText, AppContext.SelectedPerson, AppContext.SelectedParen));
            else if (AppContext.EditorMode == (int)EditMode.modify)
            {
                if (AppContext.SelectedLine != null &&
                    AppContext.SelectedLine.Modify(AppContext.InputText, AppContext.SelectedPerson))
                    RaisePropertyChanged("line");
            }
            else// AppContext.EditorMode == EditorMode.interpolate
                Project.AddLine(new Line(AppContext.InputText, AppContext.SelectedPerson, AppContext.SelectedParen),
                    AppContext.SelectedLine);
            AppContext.InputText = "";
        }
        public RelayCommand TypeLineCom { get; private set; }
        public RelayCommand TypeDescriptCom { get; private set; }
        public RelayCommand DeleteLineCom { get; private set; } 
        #endregion

        #region Mode�Ǘ��R�}���h
        public RelayCommand SetModifyModeCom { get; private set; }
        public RelayCommand SetInterpolateModeCom { get; private set; }


        #endregion

        #region Person�Ǘ�
        public RelayCommand AddPersonCom { get; private set; }
        public RelayCommand<bool> DeletePersonCom { get; private set; }
 //       public RelayCommand ModifyPersonCom { get; private set; }
        private void AddnewPerson()
        {
            Project.AddPerson(AppContext.SelectedPerson);
            AppContext.SelectedPerson = null;
        }
        private void DeletePerson()
        {
            if (Project.people.Contains(AppContext.SelectedPerson))
            {
                Project.RemovePerson(AppContext.SelectedPerson);
            }
        }
        #endregion

        /// <summary>
        /// Tab��V�����ǉ�
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public bool AddTab(TabContext tab)
        {
            if (tab != null)
            {
                tabs.Add(tab);
                RaisePropertyChanged("TabNames");
                return true;
            }
            return false;
        }
        public bool RemoveTab(TabContext tab)
        {
            
            if (tabs.Contains(tab) && tabs.Count > 0)
            {
                if(currentTabIdx >= tabs.IndexOf(tab))
                    currentTabIdx--;
                tabs.Remove(Tab);
                RaisePropertyChanged("TabNames");
                return true;
            }
            return false;
        }
        

    }
}