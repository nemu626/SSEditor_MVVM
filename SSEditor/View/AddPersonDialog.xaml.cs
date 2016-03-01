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
        public Person person{ get; set; }

        public Person[] ctrl_Hotkey;
        public Person[] alt_Hotkey;

        public AddPersonDialog()
        {
            InitializeComponent();
          　vm = this.DataContext as ViewModel.MainViewModel;
            
            hotkeyCombobox.IsEnabled = false;
            person = new Person();
            ctrl_Hotkey = null;
            alt_Hotkey = null;
        }

        private void clickFontChoosebutton(object sender, RoutedEventArgs e)
        {
            ColorFontDialog dlg = new ColorFontDialog();
            dlg.Owner = this;
            dlg.Font = FontInfo.GetControlFont(sampleTextBlock);
            if (dlg.ShowDialog() == true)
            {
                if (dlg.Font != null)
                {
                    person.font = dlg.Font;
                    FontInfo.ApplyFont(this.sampleTextBlock, person.font);
                }
            }
        }
        private void clickOKbutton(object sender, RoutedEventArgs e)
        {
            person.name = personName.Text;

            if (person.hotkey.enable)
            {
                person.hotkey.key = hotkeyCombobox.SelectedIndex + Key.D0;
            }

            this.DialogResult = true;
        }
        private void clickCancelbutton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void altclick(object sender, RoutedEventArgs e)
        {
            person.hotkey.enable = true;
            person.hotkey.Modifiers = ModifierKeys.Alt;
            hotkeyCombobox.Items.Clear();
            for (int i = 0; i < 10; i++)
                if (alt_Hotkey[i] == null)
                    hotkeyCombobox.Items.Add(i);
            hotkeyCombobox.IsEnabled = true;
        }

        private void ctrlclick(object sender, RoutedEventArgs e)
        {
            person.hotkey.enable = true;
            person.hotkey.Modifiers = ModifierKeys.Control;
            hotkeyCombobox.Items.Clear();
            for(int i = 0; i< 10; i++)
                if(ctrl_Hotkey[i] == null)
                    hotkeyCombobox.Items.Add(i);

            hotkeyCombobox.IsEnabled = true;
        }

        private void unuseClick(object sender, RoutedEventArgs e)
        {
            person.hotkey.enable = false;
            hotkeyCombobox.IsEnabled = false;
        }
    }

 
}
