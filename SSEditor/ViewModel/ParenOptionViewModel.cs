using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSEditor.ViewModel
{
    public class ParenOptionViewModel : ViewModelBase
    {
        private ObservableCollection<Parentheses> parens;
        public  ObservableCollection<Parentheses> Parens {
            get { return parens; }
            set { parens = value; RaisePropertyChanged("Parens"); } }
        private Parentheses selected;
        public  Parentheses Selected {
            get { return selected; }
            set { selected = value; RaisePropertyChanged("Selected"); }}
        private Parentheses newParen;
        public Parentheses NewParen
        {
            get { return newParen; }
            set { selected = value; RaisePropertyChanged("NewParen"); }
        }

        public ParenOptionViewModel()
        {
            Parens = null;
            Selected = null;
            newParen = new Parentheses();
            MessengerInstance.Register<ParensCollectionMessage>(this,
                message => { Parens = message.Parens; });

            AddParenRelay = new RelayCommand<Parentheses>(
                 (paren) => {Parens.Add(paren);},
                 (paren) => (paren != null) && (paren != Parentheses.BASE_EMPTY)
                );
            DeleteParenRelay = new RelayCommand<bool>(
                (delete) =>
                { if (delete) {Parens.Remove(Selected); Selected = null;}  },
                (delete) => Selected != null && Parens.Contains(Selected));
        }

        public RelayCommand<Parentheses> AddParenRelay { get; private set; }
        public RelayCommand<bool> DeleteParenRelay { get; private set; }


    }
}
