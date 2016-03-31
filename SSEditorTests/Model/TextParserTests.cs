using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace SSEditor.Model.Tests
{
    [TestClass()]
    public class TextParserTests
    {
        [TestMethod()]
        public void ParseStringtoLineTest()
        {
            var paren = Parentheses.BASE_KAGI;
            string test = "麻倉「もちょだよー」\r\n";
            var result = TextParser.ParseStringtoLine(test, paren);
            Assert.AreEqual("麻倉", result.speaker.name);
            Assert.AreEqual("もちょだよー", result.line);
        }
        [TestMethod()]
        public void ParseStringtoLineTest2()
        {
            var paren = Parentheses.BASE_COLON;
            string test = "ill Bell : いつもの君の合唱\r\n";
            var result = TextParser.ParseStringtoLine(test, paren);
            Assert.AreEqual("ill Bell", result.speaker.name);
            Assert.AreEqual("いつもの君の合唱", result.line);
        }

        [TestMethod()]
        public void makeRegexfromParensTest1()
        {
            Parentheses[] pars = {
                Parentheses.BASE_COLON,
                Parentheses.BASE_KAGI,
                Parentheses.BASE_PAREN,
                Parentheses.BASE_QUO,
                Parentheses.BASE_EMPTY
            };
            var t = TextParser.makeRegexfromParens(pars);
            Console.Write(t.ToString());
            
        }

        [TestMethod()]
        public void importProjectTest()
        {
            Parentheses[] pars = {
                Parentheses.BASE_COLON,
                Parentheses.BASE_KAGI,
                Parentheses.BASE_PAREN,
                Parentheses.BASE_QUO,
                Parentheses.BASE_EMPTY

            };
            string text = "こんにちは。\r\n雨宮「こんにちは」\r\n麻倉「あああああああ」\r\n夏川「テステス」\r\n";
            var p = TextParser.importProject(text, pars);
            Assert.AreEqual(4, p.people.Count);
            Assert.AreEqual(4, p.lines.Count);
            Assert.AreEqual("こんにちは。", p.lines.First().line);
        }
    }

}