
using UnityEngine;
using GameCore.Basic;
using System;
using GameCore.Boss.core;
namespace GameCore.Boss
{
    public class BossController : Entity
    {
        [SerializeField]Rigidbody2D _rigidbody2D;
        [SerializeField]BossData _bossData;
        StateMachine _stateMachine;
        //moveing
        Vector2 _moveDirection;


        private void Awake() {
            _stateMachine = new StateMachine();
            _stateMachine.AddState(BossStateTag.Move,new BossMoveState(this));
        }
#region  interface
        public void OnHurt(){

        }
        public void OnMove(Vector2 dir){
            _moveDirection = dir;
            _stateMachine.ChangeState(BossStateTag.Move);
        }
        public void OnAttack(int attackId,Action onComplete){

        }
#endregion
#region  actions
        public void DoMove(){
            _rigidbody2D.velocity =_bossData.MoveSpeed * _moveDirection; 
        }
        public void StopMove(){
            _rigidbody2D.velocity = Vector2.zero; 
        }
#endregion
    }
}