using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SSEditor.Model;
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
        private Project project;
        public Project Project
        {
            get { return project; }
            set
            {
                project = value;
                RaisePropertyChanged("Project");
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

        private AppContext appContext;
        public AppContext AppContext
        {
            get { return appContext; }
            set
            {
                appContext = value;
                RaisePropertyChanged("AppContext");
            }
        }


        public MainViewModel()
        {
            Project = new Project();
            AppOption = new AppOption();
            AppContext = new AppContext();
            AppContext.SelectedParen = Parentheses.BASE_KAGI;

            #region Line�Ǘ��R�}���h
            TypeLineCom = new RelayCommand(AddModifyLine,
                () => (appContext.SelectedPerson != null && appContext.SelectedParen != null));
            TypeDescriptCom = new RelayCommand(
                () => {
                    var person = AppContext.SelectedPerson;
                    var paren = AppContext.SelectedParen;
                    AppContext.SelectedPerson = Person.DESCRIPT;
                    AppContext.SelectedParen = Parentheses.BASE_EMPTY;
                    AddModifyLine();
                    appContext.SelectedPerson = person;
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
                () => (AppContext != null && Project.lines.Contains(appContext.SelectedLine))
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
            if (appContext != null && project.parens.Contains(appContext.SelectedParen))
                project.parens.Remove(AppContext.SelectedParen);
        }
        #endregion

        #region Line�Ǘ��R�}���h
        private void AddModifyLine()
        {
            if (AppContext.EditorMode == (int)EditMode.insert)
                project.AddLine(new Line(AppContext.InputText, AppContext.SelectedPerson, appContext.SelectedParen));
            else if (AppContext.EditorMode == (int)EditMode.modify)
            {
                if (AppContext.SelectedLine != null &&
                    AppContext.SelectedLine.Modify(appContext.InputText, appContext.SelectedPerson))
                    RaisePropertyChanged("line");
            }
            else// AppContext.EditorMode == EditorMode.interpolate
                project.AddLine(new Line(AppContext.InputText, AppContext.SelectedPerson, appContext.SelectedParen),
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
            project.AddPerson(AppContext.SelectedPerson);
            AppContext.SelectedPerson = null;
        }
        private void DeletePerson()
        {
            if (project.people.Contains(AppContext.SelectedPerson))
            {
                project.RemovePerson(AppContext.SelectedPerson);
            }
        }
        #endregion



    }
}