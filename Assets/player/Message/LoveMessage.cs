using System;
using ChessClub;
using UnityEngine;
using utils;

namespace player
{
    public class LoveMessage : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private HpManager _hpManager;
        private PlayerData _playerData;
        private Counter _existCounter;
        
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _existCounter.Update();
            if (_existCounter.IsTrigger())
            {
                Destroy(this.gameObject);
            }
        }
        public LoveMessage SetExistTime(float time)
        {
            _existCounter = new Counter(time);
            return this;
        }

        public LoveMessage SetPlayerData(PlayerData playerData)
        {
            _playerData = playerData;
            return this;
        }

        public LoveMessage SetHpManager(HpManager hpManager)
        {
            _hpManager = hpManager;
            return this;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Message hit");
            if (other.transform.CompareTag("women"))
            {
                _hpManager.Damage(other.gameObject, _playerData.strength);
            }
        }
    }
}