using System;
using UnityEngine;
using QuizGame.Graphics;

namespace QuizGame.Gameplay
{
    public class QuizLoader : MonoBehaviour
    {
        private const string alphabet = "abcdefghijklmnopqrstuvwxyz";

        [SerializeField] private CharacterContainer wordContainer;
        [SerializeField] private CharacterContainer alphabetContainer;


        private void Start()
        {
            if (GetComponent<Canvas>() == null) throw new Exception("Loader component must be added to canvas");

            Init();
        }

        private void Init()
        {
            var testword = "alice";

            wordContainer = Instantiate(wordContainer, transform);
            wordContainer.Init(isInteractable: false);
            wordContainer.UpdateView(testword);

            alphabetContainer = Instantiate(alphabetContainer, transform);
            alphabetContainer.Init(isInteractable: true);
            alphabetContainer.UpdateView(alphabet);
        }
    }
}
