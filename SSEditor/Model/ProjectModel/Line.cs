﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSEditor
{
    [Serializable]
    public class Line : INotifyPropertyChanged
    {
        //static readonly int paren_angles = 1;   //「」
        //static readonly int paren_doublequotation = 2; //""
        //static readonly int paren_parentheses = 3; //()
        //static readonly int paren_backets = 4; //[]
        //static readonly int paren_usercustom = 5; //custom
        private string _line;
        public string line
        {
            get { return _line; }
            set
            {
                _line = value;
                OnPropertyChanged("line");
            }
        }
        private Person _speaker;
        public Person speaker
        {
            get { return _speaker; }
            set
            {
                _speaker = value;
                OnPropertyChanged("speaker");
            }
        }
        private Parentheses _paren;
        public Parentheses paren { get { return _paren; } set
            {
                _paren = value;
                OnPropertyChanged("paren");
            }
        }

        public Line()
        {
            _line = null;
            _speaker = new Person();
            _paren = Parentheses.BASE_KAGI;
        }
        public Line(string lineStr, Person lineSpeaker)
        {
            _line = lineStr;
            _speaker = lineSpeaker;
            _paren = Parentheses.BASE_KAGI;
        }
        public Line(string lineStr, Person lineSpeaker, Parentheses lineparen)
        {
            _line = lineStr;
            _speaker = lineSpeaker;
            _paren = lineparen;
        }
        public string Line2String(bool speakerFlag = true)
        {
            if (speaker != null && speaker.name != null && speakerFlag && speaker.id != Person.ID_DESCRIPT)
                return speaker.name + paren.EncloseString(line);
            else if (speaker.id == Person.ID_DESCRIPT)
                return paren.EncloseString(line);
            else
                return paren.EncloseString(line);
        }

        public bool Modify(string modifiedLine,Person modifiedSpeaker = null,Parentheses modifiedParen = null)
        {
            if (modifiedLine == line && modifiedSpeaker == speaker && paren == modifiedParen)
                return false;
            else {
                if(modifiedLine != null && modifiedLine != line)
                    line = modifiedLine;
                if (modifiedSpeaker != null && modifiedSpeaker != speaker)
                    speaker = modifiedSpeaker;
                if (modifiedParen != null && modifiedParen != paren)
                    paren = modifiedParen;
                return true;
            }

        }
        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }


    }
    [Serializable]
    public class Parentheses
    {
        public static readonly Parentheses BASE_KAGI = new Parentheses("「", "」", System.Environment.NewLine);
        public static readonly Parentheses BASE_PAREN = new Parentheses("(", ")", System.Environment.NewLine);
        public static readonly Parentheses BASE_EMPTY = new Parentheses("", "", System.Environment.NewLine);
        public static readonly Parentheses BASE_QUO = new Parentheses("\"", "\"", System.Environment.NewLine);
        public static readonly Parentheses BASE_COLON = new Parentheses(" : ", "", System.Environment.NewLine);

        public string start { get; set; }
        public string end { get; set; }
        public string extra { get; set; }
        public string startend { get { return start + " " + end; } private set { } }
        public Parentheses() { }
        public Parentheses(string st,string en, string ex)
        {
            start = st; end = en; extra = ex;
        }
        
        public string EncloseString(string source)
        {
            return start + source + end + extra;
        }

    }
}
