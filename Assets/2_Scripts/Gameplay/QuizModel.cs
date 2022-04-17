using UniRx;
using QuizGame.Parsing;

namespace QuizGame.Gameplay
{
    public class Quiz
    {
        public ReactiveProperty<string> word = new ReactiveProperty<string>();
        public ReactiveProperty<int> attempts = new ReactiveProperty<int>();
        public ReactiveProperty<int> score = new ReactiveProperty<int>();
        public ReactiveProperty<int> rightAnswers = new ReactiveProperty<int>();

        public TextParser Parser { get; }

        public Quiz(TextParser parser) => Parser = parser;
    }
}
