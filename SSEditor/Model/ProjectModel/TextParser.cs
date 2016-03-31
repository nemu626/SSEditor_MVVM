using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSEditor.Model.ProjectModel
{
    public static class TextParser
    {
        //" |" で複数条件にすること
        public static Regex makeRegexfromParen(Parentheses paren)
        {
            return new Regex("(?<speaker>.*)" +
                       "(?<parenstart>"+ Regex.Escape(paren.start) + ")" + 
                       "(?<line>.*)" +
                       "(?<parenend>" + Regex.Escape(paren.end) + ")" +
                       "(?<parenextra>" + Regex.Escape(paren.extra) + ")");
        }
        public static Regex makeRegexfromParens(Parentheses[] parens)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < parens.Count(); i++)
            {
                if (i > 0)
                    sb.Append("|");
                sb.Append(
                    "((?<speaker>.*)" +
                    "(?<parenstart>" + Regex.Escape(parens[i].start) + ")" +
                    "(?<line>.*)" +
                    "(?<parenend>" + Regex.Escape(parens[i].end) + ")" +
                    "(?<parenextra>" + Regex.Escape(parens[i].extra) + "))");
            }
            return new Regex(sb.ToString());
        }
        public static Line ParseStringtoLine(string str,Parentheses paren)
        {
            if(str != null && paren != null)
            {
                Regex reg = makeRegexfromParen(paren);
                if (reg.IsMatch(str))
                {
                    var match = reg.Match(str);
                    var speaker = new Person(match.Groups["speaker"].Value);
                    string line = match.Groups["line"].Value;
                    return new Line(line, speaker, paren);
                }
            }
            return null;
        }
        public static Project importProject(string text, Parentheses[] parens)
        {
            var result = new Project();
            result.parens = new ObservableCollection<Parentheses>(parens);

            var regex = makeRegexfromParens(parens);
            var matches = regex.Matches(text);

            foreach (Match match in matches)
            {
                string spk = match.Groups["speaker"].Value;
                string line = match.Groups["line"].Value;
                string pstart = match.Groups["parenstart"].Value;
                string pend = match.Groups["parenend"].Value;
                string pextra = match.Groups["parenextra"].Value;
                Person speaker = null;
                Parentheses paren = null;

                foreach (Parentheses p in result.parens)
                    if (p.start == pstart && p.end == pend && p.extra == pextra)
                        paren = p;
                if (paren == null)
                {
                    paren = new Parentheses(pstart, pend, pextra);
                    result.parens.Add(paren);
                }

                if (paren != Parentheses.BASE_EMPTY)
                {
                    foreach (Person p in result.people)
                        if (p.name == spk)
                            speaker = p;
                    if (speaker == null)
                    {
                        speaker = new Person(spk);
                        result.AddPerson(speaker);
                    }
                }else
                {
                    line = match.Groups["speaker"].Value;
                    speaker = Person.DESCRIPT;
                }
                
                result.AddLine(new Line(line, speaker, paren));
            }

                return result;
        }
    }
}
