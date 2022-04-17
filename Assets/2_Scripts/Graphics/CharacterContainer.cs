using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
using UniRx;

namespace QuizGame.Graphics
{
    public class CharacterContainer : MonoBehaviour
    {
        [SerializeField] private CharacterButton buttonPrefab;
        [SerializeField] private Transform root;

        private readonly List<CharacterButton> buttons = new List<CharacterButton>();
        public readonly Subject<CharacterButton> buttonClicked = new Subject<CharacterButton>();
        
        public void SetWord(string word, bool isShown, bool isInteractable)
        {
            SetCharactersCapacity(word.Length);

            foreach (var (b, c) in buttons.Zip(word, Tuple.Create))
            {
                b.state.Value = isShown;
                b.character.Value = c;
                b.isInteractable.Value = isInteractable;
            }
        }

        public void UpdateCharacters(char c, bool state)
        {
            buttons.Where(x => x.character.Value == c).ToList().ForEach(x => x.state.Value = state);
        }

        private void SetCharactersCapacity(int capacity)
        {
            while (buttons.Count < capacity)
            {
                var newButton = Instantiate(buttonPrefab, root);
                newButton.Init();
                buttons.Add(newButton);
                newButton.onClick.Subscribe(buttonClicked).AddTo(this);
            }
            for (int i = 0; i < buttons.Count; i++) 
            {
                buttons[i].gameObject.SetActive(i < capacity);
            }
        }
    }
}
