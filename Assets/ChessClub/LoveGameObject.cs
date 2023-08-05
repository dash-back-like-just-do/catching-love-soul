using UnityEngine;
using utils;

namespace ChessClub
{
    public class LoveGameObject : MonoBehaviour, Ihp
    {
        #region hpInterface

        private float _hp;
        private HpManager _hpManager;

        public HpManager GetHpManager( )
        {
            return _hpManager;
        }

        public void SetHpManager(HpManager hpManager)
        {
            _hpManager = hpManager;
            hpManager.addNewIhp(this);
        }

        public void Hurted(float damage)
        {
            _hp -= damage;
            Debug.Log(_hp);
        }

        public void Heal(float heal)
        {
            _hp += heal;
        }

        public float GetHp()
        {
            return _hp;
        }

        public void SetHp(float hp)
        {
            _hp = hp;
        }
        #endregion
    }
}