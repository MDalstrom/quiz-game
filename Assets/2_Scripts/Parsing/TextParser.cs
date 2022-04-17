using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

namespace QuizGame.Parsing
{
    public class TextParser
    {
        public const string letters = "abcdefghijklmnopqrstuvwxyz";

        private readonly List<string> uniqueWords;
        private readonly List<string> wordsBuffer;
        private readonly string source;

        private int index;

        public int MinLength { get; set; }

        public TextParser(string source, int minLength = 1, int capacity = 8)
        {
            this.source = source.ToLower();
            MinLength = minLength;

            uniqueWords = new List<string>();
            index = 0;
        }

        public string GetWord()
        {
            wordsBuffer.
        }

        private string ParseWord()
        {
            var result = "";

            while (index < source.Length)
            {
                var currentChar = source[index];
                index++;

                if (letters.Contains(currentChar))
                {
                    result += currentChar;
                }
                else if (result.Length < MinLength || uniqueWords.Contains(result))
                {
                    result = "";
                }
                else 
                {
                    uniqueWords.Add(result);
                    return result;
                }
            }
            return null;
        }
    }
}
