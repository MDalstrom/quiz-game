using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QuizGame.Gameplay
{
    public class QuizRoundController
    {
        private readonly string word;
        private int attempts;

        public QuizRoundController(string word, int attempts)
        {
            this.word = word;
            this.attempts = attempts;
        }

        public bool Guess(char c)
        {
            return word.Contains(c);
        }
    }
}
