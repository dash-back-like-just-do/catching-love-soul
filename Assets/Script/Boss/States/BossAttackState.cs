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
            _bossContext.DoAttack();
            _bossContext.AnimationController.PlaySummon();
            _bossContext.StartCoroutine(Next());
        }
        IEnumerator Next(){
            yield return new WaitForSeconds(.5f);
            _stateMachine.MoveNextState();
        }
        public override void OnUpdate(){

        }
    }
}