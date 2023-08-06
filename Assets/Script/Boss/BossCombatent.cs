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
            Debug.Log("on boss hurt");
            if(!_freezeTimer.IsTrigger()) return;
            _hp -= damage;
            Debug.Log("boss:"+_hp);
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
        private void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.CompareTag("Player"))
            {
                DamagePlayer(other);
            }
        }

        private void DamagePlayer(Collision2D other)
        {
            float damage;
            if (_bossController.CurrentState == BossStateTag.Rush)
                damage = _bossData.RushDamage;
            else
                damage = _bossData.Damage;
            _hpManager.Damage(other.gameObject, damage);
        }

        private void OnCollisionStay2D(Collision2D other) {
            if(other.gameObject.CompareTag("Player"))
            {
                DamagePlayer(other);
            }
        }
        private void OnTriggerEnter2D(Collider2D other) {
            
        }
    }
}