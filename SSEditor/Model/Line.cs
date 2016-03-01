using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSEditor
{
    [Serializable]
    public class Line
    {
        //static readonly int paren_angles = 1;   //「」
        //static readonly int paren_doublequotation = 2; //""
        //static readonly int paren_parentheses = 3; //()
        //static readonly int paren_backets = 4; //[]
        //static readonly int paren_usercustom = 5; //custom
        public string line { get; set; }
        public Person speaker { get; set; }
        public Parentheses paren { get; set; }

        public Line()
        {
            line = null;
            speaker = new Person();
            paren = Parentheses.BASE_KAGI;
        }
        public Line(string lineStr, Person lineSpeaker)
        {
            line = lineStr;
            this.speaker = lineSpeaker;
            paren = Parentheses.BASE_KAGI;
        }
        public Line(string lineStr, Person lineSpeaker, Parentheses lineparen)
        {
            line = lineStr;
            this.speaker = lineSpeaker;
            paren = lineparen;
        }

        public string Line2String()
        {
            if( speaker != null && speaker.name != null)
                return speaker.name + paren.EncloseString(line) + Environment.NewLine;
            else
                return paren.EncloseString(line) + Environment.NewLine;
        }

        public bool Modify(string modifiedLine,Person modifiedSpeaker = null)
        {
            if (modifiedLine == line && modifiedSpeaker == speaker ||
                modifiedSpeaker == null)
                return false;
            else {
                if(modifiedLine != line)
                    line = modifiedLine;
                if (modifiedSpeaker != speaker)
                    speaker = modifiedSpeaker;
                return true;
            }

        }


    }
    [Serializable]
    public class Parentheses
    {
        public static readonly Parentheses BASE_KAGI = new Parentheses("「", "」", "");
        public static readonly Parentheses BASE_PAREN = new Parentheses("(", ")", "");
        public static readonly Parentheses BASE_EMPTY = new Parentheses("", "", "");
        public static readonly Parentheses BASE_QUO = new Parentheses("\"", "\"", "");

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
