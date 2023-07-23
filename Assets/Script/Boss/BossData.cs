using UnityEngine;
namespace GameCore.Boss.core
{
    [CreateAssetMenu(fileName = "BossData", menuName = "Boss/BossData", order = 0)]
    public class BossData : ScriptableObject {
        
        [SerializeField]float _moveSpeed;
        public float MoveSpeed => _moveSpeed;
    }
}