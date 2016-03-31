using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using ColorFont;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Windows.Input;

namespace SSEditor
{
    [Serializable]
    public class Person : INotifyPropertyChanged
    {
        public const int ID_DESCRIPT = -2;
        public const int ID_UNDEFINED = -1;
        public static readonly Person DESCRIPT = new Person(ID_DESCRIPT, "[地の文]");
        #region コンストラクタ
        public Person()
        {
            _id = -1;
            _name = "noname";
            font = new FontInfo();


            hotkey = new HotkeyInfo();
        }
        public Person(string name)
        {
            _id = ID_UNDEFINED;
            _name = name;
            font = new FontInfo();
            _hotkey = new HotkeyInfo();

        }
        public Person(int id, string name)
        {
            _id = id;
            _name = name;
            font = new FontInfo();
            _hotkey = new HotkeyInfo();
        }
        public Person(int id, string name, ModifierKeys modifier, Key key)
        {
            _id = id;
            _name = name;
            font = new FontInfo();
            _hotkey = new HotkeyInfo(true, modifier, key);
        }
        public Person(Person p)
        {
            _id = p.id;
            _name = p.name;
            font = p.font;
            _hotkey = p.hotkey;
        }
        #endregion

        private int _id;
        public int id { get { return _id; } set
            {
                _id = value;
                OnPropertyChanged("id");
            }
        }
        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("name");
            }
        }
        private HotkeyInfo _hotkey;
        public HotkeyInfo hotkey { get { return _hotkey; } set
            {
                _hotkey = value;
                OnPropertyChanged("hotkey");
            } }

        

        /// <summary>
        /// 解決する必要あり
        /// </summary>
        private SerializableFontInfo _font_serializable;

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        public FontInfo font
        {
            get
            {
                return _font_serializable.convert2FontInfo();
            }
            set
            {
                _font_serializable = new SerializableFontInfo(value);
                OnPropertyChanged("font");
            }
        }
    }

    [Serializable]
    public class HotkeyInfo{
        public HotkeyInfo()
        {
            enable = false;
            Modifiers = ModifierKeys.Control;
            key = Key.D0;

        }
        public HotkeyInfo(bool e, ModifierKeys m, Key k)
        {
            enable = e;
            Modifiers = m;
            key = k;
        }
        public bool enable { get; set; }
        public ModifierKeys Modifiers { get; set; }
        public Key key { get; set; }
        public string Key2String
        {
            get
            {
                if (enable)
                    return "(" + Modifiers.ToString() + "+" + key.ToString().Replace("D", "") + ")";
                else
                    return "";
            }
            private set { }
        }


    }

}
