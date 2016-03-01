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
    public class Person
    {
        public const int ID_DES = -2;
        public const int ID_UNDEFINED = -1;
        public static readonly Person DESCRIPT = new Person(-2, "");

        public Person()
        {
            id = -1;
            name = "noname";
            font = new FontInfo();


            hotkey = new HotkeyInfo();
        }
        public Person(int _id, string _name)
        {
            id = _id;
            name = _name;
            font = new FontInfo();
            hotkey = new HotkeyInfo();
        }
        public Person(int _id, string _name, ModifierKeys _modifier, Key _key)
        {
            id = _id;
            name = _name;
            font = new FontInfo();
            hotkey = new HotkeyInfo(true, _modifier, _key);
        }
        public int id { get; set; }
        public string name { get; set; }
        public HotkeyInfo hotkey { get; set; }
        

        /// <summary>
        /// 解決する必要あり
        /// </summary>
        private SerializableFontInfo _font_serializable;
        [XmlIgnore]
        public FontInfo font
        {
            get
            {
                return _font_serializable.convert2FontInfo();
            }
            set
            {
                _font_serializable = new SerializableFontInfo(value);
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
