
using GameCore.Basic;

namespace GameCore.Boss.core
{

    public class BossIdleState:BossState
    {
        public BossIdleState(BossController bossContext,IStateMachineContext stateMachine) : base(bossContext,stateMachine){
            
        }
        public override void OnEnter(){
            _bossContext.AnimationController.PlayFloat();
        }

        public override void OnUpdate(){

        }
    }
}