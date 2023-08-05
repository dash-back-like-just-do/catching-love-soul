using System.Collections;
using GameCore.Basic;
using UnityEngine;

namespace GameCore.Boss.core
{
    public class BossAttackState:BossState
    {
        public BossAttackState(BossController bossContext,IStateMachineContext stateMachine) : base(bossContext,stateMachine){
            
        }
        public override void OnEnter(){
            
            _bossContext.AnimationController.PlaySummon(()=>{
                _stateMachine.MoveNextState();
            });
            _bossContext.StartCoroutine(DelayAttack());
        }
        IEnumerator DelayAttack(){
            yield return new WaitForSeconds(.3f);
            _bossContext.DoAttack();
        }
        public override void OnUpdate(){

        }
    }
}