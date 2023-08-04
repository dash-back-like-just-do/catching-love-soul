
using UnityEngine;
using GameCore.Basic;
using System;
using GameCore.Boss.core;
using System.Collections;
using utils;

namespace GameCore.Boss
{

    public class BossController : Entity, IBossController
    {
        [SerializeField] Rigidbody2D _rigidbody2D;
        [SerializeField] BossData _bossData;
        [SerializeField] BossSummonSetting _summonSetting;
        StateMachine _stateMachine;

        Vector2 _moveDirection;
        float _moveDuration = 0;
        int _attackType = 0;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            _stateMachine.AddState(BossStateTag.Idle, new BossIdleState(this, _stateMachine));
            _stateMachine.AddState(BossStateTag.Move, new BossMoveState(this, _stateMachine));
            _stateMachine.AddState(BossStateTag.Attack, new BossAttackState(this, _stateMachine));
            _stateMachine.AddState(BossStateTag.Hurt, new BossHurtState(this, _stateMachine));
            _stateMachine.SetDefaultState(BossStateTag.Idle);

        }
        private void Start()
        {
            _stateMachine.Start();
        }
        private void Update()
        {
            _stateMachine.OnUpdate();
        }
        private void FixedUpdate()
        {
            _stateMachine.OnFixUpdate();
        }
        #region  interface
        public void OnHurt(Action onComplete)
        {
            _stateMachine.ChangeState(BossStateTag.Hurt, onComplete);
        }
        //sec <= 0 : move for ever
        public void OnMove(Vector2 dir, Action onComplete, float sec = 0)
        {
            _moveDirection = dir;
            _moveDuration = sec;
            _stateMachine.ChangeState(BossStateTag.Move, onComplete);
        }
        public void OnAttack(int attackId, Action onComplete)
        {
            this._attackType = attackId;
            _stateMachine.ChangeState(BossStateTag.Attack, onComplete);

        }
        #endregion
        #region  actions
        public void DoMove(Action onComplete)
        {
            _rigidbody2D.velocity = _bossData.MoveSpeed * _moveDirection;

            if (_moveDuration > 0)
                StartCoroutine(WatForComplete());

            IEnumerator WatForComplete()
            {
                yield return new WaitForSeconds(_moveDuration);
                onComplete();
            }
        }

        public void StopMove()
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
        public void DoAttack()
        {
            BossSummonHandller bossSummonHandller = new BossSummonHandller(_summonSetting);
            bossSummonHandller.Summon((summonType)_attackType,transform.position);
        }
        #endregion

        #region Testing
        [Header("Testing")]
        public Vector2 dir;
        public float duration;
        public void OnMoveTest()
        {
            OnMove(dir, () => { }, duration);
        }
        public void OnIdleTest()
        {
            _stateMachine.ChangeState(BossStateTag.Idle);
        }
        public void OnAttack(){
            OnAttack(1,()=>{});
        }
        #endregion

    }
}