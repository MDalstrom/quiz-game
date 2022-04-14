using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace QuizGame.Graphics
{
    public class CharacterContainer : MonoBehaviour
    {
        [SerializeField] private CharacterButton buttonPrefab;
        [SerializeField] private Transform root;

        private readonly List<CharacterButton> buttons = new List<CharacterButton>();

        public void UpdateView(char?[] word)
        {
            ResetWord();

            for (int i = 0; i < word.Length; i++)
            {
                AddCharacter(word[i]);
            }
        }
        public void UpdateView(string word)
        {
            UpdateView(word);
        }

        /// <summary>
        /// Fill up container with empty word of given length
        /// </summary>
        public void UpdateView(int length)
        {
            UpdateView(new char?[length]);
        }

        private void ResetWord()
        {
            buttons.ForEach(x => Destroy(x));
            buttons.Clear();
        }

        private CharacterButton AddCharacter(char? value)
        {
            var button = Instantiate(buttonPrefab, root);
            button.Init();
            button.UpdateView(value);
            return button;
        }
    }
}
