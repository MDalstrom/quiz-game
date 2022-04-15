using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

namespace QuizGame.Parsing
{
    public class TextParser
    {
        private string source;
        public TextParser(string source, int minLength = 1) => this.source = source.ToLower();

        public List<string> GetWords()
        {
            Regex.Matches(source, @"\b(?:[a-z]{2,}|[ai])\b", RegexOptions.IgnoreCase);
            throw new System.Exception();
        }
    }
}
