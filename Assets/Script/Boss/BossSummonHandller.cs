
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace GameCore.Boss.core
{
    public class BossSummonHandller
    {
        Dictionary<summonType,SummonData> _summonDictonary;
        public BossSummonHandller(BossSummonSetting setting)
        {
            _summonDictonary = new Dictionary<summonType, SummonData>();

            ImportSummonDictonary(setting);
        }

        private void ImportSummonDictonary(BossSummonSetting setting)
        {
            IEnumerator<SummonData> summonDataEmuerator = setting.GetSummonData();
            while (summonDataEmuerator.MoveNext())
            {
                SummonData data = summonDataEmuerator.Current;
                _summonDictonary.Add(data.summonType, data);
            }
        }

        public void Summon(summonType type,Vector2 onPosition){

            GameObject prefab = _summonDictonary[type].prefab;
            IEnumerator<SummonPoint> points =  _summonDictonary[type].summonPoint.AsEnumerable().GetEnumerator();
            while(points.MoveNext())
            {
                SummonPoint currentPoint = points.Current;
                SummonNewUnit(onPosition, prefab, currentPoint);
            }
        }

        private static void SummonNewUnit(Vector2 onPosition, GameObject prefab, SummonPoint currentPoint)
        {
            Vector2 instalatePos = onPosition + currentPoint.relativePosition;
            GameObject.Instantiate(
                                prefab,
                                instalatePos,
                                Quaternion.FromToRotation(instalatePos, currentPoint.direction));
        }
    }
}