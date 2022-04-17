using UnityEngine;

namespace QuizGame.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config", order = 1)]
    public class GameConfig : ScriptableObject
    {
        [Header("Game parameters")]
        public TextAsset textSource;
        public int minWordLength;
        public int attemptsPerWord;
        public int randomizeBufferCapacity;

        [Header("In-game texts")]
        public string onWonHeader;
        public string onWonDescription;
        [Space]
        public string onLostHeader;
        public string onLostDescription;
    }
}
