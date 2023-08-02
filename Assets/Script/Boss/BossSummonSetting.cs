using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace GameCore.Boss.core
{
    [CreateAssetMenu(fileName = "BossSummonSetting", menuName = "Boss/BossSummonSetting", order = 0)]
    public class BossSummonSetting : ScriptableObject {
        [SerializeField] SummonData[] summonDatas;
        public IEnumerator<SummonData> GetSummonData(){ 
            return summonDatas.AsEnumerable().GetEnumerator();
        }
    }
    
}