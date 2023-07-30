using UnityEngine;
namespace GameCore.Boss.core
{
    [CreateAssetMenu(fileName = "BossData", menuName = "Boss/BossData", order = 0)]
    public class BossData : ScriptableObject {
        
        [field: SerializeField]public float MoveSpeed{get;private set;}
    }
}