using System;
using ChessClub;
using GameCore.Boss.core;
using UnityEngine;
using utils;

namespace GameCore.Boss.Combatent
{

    public class BossCombatent : MonoBehaviour, Ihp
    {
        [SerializeField] BossController _bossController;
        [SerializeField] BossData _bossData;
        private HpManager _hpManager;
        private Counter _freezeTimer;
        private float _hp;
        bool freeze;
        private void Awake() {
            _freezeTimer = new Counter(_bossData.HurtFreezeTime);
            
        }
        private void Update() {
            _freezeTimer.Update();
        }
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
            if(!_freezeTimer.IsTrigger()) return;
            _hp -= damage;
            Debug.Log(_hp);
            _bossController.OnHurt();
            _freezeTimer.Reset();
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
    }
}