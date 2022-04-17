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
        private CompositeDisposable quizDisp = new CompositeDisposable();

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            wordContainer = Instantiate(wordContainer, uiRoot);

            alphabetContainer = Instantiate(alphabetContainer, uiRoot);
            alphabetContainer.buttonClicked.Subscribe(OnTriedToGuess).AddTo(this);

            InitQuiz();
        }

        private void InitQuiz()
        {
            quizDisp.Clear();

            quiz = new Quiz(new TextParser(config.textSource.text, config.minWordLength, config.randomizeBufferCapacity));
            quiz.score.Value = 0;
            StartNewRound();

            quiz.word.Subscribe(OnWordChanged).AddTo(quizDisp);
            quiz.attempts.Subscribe(count => attemptsCounter.text = count.ToString()).AddTo(quizDisp);
            quiz.score.Subscribe(count => scoreCounter.text = count.ToString()).AddTo(quizDisp);

            quiz.attempts.Where(count => count == 0).Subscribe(_ => OnLost()).AddTo(quizDisp);
            quiz.rightAnswers.Where(count => count == quiz.word.Value.Distinct().Count()).Subscribe(_ => OnWordCompleted()).AddTo(quizDisp);
        }

        private void OnWordChanged(string newWord)
        {
            if (string.IsNullOrEmpty(newWord))
            {
                OnWon();
                return;
            }

            Debug.Log($"Hint:\r\n{newWord}");
            wordContainer.SetWord(newWord, isShown: false, isInteractable: false);
            alphabetContainer.SetWord(TextParser.letters, isShown: true, isInteractable: true);
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
            InitQuiz();
        }

        private void StartNewRound()
        {
            quiz.rightAnswers.Value = 0;
            quiz.attempts.Value = config.attemptsPerWord;
            quiz.word.Value = quiz.Parser.GetWord();
        }
    }
}
