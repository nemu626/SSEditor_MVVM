using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;

namespace SSEditor.View.Behavior
{
    public class MessageBoxBehavior : Behavior<Button>
    {
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageBoxBehavior),
                                        new UIPropertyMetadata(null));
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        private void Alert(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Message))
            {
                MessageBox.Show(this.Message);
            }
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Click += Alert;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.Click -= Alert;
        }
    }

    public class MessageOKCancelBehavior : Behavior<Button>
    {
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageOKCancelBehavior),
                                        new UIPropertyMetadata(null));
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        private void Alert(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Message))
            {
                var b = sender as Button;
                if (MessageBox.Show(this.Message, "", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    b.CommandParameter = true;
                else
                    b.CommandParameter = false;
            }
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Click += Alert;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.Click -= Alert;
        }
    }
}
