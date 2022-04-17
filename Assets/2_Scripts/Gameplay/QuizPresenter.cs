using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using QuizGame.Parsing;
using QuizGame.Graphics;
using QuizGame.Configs;

namespace QuizGame.Gameplay
{
    public class QuizPresenter : MonoBehaviour
    {
        [SerializeField] private GameConfig config;
        [Header("UI")]
        [SerializeField] private Text scoreCounter;
        [SerializeField] private Text attemptsCounter;
        [Header("Container prefabs")]
        [SerializeField] private CharacterContainer wordContainer;
        [SerializeField] private CharacterContainer alphabetContainer;
        [SerializeField] private MessageContainer gameEndContainer;
        [Space]
        [SerializeField] private Transform uiRoot;

        private Quiz quiz;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            quiz = new Quiz(new TextParser(config.textSource.text, config.minWordLength, config.randomizeBufferCapacity));
            quiz.score.Value = 0;
            StartNewRound();

            // Prefabs
            wordContainer = Instantiate(wordContainer, uiRoot);

            alphabetContainer = Instantiate(alphabetContainer, uiRoot);
            alphabetContainer.buttonClicked.Subscribe(OnTriedToGuess).AddTo(this);

            // Subscriptions
            quiz.word.Subscribe(OnWordChanged).AddTo(this);
            quiz.attempts.Subscribe(count => attemptsCounter.text = count.ToString()).AddTo(this);
            quiz.score.Subscribe(count => scoreCounter.text = count.ToString()).AddTo(this);

            quiz.attempts.Where(count => count == 0).Subscribe(_ => OnLost()).AddTo(this);
            quiz.rightAnswers.Where(count => quiz.word.HasValue && count == quiz.word.Value.Length).Subscribe(_ => OnWordCompleted()).AddTo(this);
        }

        private void OnWordChanged(string newWord)
        {
            if (string.IsNullOrEmpty(newWord)) OnWon();

            wordContainer.SetWord(newWord, isShown: false);
            alphabetContainer.SetWord(TextParser.letters, isShown: true);
        }

        private void OnTriedToGuess(CharacterButton sender)
        {
            var character = sender.character.Value;

            wordContainer.UpdateCharacters(character, true);
            alphabetContainer.UpdateCharacters(character, false);

            if (quiz.word.Value.All(x => x != character)) quiz.attempts.Value--;
            else quiz.rightAnswers.Value++;
        }

        private void OnWordCompleted()
        {
            quiz.score.Value += quiz.attempts.Value;
            StartNewRound();
        }

        private async void OnLost()
        {
            var messageContainer = Instantiate(gameEndContainer, uiRoot);
            await messageContainer.ShowMessage(config.onLostHeader, config.onLostDescription);
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
        private async void OnWon()
        {
            var messageContainer = Instantiate(gameEndContainer, uiRoot);
            await messageContainer.ShowMessage(config.onWonHeader, config.onWonDescription);
        }

        private void StartNewRound()
        {
            quiz.rightAnswers.Value = 0;
            quiz.attempts.Value = config.attemptsPerWord;
            quiz.word.Value = quiz.Parser.GetWord();
        }
    }
}
