using UnityEngine;

namespace QuizGame.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config", order = 1)]
    public class GameConfig : ScriptableObject
    {
        public TextAsset textSource;
        public int minWordLength;

        public int attemptsPerWord;
    }
}
