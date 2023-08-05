using GameCore.Boss.ChessSpace;
using UnityEngine;
namespace GameCore.Boss.core
{
    [System.Serializable]
    public struct SummonData{
        public summonType summonType;
        public Chess chess;
        public SummonPoint[] summonPoint;
    }
    [System.Serializable]
    public struct SummonPoint{
        public Vector2 relativePosition;
        public Vector2 direction;
    }
}