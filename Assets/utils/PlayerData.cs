using UnityEngine;

namespace utils
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "data/PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public float attackTime;
        public float messageSpeed;

    }
}