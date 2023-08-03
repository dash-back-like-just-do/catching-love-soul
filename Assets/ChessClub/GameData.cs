using UnityEngine;

namespace ChessClub
{
    [CreateAssetMenu(fileName = "ChessGameData", menuName = "GameData/ChessGameData", order = 0)]
    public class GameData : ScriptableObject
    {
        public Vector2 playerInitPosition;
        public Vector2 bossInitPosition;

        public Vector2 mapCenter;
        public Vector2 mapSize;
    }
}