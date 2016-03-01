using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;


//Ver 0.01
//現在未使用(Code Behindにて実装)
namespace SSEditor.View.Behavior
{
    class InputTextBoxBehavior :Behavior<TextBox>
    {
        void Focus(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(this.AssociatedObject.Focusable)
                this.AssociatedObject.Focus();
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseDoubleClick += Focus;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseDoubleClick -= Focus;
        }


    }
}
