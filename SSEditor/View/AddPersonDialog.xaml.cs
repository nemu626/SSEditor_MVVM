using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ColorFont;

namespace SSEditor
{
    /// <summary>
    /// AddPersonDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class AddPersonDialog : Window
    {

        private ViewModel.MainViewModel vm;

        public AddPersonDialog()
        {
            InitializeComponent();
          　vm = this.DataContext as ViewModel.MainViewModel;
        }

        private void clickFontChoosebutton(object sender, RoutedEventArgs e)
        {
            ColorFontDialog dlg = new ColorFontDialog();
            dlg.Owner = this;
            dlg.Font = FontInfo.GetControlFont(sampleTextBlock);
            if (dlg.ShowDialog() == true && dlg.Font != null)
                    vm.AppContext.SelectedPerson.font = dlg.Font;
        }
        private void clickOKbutton(object sender, RoutedEventArgs e)
        {
            if (vm.AppContext.SelectedPerson.hotkey.enable)
            {
                if ((bool)altradio.IsChecked)
                    vm.AppContext.SelectedPerson.hotkey.Modifiers = ModifierKeys.Alt;
                else if ((bool)ctrlradio.IsChecked)
                    vm.AppContext.SelectedPerson.hotkey.Modifiers = ModifierKeys.Control;
                else
                    vm.AppContext.SelectedPerson.hotkey.enable = false;

                if (hotkeyCombobox.SelectedIndex >= 0 && hotkeyCombobox.SelectedIndex < 10)
                    vm.AppContext.SelectedPerson.hotkey.key = hotkeyCombobox.SelectedIndex + Key.D0;
                else
                    vm.AppContext.SelectedPerson.hotkey.enable = false;


            }
            this.DialogResult = true;
        }
        private void clickCancelbutton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }

 
}
