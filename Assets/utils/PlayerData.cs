using UnityEngine;

namespace utils
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "data/PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public float attackTime;
        public float messageSpeed;
        public float moveSpeed;
        public float rollTime;
        public float rollCd;
        public float rollDelay;

        public float rollSpeed;
        public float walkSpeed;
    }
}