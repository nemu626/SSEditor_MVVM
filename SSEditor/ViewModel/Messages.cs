using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSEditor.ViewModel
{
    public class ParensCollectionMessage
    {
        public ParensCollectionMessage(ObservableCollection<Parentheses> parens)
            {Parens = parens;}
        public ObservableCollection<Parentheses> Parens { get; private set; }
    }
    public class TextFilePathMessage
    {
        public TextFilePathMessage(string path)
            { Path = path;}
        public string Path { get; private set; }
    }
    
}
