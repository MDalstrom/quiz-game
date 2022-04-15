using System;
using UnityEngine;
using QuizGame.Graphics;
using QuizGame.Configs;
using QuizGame.Parsing;

namespace QuizGame.Gameplay
{
    public class QuizLoader : MonoBehaviour
    {
        private const string alphabet = "abcdefghijklmnopqrstuvwxyz";

        [SerializeField] private GameConfig config;
        [Header("Container prefabs")]
        [SerializeField] private CharacterContainer wordContainer;
        [SerializeField] private CharacterContainer alphabetContainer;
        [Space]
        [SerializeField] private Transform uiRoot;

        private void Start()
        {
            InitContainers("alice");
        }

        private void InitContainers(string word)
        {
            wordContainer = Instantiate(wordContainer, uiRoot);
            wordContainer.Init(isInteractable: false);
            wordContainer.UpdateView(word);

            alphabetContainer = Instantiate(alphabetContainer, uiRoot);
            alphabetContainer.Init(isInteractable: true);
            alphabetContainer.UpdateView(alphabet);
        }
    }
}
