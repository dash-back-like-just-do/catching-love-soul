
using System.Collections.Generic;
using System.Linq;
using GameCore.Boss.ChessSpace;
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

            Chess chess = _summonDictonary[type].chess;
            IEnumerator<SummonPoint> points =  _summonDictonary[type].summonPoint.AsEnumerable().GetEnumerator();
            while(points.MoveNext())
            {
                SummonPoint currentPoint = points.Current;
                Chess unit = SummonNewUnit(onPosition, chess, currentPoint);
                unit.OnSpawn(currentPoint.direction);
            }
        }

        private Chess SummonNewUnit(Vector2 onPosition, Chess chess, SummonPoint currentPoint)
        {
            Vector2 instalatePos = onPosition + currentPoint.relativePosition;
            return Object.Instantiate(
                                chess,
                                instalatePos,
                                Quaternion.FromToRotation(instalatePos, Vector3.zero));
        }
    }
}