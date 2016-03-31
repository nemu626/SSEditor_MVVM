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
    ///tabs : �^�u�̃R���N�V�����B
    ///CurrentTabIdx : ���݊J����Ă���^�u��Index
    ///TabNames : �e�^�u�̃t�@�C�������X�g
    ///
    ///AppOption : �A�v���̊��ݒ�l�Ɋւ���v���p�e�B�i�����I��settings.settings�ő�ւł��邩��)
    ///AppTheme : �A�v���̃J���[�e�[�}
    ///
    /// �ȉ��A���݂̃^�u�̃v���p�e�B�ւ̃A�N�Z�b�T
    /// CurrentTab
    /// CurrentTabContext
    /// Manager
    /// Project
    /// AppContext
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<TabViewModel> tabs { get; private set; }

        private int currentTabIdx;
        public int CurrentTabIdx {
            get { return currentTabIdx; }
            set
            {
                if (value < tabs.Count)
                {
                    currentTabIdx = value;
                    RaisePropertyChanged("CurrentTabIdx");
                    RaisePropertyChanged("CurrentTab");
                    RaisePropertyChanged("CurrentTabContext");
                    RaisePropertyChanged("Project");
                    RaisePropertyChanged("AppContext");
                    RaisePropertyChanged("Manager");
                }
            }
        }

        public List<string> TabNames
        {
            get
            {
                var s = new List<string>();
                foreach (TabViewModel t in tabs)
                    s.Add(t.TabContext.Project.title);
                return s;
            }
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
        private Theme appTheme;
        public Theme AppTheme
        {
            get { return appTheme; }
            set
            {
                appTheme = value;
                RaisePropertyChanged("AppTheme");
            }
        }


        public TabViewModel CurrentTab
        {
            get { return tabs[currentTabIdx]; }
        }
        public TabContext CurrentTabContext
        {
            get { return CurrentTab.TabContext; }
        }
        public CommandManager Manager
        {
            get { return CurrentTab.Manager; }
        }
        public Project Project
        {
            get { return CurrentTab.TabContext.Project; }
        }
        public AppContext AppContext
        {
            get { return CurrentTab.TabContext.Context; }
        }

        public MainViewModel()
        {
            tabs = new ObservableCollection<TabViewModel>();
            tabs.Add(new TabViewModel());
            currentTabIdx = 0;

            AppOption = new AppOption();
            AppTheme = new Theme();
            AppContext.SelectedParen = Parentheses.BASE_KAGI;


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
                ((AppContext.EditorMode == (int)EditMode.modify)
                        || (AppContext.EditorMode == (int)EditMode.insert && AppContext.InputText == "")));

            SetInterpolateModeCom = new RelayCommand(
                () => { AppContext.EditorMode = (int)EditMode.interpolate; },
                () => (AppContext != null && Project.lines.Contains(AppContext.SelectedLine))
                );
            #endregion
            
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

            #region ���[���܂��

            UndoRelay = new RelayCommand(
                () => { Manager.Undo();},
                () => { return Manager.CanUndo(); });
            RedoRelay = new RelayCommand(
                () => { Manager.Redo(); },
                () => { return Manager.CanRedo(); });
            SubmitLineRelay = new RelayCommand(
                () => { CurrentTab.SubmitLine(); },
                () => { return CurrentTab.CanSubmitLine(); });
            DeleteLineRelay = new RelayCommand(
                () => { Manager.Execute(new DeleteLineCommand(CurrentTabContext)); },
                () => { return CurrentTab.DeleteLine.CanExecute(); });
            AddDescriptionRelay = new RelayCommand(
                () => { Manager.Execute(new AddDescriptionLineCommand(CurrentTabContext)); },
                () => { return CurrentTab.AddDescription.CanExecute(); }
                );
            DeleteTabCommand = new RelayCommand(
                () => { RemoveTab(CurrentTab); },
                () => { return (tabs.Count >= 2); }
                );
            #endregion
        }

        /// <summary>
        /// Tab��V�����ǉ�
        /// </summary>
        /// <returns></returns>
        public bool AddTab(TabViewModel tab)
        {
            if (tab != null)
            {
                tabs.Add(tab);
                RaisePropertyChanged("TabNames");
                return true;
            }
            return false;
        }
        public bool RemoveTab(TabViewModel tab)
        {
            
            if (tabs.Contains(tab) && tabs.Count > 0)
            {
                if(CurrentTabIdx >= tabs.IndexOf(tab) && CurrentTabIdx >= 1)
                {
                    tabs.Remove(tab);
                    CurrentTabIdx--;
                }
                else
                    tabs.Remove(tab);
                RaisePropertyChanged("TabNames");
                return true;
            }
            return false;
        }

        #region 
        ///�����[�R�}���h�B�������R�}���h�N���X�ɂقڊۓ������Ă�̂�Relay,
        ///�����ŏ�������������Ă�����̂�Command�Ɩ���

        public RelayCommand DeleteLineRelay { get; private set; }
        public RelayCommand SubmitLineRelay { get; private set; }
        public RelayCommand AddDescriptionRelay { get; private set; }

        public RelayCommand RedoRelay { get; private set; }
        public RelayCommand UndoRelay { get; private set; }
        public RelayCommand DeleteTabCommand { get; private set; }

        #endregion
        #region �R�}���h Commands Classes �����ɂ��A�폜�\��
        #region Paren�Ǘ���ʃR�}���h
        public RelayCommand AddParenCom { get; private set; }

        public RelayCommand<bool> DeleteParenCom { get; private set; }
        private void DeleteParen()
        {
            if (AppContext != null && Project.parens.Contains(AppContext.SelectedParen))
                Project.parens.Remove(AppContext.SelectedParen);
        }
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
        #endregion

        public void setP()
        {
            MessengerInstance.Send<ParensCollectionMessage>(new ParensCollectionMessage(Project.parens));
        }
    }
}