using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.IO;
using System.Xml.Serialization;
using ColorFont;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace SSEditor
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// 
    /// 本来MVVM的実装は、ビハインドを記述しないのが理想らしいけど、
    /// FileDialog, Focus, InputBindingsの動的操作等
    /// ビハインドが必要な機能がかなりあります。
    /// このファイルからどんどんメソッドを減らしてって初期コードに近づけていくのが目標
    /// (将来的にMessage・Behaviorを使うべきだと思われる）
    /// 主に実装した機能
    /// １．MenuBarクリックに対する操作(FileDialog関連が多かったのでビハインドに記述)
    /// ２．Focusに関する機能(あらゆる場面でエディター入力ボックスにFocusする機能が必要)
    /// 　　Behaviorで解決できるならいつかしたい。
    /// ３．Hotkey登録。VM側からViewにKeybindはできないので。解決するならMessageによる実装になりそう。
    /// 
    /// プロパティ
    /// MainViewModel vm
    ///     DataContextとして指定されたViewModel.将来的にタブエディタにする場合、複数のViewModelを使うので
    ///     MainViewのコレクションを保持し、vmを現在選択中のViewModelにするのが良いかと思われる。
    /// </summary>
    public partial class MainWindow
    {
        ViewModel.MainViewModel vm;
        
        public MainWindow()
        {
            InitializeComponent();
            vm = this.DataContext as ViewModel.MainViewModel;
            UpdateHotkeys();
        }

        private void AddPersoNclicked(object sender, RoutedEventArgs e)
        {
            AddPersonDialog d = new AddPersonDialog();
            vm.AppContext.SelectedPerson = new Person();
            if(d.ShowDialog() == true)
            {
                vm.Project.AddPerson(vm.AppContext.SelectedPerson);
                AddHotkey(vm.AppContext.SelectedPerson);
            }
        }
        private void ModifyPersonclicked(object sender, RoutedEventArgs e)
        {
            AddPersonDialog d = new AddPersonDialog();
            var p = vm.AppContext.SelectedPerson;
            if (p == null)
                return;
            vm.AppContext.SelectedPerson = new Person(vm.AppContext.SelectedPerson);
            if (d.ShowDialog() == true)
            {
                vm.Project.people[vm.Project.people.IndexOf(p)] = vm.AppContext.SelectedPerson;
                UpdateHotkeys();
                
            }
        }
        #region MenuBar 関連操作メソッド
        private void New()
        {
            vm.AddTab(new ViewModel.TabViewModel());
            vm.CurrentTabIdx++;
            UpdateHotkeys();
        }
        private void Open()
        {
            var dlg = FileManager.DlgFilterSST(new OpenFileDialog(), vm.AppContext.FileName);

            if (dlg.ShowDialog() == true &&
                (vm.AddTab(new ViewModel.TabViewModel(
                    new ViewModel.TabContext(FileManager.DeserializeProject(dlg.FileName), new ViewModel.AppContext())))))
            {
                vm.CurrentTabIdx = vm.CurrentTabIdx + 1;
                vm.AppContext.FilePath = dlg.FileName;
                vm.AppContext.FileName = new FileInfo(vm.AppContext.FilePath).Name;
                vm.Project.title = vm.AppContext.FileName;
            }
            Focus2InputTextBox();
            UpdateHotkeys();
        }
        private void Save()
        {
            if (String.IsNullOrEmpty(vm.AppContext.FilePath))
                SaveAs();
            else
                FileManager.SerializeProject(vm.Project, vm.AppContext.FilePath);
        }
        private void SaveAs()
        {
            var dlg = FileManager.DlgFilterSST(new SaveFileDialog(), vm.AppContext.FileName);
            if (dlg.ShowDialog() == true)
                FileManager.SerializeProject(vm.Project, dlg.FileName);
        }
        private void Export2Text()
        {
            var dlg = FileManager.DlgFilterTXT(new SaveFileDialog(), vm.AppContext.FileName);
            if (dlg.ShowDialog() == true)
                FileManager.WriteFile(dlg.FileName, vm.Project.Lines2Text(vm.Project.displaySpeakerFlag));
        }
        private void ParenSetting()
        {
            var dlg = new View.ParenOption();
            dlg.ShowDialog();
        }

        private void MenuHandler_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            switch (item.Name)
            {
                case "NewMenu":
                    New();
                    break;

                case "OpenMenu":
                    Open();
                    break;

                case "SaveMenu":
                    Save();
                    break;

                case "SaveAsMenu":
                    SaveAs();
                    break;

                case "ExportTextMenu":
                    Export2Text();
                    break;

                case "ExitMenu":
                    Application.Current.MainWindow.Close();
                    break;
                case "SetParenMenu":
                    ParenSetting();
                    break;

            }
        }
        #endregion

        #region HotKey関連操作

        /// <summary>
        /// parameterで渡されたPersonオブジェクトに対し、hotkeyプロパティからキーバインド情報を取得し、
        /// 現在のView(MainView)にキーバインドとして追加する。
        /// ※現在のInputBindingsからの重複チェックは行わないため、注意が必要
        /// というか重複チェックの実装の仕方がわからない
        /// </summary>
        /// <param name="person"></param>
        private void AddHotkey(Person person)
        {
            if (person.hotkey!=null && person.hotkey.enable)
            {
                var rCom = new RelayCommand<Person>(SelectPerson);
                var kb = new KeyBinding(rCom, person.hotkey.key, person.hotkey.Modifiers);
                kb.CommandParameter = person;
                this.InputBindings.Add(kb);
            }
        }

        /// <summary>
        /// ParameterのpをSelect状態にして、InputTextBoxをフォーカスする。
        /// (現在AddHotkeyのためのdelegateとしてにみ使われているので削除して匿名メソッドにしたほうがいいかも知れない)
        /// </summary>
        /// <param name="p"></param>
        public void SelectPerson(Person p)
        {
            vm.AppContext.SelectedPerson = p;
            Focus2InputTextBox();
        }
        
        /// <summary>
        /// 現在の全てのKeybindsをリセットし、Project.peopleから再Bind。
        /// personのhotkeyが変わったときに行うよう実装するべし・・・だが
        /// いちいち全部再Bindするので、特定のBindを取り出し修正するメソッドが必要だと思う。
        /// </summary>
        public void UpdateHotkeys()
        {
            this.InputBindings.Clear();
            //Global Hotkey : "ALT + ENTER = TypeLine"
            //                "CTRL+ ENTER = TypeLine DESCRIPT(地の文)
            if (vm != null)
            {
                InputBindings.Add(new KeyBinding(vm.SubmitLineRelay, Key.Enter, ModifierKeys.Alt));
                InputBindings.Add(new KeyBinding(vm.AddDescriptionRelay, Key.Enter, ModifierKeys.Control));
                InputBindings.Add(new KeyBinding(vm.UndoRelay, Key.Z, ModifierKeys.Control));
                InputBindings.Add(new KeyBinding(vm.RedoRelay, Key.Y, ModifierKeys.Control));
            }
            //CusTom HotKey : "Mod + Key = Select Person & Focus to InputTextBox"
            if (vm != null && vm.Project != null)
                foreach (Person p in vm.Project.people)
                    AddHotkey(p);
        }
        #endregion

        #region Focus関連メソッド
        public void MouseButtonFocus2InputTextBox(object sender, MouseButtonEventArgs e)
        {
            Focus2InputTextBox();
        }
        public void Focus2InputTextBox()
        {
            if (this.InputTextBox.Focusable)
                this.InputTextBox.Focus();
        }
        #endregion
    }
}
