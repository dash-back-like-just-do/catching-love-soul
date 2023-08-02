using GameCore.Basic;

namespace GameCore.Boss.core
{
    public class BossAttackState:BossState
    {
        public BossAttackState(BossController bossContext,IStateMachineContext stateMachine) : base(bossContext,stateMachine){
            
        }
        public override void OnEnter(){
            _bossContext.DoAttack();
        }

        public override void OnUpdate(){

        }
    }
}