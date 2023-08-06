using System.Collections.Generic;
using UnityEngine;
using utils;
namespace ChessClub
{
    
    public class HpManager
    {
        private List<Ihp> Ihps = new List<Ihp>();
        public void addNewIhp(Ihp i)
        {
            Ihps.Add(i);
        }

        public void Damage(GameObject target, float damage)
        {
            Ihp ihp = target.GetComponent<Ihp>();
            if (ihp!=null)
            {
                ihp.Hurted(damage);
            }
            else
            {
                Debug.Log(target);
            }

        }
    }
}