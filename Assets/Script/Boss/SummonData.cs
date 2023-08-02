using UnityEngine;
namespace GameCore.Boss.core
{
    [System.Serializable]
    public struct SummonData{
        public summonType summonType;
        public GameObject prefab;
        public SummonPoint[] summonPoint;
        public float speed;
    }
    [System.Serializable]
    public struct SummonPoint{
        public Vector2 relativePosition;
        public Vector2 direction;
    }
}