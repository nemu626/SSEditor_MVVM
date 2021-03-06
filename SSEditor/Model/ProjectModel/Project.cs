﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SSEditor
{
    [Serializable]
    public class Project : INotifyPropertyChanged
    { 
        //properties
        public string _title { get; set; }
        public string title {
            get
            { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("title");
            }
        }

        public ObservableCollection<Person> people { get; set; }
        public ObservableCollection<Line> lines { get; set; }
        public ObservableCollection<Parentheses> parens { get; set; }

        private bool _displaySpeakerFlag;
        public bool displaySpeakerFlag
        {
            get { return _displaySpeakerFlag; }
            set { _displaySpeakerFlag = value;
                OnPropertyChanged("displaySpeakerFlag");
                OnPropertyChanged("text");
            }
        }
        private string _text;
        public string text {
            get
            {
                if(_text != Lines2Text(displaySpeakerFlag))
                    text = Lines2Text(displaySpeakerFlag);
                return _text;
            }
            private set
            {
                _text = value;
                OnPropertyChanged("text");
            }
        }


        public Person[] ctrlHotkey;
        public Person[] altHotkey;

        public Project()
        {
            this.title = "Untitled";
            this.people = new ObservableCollection<Person>();
            this.lines = new ObservableCollection<Line>();
            this.parens = new ObservableCollection<Parentheses>();
            this.displaySpeakerFlag = true;
            people.Add(Person.DESCRIPT);
            parens.Add(Parentheses.BASE_KAGI);
            parens.Add(Parentheses.BASE_PAREN);
            parens.Add(Parentheses.BASE_EMPTY);
            parens.Add(Parentheses.BASE_QUO);
            parens.Add(Parentheses.BASE_COLON);


            this.text = "";
            ctrlHotkey = new Person[10];
            altHotkey = new Person[10];
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        public string Lines2Text(bool speakerFlag = true)
        {
            StringBuilder sb = new StringBuilder();
            foreach(Line l in lines)
                sb.Append(l.Line2String(speakerFlag));
            return sb.ToString();
        }
        public bool AddPerson(Person p)
        {
            if (p == null || String.IsNullOrEmpty(p.name))
                return false;

            if (p.hotkey.enable && CheckSameHotKey(p.hotkey))
                return false;

            people.Add(p);
            
            return true;
        }
        public bool RemovePerson(Person p)
        {
            if (people.Contains(p))
            {
                for (int i = lines.Count - 1; i >= 0; i--)
                {
                    if (lines[i].speaker.Equals(p))
                        lines.Remove(lines[i]);
                }
                people.Remove(p);
                return true;
            }
            return false;
        }
        public void AddLine(Line l, Line frontLine = null)
        {
            if(l != null)
            {
                //frontLineの後に挿入
                if (frontLine != null || lines.Contains(frontLine))
                    lines.Insert(lines.IndexOf(frontLine) + 1, l);
                //末尾にAdd
                else
                    lines.Add(l);
                OnPropertyChanged("text");
            }
        }
        public bool RemoveLine(Line l)
        {
            if (lines.Remove(l))
            {
                OnPropertyChanged("text");
                return true;
            }return false;
        }
        public bool ModifyLine(Line l,string newline,Person newperson = null,Parentheses newparen = null)
        {
            if (lines.Contains(l) && l.Modify(newline, newperson,newparen))
            {
                OnPropertyChanged("text");
                return true;
            }
            return false;
        }

        public bool CheckSameHotKey(HotkeyInfo key)
        {
            foreach(Person p in this.people)
            {
                if (p.hotkey.Equals(key))
                    return true;
            }
            return false;
        }
    }
}
